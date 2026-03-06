using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM
{
   public class PhotoInfo
    {
        public int Photo_Id { get; set; }
        public byte[] FileData { get; set; }
        public string ContentType { get; set; }
    }
}
