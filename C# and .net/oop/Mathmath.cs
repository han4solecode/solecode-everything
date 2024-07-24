using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    public class Mathmath
    {

        public static int SumAll(params int[] num)
        {
            int sum = 0;
            for (int i = 0; i < num.Length; i++) {
               sum += num[i];
            }

            return sum;
        }
    }
}
