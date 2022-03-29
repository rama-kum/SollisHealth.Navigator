using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Helper
{
    public class Validations
    {

        public bool ValidateDateTime(DateTime dateTime_value)
        {
            DateTime dt = DateTime.Parse(dateTime_value.ToString());
            string timevalue = dt.ToString("HH:mm:ss");
            if (timevalue != "00:00:00")
                return true;
            else
                return false;

        }
        public bool ValidateEmail(string emailvalue)
        {            
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(emailvalue);
            if (match.Success)
                return true;
            else
                return false;
        }

    }
}
