using AbBus.DAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbBus.DAL
{
    public class CityRepository : GenericRepository<master_city>, ICityRepository
    {
        private readonly AbBusEntities _abBusEntities;
        public CityRepository(AbBusEntities dbcontext) : base(dbcontext)
        {
            _abBusEntities = dbcontext;
        }
    }
}
