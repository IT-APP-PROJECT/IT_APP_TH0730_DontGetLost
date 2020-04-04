using DontGetLost.Contracts;
using DontGetLost.Models;

namespace DontGetLost.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private ITestRepository _test;

        public ITestRepository Test
        {
            get
            {
                if (_test == null)
                {
                    _test = new TestRepository(_repoContext);
                }

                return _test;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
