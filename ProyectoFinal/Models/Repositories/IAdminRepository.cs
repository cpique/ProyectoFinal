using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models.Repositories
{
    public interface IAdminRepository : IDisposable
    {
        IEnumerable<Admin> GetAdmins();
        Admin GetAdminByID(int adminID);
        void InsertAdmin(Admin admin);
        void DeleteAdmin(int adminID);
        void UpdateAdmin(Admin admin);
        void Save();
    }
}
