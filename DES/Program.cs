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
            DES d = new DES("133457799BBCDFF1", "");
            bool[] temp = d.pc1table();
            bool[][] K = new bool[16][];
            for (int r = 0; r < 16; r++)
            {
                temp = d.prepc2( r);
                K[r] = d.pc2table(r);
                for (int i = 0; i < 48; i++)
                {
                    if (K[r][i])
                        Console.Write("1");
                    else
                        Console.Write("0");
                }
                Console.WriteLine();
            }
        }
    }
}
