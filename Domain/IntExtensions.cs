using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class IntExtensions
    {
        public static bool IsBetween (this int number, int min,int max)
        {
            return number >=min && number <= max;
        }
    }
}
