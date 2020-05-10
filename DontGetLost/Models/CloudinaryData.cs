using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontGetLost.Models
{
    public class CloudinaryData
    {
        public string ImageName { get; }
        public Uri Url { get; }

        public CloudinaryData(string name, Uri url)
        {
            this.ImageName = name;
            this.Url = url;
        }
        public CloudinaryData()
        {
        }
    }
}
