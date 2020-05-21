using DontGetLost.Constants;
using DontGetLost.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DontGetLost.Data.Seed
{
    public class ImageSeed : ISeed<Image>
    {
        private const string c_linkBase = "http://res.cloudinary.com/do4piuzmh/image/upload/";
        public IEnumerable<Image> Content { get; private set; }

        public ImageSeed()
        {
            Content = GenerateImages(new List<(string, string)> {
                 (Maps.C300, "v1589896311/nlkhzxyulfqe5rfufuui.svg"),
                 (Maps.C301, "v1589896323/xe1e3hbpmyc5sa667yqp.svg"),
                 (Maps.C302, "v1589896377/e54austgxanzdpfay8fy.svg"),
                 (Maps.C303, "v1589896386/bbmohsxkwbsbmvngsqso.svg"),
                 (Maps.C304, "v1589896398/pkryaway7zigyxtdbwf7.svg"),
                 (Maps.C400, "v1589896470/ey9yjed13uyghbbttn3j.svg"),
                 (Maps.C401, "v1589896459/hpwvdyzsyfvc9kmslxuc.svg"),
                 (Maps.C402, "v1589896449/a1q5u6wxvprkq6iimocx.svg"),
                 (Maps.C403, "v1589896427/sda1vydpebgojupqnda0.svg"),
                 (Maps.C404, "v1589896412/kvhxtwtf0vt0vktawsif.svg")
            });
        }

        private IEnumerable<Image> GenerateImages(List<(string name, string uri)> dataTuples)
        => dataTuples.Select(t => new Image(t.name, new Uri(c_linkBase + t.uri)));
    }
}