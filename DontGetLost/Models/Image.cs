using System;

namespace DontGetLost.Models
{
    public class Image
    {
        public string Name { get; }
        public Uri Url { get; }

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
