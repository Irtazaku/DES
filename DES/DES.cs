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
        public bool[] encrypted = new bool[64];
        public bool[][] pre = new bool[17][];
        public bool[][] sk = new bool[16][];

        int[] round = {1,1,2,2,2,2,2,2,1,2,2,2,2,2,2,1};
        int[] pc1 = { 57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18, 10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36, 63, 55, 47, 39, 31, 23, 15, 7, 62, 54, 46, 38, 30, 22, 14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4 };
        int[] pc2 = { 14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23, 19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32 };
        int[] ip = { 58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4, 62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8, 57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3, 61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7 };
        int[] e = { 32, 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9, 8, 9, 10, 11, 12, 13, 12, 13, 14, 15, 16, 17, 16, 17, 18, 19, 20, 21, 20, 21, 22, 23, 24, 25, 24, 25, 26, 27, 28, 29, 28, 29, 30, 31, 32, 1 };
        int[, ,] s = { { { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 }, { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 }, { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 }, { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 } }, { { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 }, { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 }, { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 }, { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 } }, { { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 }, { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 }, { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 }, { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 } }, { { 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 }, { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 }, { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 }, { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 } }, { { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 }, { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 }, { 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 }, { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 } }, { { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 }, { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 }, { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 }, { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 } }, { { 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 }, { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 }, { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 }, { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 } }, { { 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 }, { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 }, { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 }, { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 } } };
        int[] p = { 16, 7, 20, 21, 29, 12, 28, 17, 1, 15, 23, 26, 5, 18, 31, 10, 2, 8, 24, 14, 32, 27, 3, 9, 19, 13, 30, 6, 22, 11, 4, 25 };
        int[] ip_1 = { 40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31, 38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29, 36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27, 34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17, 57, 25 };
        
        public DES(string k, string msg)
        {
            key= strToDeci(k);
            message = strToDeci(msg);
        }
        
        public void keygen ()
        {
            bool[] temp = masking(pc1, key);
            bool[][] K = new bool[16][];
            for (int r = 0; r < 16; r++)
            {
                temp = prepc2( r);
                K[r] = masking(pc2, pre[r]);
                sk[r] = K[r];
                
            }
        }
        public string encrypt()
        {
            bool[] temp = masking(ip, message);
            bool[] tempL = new bool[32];
            bool[] tempR = new bool[32];
            for (int i = 0; i < (temp.Length/2); i++)
            {
                tempR[i] = temp[(temp.Length / 2) + i];
                tempL[i] = temp[i];
            }
            for (int r = 0; r < 16; r++)
            {
                bool[] temp1 = masking(e, tempR);
                temp1 = XOR(temp1,sk[r]);
                bool[] temp2 = SBox(temp1);
                bool[] temp3 = masking(p, temp2);
                temp1 = XOR(temp3, tempL);
                //tempR = tempR;
                tempL = tempR;
                tempR = temp1;

                
                
            }

            for (int i = 0; i < tempR.Length; i++)
            {
                encrypted[i] = tempR[i];
                encrypted[i + 32] = tempL[i];
            }
            encrypted = masking(ip_1, encrypted);
            
            string result = "";
            string output = "";

            for (int i = 0; i < 65; i++)
            {
                if (i % 4 == 0&& i!=0)
                {
                    result += ToBase(output, 2, 16);
                    output = "";
                }
                try
                {
                    if (encrypted[i])
                        output += "1";
                    else
                        output += "0";
                }
                catch (Exception e)
                { }
                

            }


            return result;

        }
        public bool[] SBox(bool[] arr)
        {
            
            bool[] output = new bool[32];
            int pointer = 0;
            int pointer1 = 0;
            for (int i = 0; i < 8; i++)
            {
                string temp="";
                for (int j = 0; j < 6; j++)
                {
                    if (arr[pointer])
                    {
                        temp += "1";
                        pointer++;
                    }
                    else
                    {
                        temp += "0";
                        pointer++;
                    }
                    
                }
                int r = Convert.ToInt32((temp[0].ToString() + temp[5].ToString()), 2);
                int c = Convert.ToInt32((temp[1].ToString() + temp[2].ToString()+temp[3].ToString() + temp[4].ToString()), 2);
                string binary = Convert.ToString(s[i,r,c], 2);
                if (binary.ToString().Length < 2)
                    binary = "000" + binary;
                else if (binary.ToString().Length < 3)
                    binary = "00" + binary;
                else if (binary.ToString().Length < 4)
                    binary = "0" + binary;
                
                for (int j = 0; j < 4; j++)
                {
                    if (binary[j] == '0')
                        output[pointer1++] = false;
                    else
                        output[pointer1++] = true;
                }
            }
            return output; 
        }
        public bool[] XOR(bool[] arr1, bool[] arr2)
        {
            if (arr1.Length == arr2.Length)
            {
                bool[] temp = new bool[arr2.Length];
                for (int i = 0; i < arr2.Length; i++)
                {
                    if (arr2[i] == arr1[i])
                        temp[i] = false;
                    else
                        temp[i] = true;
                }
                return temp;
            }
            else
                return new bool[0];
        }

        public bool[] masking(int[] arr, bool[] arr2)
        { 
            bool[] temp = new bool[arr.Length];
            
            for (int i = 0; i < arr.Length; i++)
            {
                temp[i] = arr2[arr[i] - 1];
            }
            pre[0] = temp;
                return temp;
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
            
            
            for (int i = 0; i < 28; i++)
            {
                pre[r][i] = tempL[(i + round[r]) % 28];
                pre[r][i + 28] = tempR[((i + round[r]) % 28)];

            }
            pre[r + 1] = pre[r];
            return pre[r];
        }
       
        private bool[] strToDeci(string k)
        {
            bool[] key = new bool[64];
            int pointer = 0;
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
            return key;
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
