using System.Collections.Generic;

namespace DontGetLost.Data.Seed
{
    public interface ISeed<T>
    {
        IEnumerable<T> Content { get; }
    }
}