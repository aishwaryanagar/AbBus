using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbBus.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AbBusEntities _dbcontext;
        public  UserRepository Users { get; }

        public CityRepository Cities { get; }
        public UnitOfWork()
        {
            _dbcontext = new AbBusEntities();
            Users = new UserRepository(_dbcontext);
            Cities = new CityRepository(_dbcontext);
        }

        public int Save()
        {
            return _dbcontext.SaveChanges();
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
