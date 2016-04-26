using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    
    
    class Program
    {
        static void Main(string[] args)
        {
            DES d = new DES("133457799BBCDFF1", "0123456789ABCDEF");
            d.keygen();
            d.encrypt();
        }
    }
}
