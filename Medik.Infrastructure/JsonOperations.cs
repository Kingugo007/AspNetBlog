using Medik.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Medik.Core.Utilities
{
    public class JsonOperations : IJsonOperations
    {
        private readonly string dir = Path.Combine(Environment.CurrentDirectory, "db");
        public string _result { get; set; } = String.Empty;
        public async Task<List<T>> ReadJson<T>(string jsonFile)
        {            
            try
            {
                var path = Path.Combine(dir, jsonFile);
                var file = File.ReadAllText(path);
                var data = JsonConvert.DeserializeObject<List<T>>(file);
                return await Task.FromResult(data!); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }
        public async Task<bool> WriteJson<T>(T model, string jsonFile)
        {
            List<T> collections = new List<T>();
            try
            {
               var path = Path.Combine(dir, jsonFile);
               //get all json data and add to list of users
               var  readtext = File.ReadAllText(path);
               var  data = JsonConvert.DeserializeObject<List<T>>(readtext);
                 if (data != null)
                 {
                 collections.AddRange(data);
                 }
                 //add incoming users to allusers and serialize object
                 collections.Add(model);
                 _result = JsonConvert.SerializeObject(collections);
                 await File.WriteAllTextAsync(path, _result);
                return true; 
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdateJson<T>(List<T> model, string jsonFile)
        {
            try
            {
                var path = Path.Combine(dir, jsonFile);
                if (File.Exists(path))
                { //add incoming users to allusers and serialize object                 
                    _result = JsonConvert.SerializeObject(model);
                    await File.WriteAllTextAsync(path, _result);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
