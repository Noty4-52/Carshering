using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carshering
{
    internal class Helper
    {
        public static CarsheringEntities1 GetContext()
        {
            return new CarsheringEntities1();
        }
    }
}
