using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace AdventOfCode._2015
{

   
    internal class Day4
    {
        public static string ConvertStringtoMD5(string strword)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(strword);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
        public static int Day4A() {
            foreach(int i in Enumerable.Range(1, 1000000000)){ 
                String str = "iwrupvqb" + i;
                var output = ConvertStringtoMD5(str);
                if(output.StartsWith("00000")){
                    return i;
                }
            }
            return 0;
        }

        public static int Day4B()
        {
            foreach (int i in Enumerable.Range(1, 1000000000))
            {
                String str = "iwrupvqb" + i;
                var output = ConvertStringtoMD5(str);
                if (output.StartsWith("000000"))
                {
                    return i;
                }
            }
            return 0;
        }
    }
}
