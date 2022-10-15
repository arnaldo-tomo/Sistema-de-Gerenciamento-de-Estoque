using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemCsharp
{
    class ManagerDetails
    {
        private static string Mname;

        public void setMname(string mname)
        {
            Mname = mname;
        }
        public string getMname()
        {
            return Mname;
        }
    }
}
