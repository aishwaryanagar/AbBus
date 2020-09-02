using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbBus.DAL
{
    interface IUnitOfWork : IDisposable
    {
        int Save();
        UserRepository Users { get; }
    }
}
