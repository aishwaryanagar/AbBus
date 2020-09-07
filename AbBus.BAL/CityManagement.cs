using AbBus.BAL.DTO;
using AbBus.DAL;
using System;
using System.Linq;

namespace AbBus.BAL
{
    
    public class CityManagement
    {
        private readonly UnitOfWork _uow;

        public CityManagement()
        {
            _uow = new UnitOfWork();
        }

        public bool Add(string name)
        {
            if (!CityExists(name)) // avoid adding duplicate cities 
            {
                try
                {
                    master_city cityObj = new master_city
                    {
                        name = name
                    };
                    _uow.Cities.Insert(cityObj);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool CityExists(string name)
        {
            master_city cityObj = _uow.Cities.Get(filter: x => x.name.Equals(name)).FirstOrDefault();
            if (cityObj == null)
            {
                return false;
            }
            return true;
        }
    }
}
