using AbBus.BAL.ViewModels;
using AbBus.DAL;

namespace AbBus.BAL.DTO
{
    public class UserMapper
    {
        public user DataMapper(UserVM usrMapperObj)
        {
            if (usrMapperObj == null)
            {
                return null;
            }
            return new user
            {
                id = usrMapperObj.Id,
                name = usrMapperObj.Name,
                email = usrMapperObj.Email,
                password = usrMapperObj.Password,
                registeredon = usrMapperObj.RegisteredOn
            };
        }

        public UserVM VMMapper(user usrObj)
        {
            if (usrObj == null)
            {
                return null;
            }
            return new UserVM
            {
                Id = usrObj.id,
                Name = usrObj.name,
                Email = usrObj.email,
                Password = usrObj.password
            };
        }
    }
}
