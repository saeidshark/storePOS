using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorePOS
{
    public static class Session
    {
        public static string LoggedInUsername = "";
        public static string LoggedInRole = "";
    }

    public static class Sessioncashier
    {
        public static string LoggedInUsername { get; set; }
        public static string LoggedInRole { get; set; }
    }


}
