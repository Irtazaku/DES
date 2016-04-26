using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    class DES
    {

        public bool[] key, message;
        public bool[][] pre = new bool[17][];
        public bool[][] sk = new bool[16][];
        int[] round = {1,1,2,2,2,2,2,2,1,2,2,2,2,2,2,1};
        int pointer = 0;
        public DES(string k, string msg)
        {
            strToDeci(k);
        }

        public bool[] prepc2(int r)
        {
            bool[] tempL = new bool[28];
            bool[] tempR = new bool[28];
            for (int i= 0;i<28;i++)
            {
                tempR[i] = pre[r][28+i];
                tempL[i] = pre[r][i];
            }
            
            int[] roundtable = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
            for (int i = 0; i < 28; i++)
            {
                pre[r][i] = tempL[(i + roundtable[r]) % 28];
                pre[r][i + 28] = tempR[((i + roundtable[r]) % 28)];

            }
            pre[r + 1] = pre[r];
            return pre[r];
        }
        public bool[] pc1table()
        {
            bool[] temp = new bool[56];
            int[] pc1 = { 57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18, 10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36, 63, 55, 47, 39, 31, 23, 15, 7, 62, 54, 46, 38, 30, 22, 14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4 };
            for (int i = 0; i < 56; i++)
            {
                temp[i] = key[pc1[i]-1];
            }
            pre[0] = temp;
                return temp;
        }
        public bool[] pc2table(int r)
        {
            bool[] temp=this.pre[r];
            sk[r] = new bool[48];
            int[] pc2 = { 14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23, 19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32 };
            for (int i = 0; i < 48; i++)
            {
                sk[r][i] = temp[pc2[i] - 1];
            }
            return sk[r];
        }
        private void strToDeci(string k)
        {
            key = new bool[64];
            for (int i = 0; i < 16; i++)
            { 
                int temp = Convert.ToInt32(ToBase(k[i].ToString(), 16, 2));
                string outpt = "";
                if (temp.ToString().Length < 2)
                    outpt = "000" + temp;
                else if (temp.ToString().Length < 3)
                    outpt = "00" + temp;
                else if (temp.ToString().Length < 4)
                    outpt = "0" + temp;
                else
                    outpt = temp.ToString();
                for (int j = 0; j < 4;j++ )
                {
                    if (outpt[j] == '0')
                    {
                        key[pointer++] = false;
                    }
                    else {
                        key[pointer++] = true;
                    }
                }
            }
           // return new bool [33];
        }
        public static string ToBase(string number, int start_base, int target_base)
        {

            long base10 = ToBase10(number, start_base);
            string rtn = FromBase10(base10, target_base);
            return rtn;

        }

        public static long ToBase10(string number, int start_base)
        {

            if (start_base < 2 || start_base > 36) return 0;
            if (start_base == 10) return Convert.ToInt64(number);

            char[] chrs = number.ToCharArray();
            long m = chrs.Length - 1;
            long n = start_base;
            long x;
            long rtn = 0;

            foreach (char c in chrs)
            {

                if (char.IsNumber(c))
                    x = long.Parse(c.ToString());
                else
                    x = Convert.ToInt64(c) - 55;

                rtn += x * (Convert.ToInt64(Math.Pow(n, m)));

                m--;

            }

            return rtn;

        }

        public static string FromBase10(long number, int target_base)
        {

            if (target_base < 2 || target_base > 36) return "";
            if (target_base == 10) return number.ToString();

            int n = target_base;
            long q = number;
            long r;
            string rtn = "";

            while (q >= n)
            {

                r = q % n;
                q = q / n;

                if (r < 10)
                    rtn = r.ToString() + rtn;
                else
                    rtn = Convert.ToChar(r + 55).ToString() + rtn;

            }

            if (q < 10)
                rtn = q.ToString() + rtn;
            else
                rtn = Convert.ToChar(q + 55).ToString() + rtn;

            return rtn;

        } 

    }
}
