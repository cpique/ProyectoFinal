using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models.Repositories
{
    public interface IMachineRepository : IDisposable
    {
        IEnumerable<Machine> GetMachines();
        Machine GetMachineByID(int machineID);
        void InsertMachine(Machine machine);
        void DeleteMachine(int machineID);
        void UpdateMachine(Machine machine);
        void Save();
    }
}
