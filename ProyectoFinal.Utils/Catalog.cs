using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Utils
{
    public class Catalog
    {
        public enum Status { Active = 1, Inactive }
        public enum Roles { Admin = 1, Instructor, Client }
        public enum ItemStatus { Ok = 1, Deteriorated, Broken }
        public enum LevelRoutine { Begginer = 1, Medium, Advanced, Expert}

    }
}
