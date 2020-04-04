using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DontGetLost.Contracts;
using DontGetLost.Models;

namespace DontGetLost.Repository
{
    public class TestRepository : Repository<Test>, ITestRepository
    {
        public TestRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Test> GetAll()
        {
            return FindAll().ToList();
        }
    }
}
