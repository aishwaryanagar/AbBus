using AbBus.BAL.ViewModels;
using AbBus.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbBus.BAL.DTO
{
    public class CityMapper
    {
        public master_city DataMapper(CityVM cityMapperObj)
        {
            if (cityMapperObj == null)
            {
                return null;
            }
            return new master_city
            {
                id = cityMapperObj.Id,
                name = cityMapperObj.Name,
            };
        }

        public CityVM VMMapper(master_city cityObj)
        {
            if (cityObj == null)
            {
                return null;
            }
            return new CityVM
            {
                Id = cityObj.id,
                Name = cityObj.name
            };
        }
    }
}
