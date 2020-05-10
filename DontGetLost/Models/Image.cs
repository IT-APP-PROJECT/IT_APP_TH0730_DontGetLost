using System;

namespace DontGetLost.Models
{
    public class Image
    {
        public string Name { get; set; }
        public Uri Url { get; set; }

        public Image(string name, Uri url)
        {
            this.Name = name;
            this.Url = url;
        }
        public Image()
        {
        }
    }
}
