using System.Linq;

namespace AbBus.DAL
{
    public class UserRepository: GenericRepository<user>, IUserRepository
    {
        private readonly AbBusEntities _abBusEntities;                                                                     
        public UserRepository(AbBusEntities dbcontext): base(dbcontext)
        {
            _abBusEntities = dbcontext;
        }
    }
}
