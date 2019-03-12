using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest.Inventory
{
    internal class FileDetails
    {
        public Guid? Id { get; set; }
        public string Filename { get; set; }
        public Byte[] FileContents { get; set; }
        public Guid FileId { get; set; }
        public DateTime FileUploadDate { get; set; }
        public string UploadedBy { get; set; }
    }
}
