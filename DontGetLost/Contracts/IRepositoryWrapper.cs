using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontGetLost.Contracts
{
    interface IRepositoryWrapper
    {
        ITestRepository Test { get; }
        void Save();
    }
}
