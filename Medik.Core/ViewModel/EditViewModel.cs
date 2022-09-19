using System;
using System.Collections.Generic;
using System.Text;

namespace Medik.Core.ViewModel
{
    public class EditViewModel : PostViewModel
    {
        public string Id { get; set; } 
        public string ExistingPhotoPath { get; set; }
    }
}
