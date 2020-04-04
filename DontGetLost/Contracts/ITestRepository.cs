using DontGetLost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontGetLost.Contracts
{
    public interface ITestRepository : IRepository<Test>
    {
        IEnumerable<Test> GetAll();
    }
}
