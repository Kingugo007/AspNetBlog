using Medik.Core.Interfaces;
using Medik.Core.ViewModel;
using Medik.Domain.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Medik.Infrastructure.RecordRepository
{
    public class RecordRepository : IRecordRepository
    {

        private readonly string _josnFile = "Stat.json";
        private readonly IJsonOperations _dbContext;
        public RecordRepository(IJsonOperations dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<StatViewModel> GetAllRecords()
        {
            try
            {
                var record = await _dbContext.ReadJson<Record>(_josnFile);
                StatViewModel result = new StatViewModel()
                {
                    NR = record.Select(x => x.NewResearch).Sum(),
                    CR = record.Select(x => x.CancerResearch).Sum(),
                    CS = record.Select(x => x.CancerSupport).Sum() % 100
                };
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
