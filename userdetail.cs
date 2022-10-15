using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemCsharp
{
    
    class userdetail
    {
        private static string uname;

        public void setUname(string name)
        {
            uname = name;
        }
        public string getUname()
        {
            return uname;
        }
    }
}
