using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.Business.Helpers
{
    public class UserNameHelper
    {
        public static string Transform(string name)
        {
            string userName = name.Replace(' ', '_');

            return userName;
        }
    }
}
