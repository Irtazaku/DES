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
            DES d = new DES("E8C9E7DD7C00A521", "B0B7B40DA9EE1A54");
            d.keygen();
            string re=d.encrypt();
            Console.WriteLine(re);
        }
    }
}
