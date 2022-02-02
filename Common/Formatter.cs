using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Formatter
    {
        /// <summary>
        /// Returns username based on the Windows Logon Name. 
        /// </summary>
        /// <param name="winLogonName"> Windows logon name can be formatted either as a UPN (<username>@<domain_name>) or a SPN (<domain_name>\<username>) </param>
        /// <returns> username </returns>
        public static string ParseName(string winLogonName)
        {
            string[] parts = new string[] { };

            if (winLogonName.Contains("@"))
            {
                ///UPN format
                parts = winLogonName.Split('@');
                return parts[0];
            }
            else if (winLogonName.Contains("\\"))
            {
                /// SPN format
                parts = winLogonName.Split('\\');
                return parts[1];
            }
            else
            {
                return winLogonName;
            }
        }


        public static string IzvadiUsername(string imeSertifikata)
        {
            string ime = string.Empty;
            string[] s;
            string[] p;
            if (imeSertifikata.Contains(","))
            {

                s = imeSertifikata.Split(',');
                p = s[0].Split('=');
                ime = p[1];
                return ime;

            }
            else
            {

                p = imeSertifikata.Split('=');
                ime = p[1];
                return ime;
            }
        }

        public static string IzvadiGrupe(string imeSertifikata)
        {

            if (imeSertifikata.Contains(","))
            {

                string[]  s = imeSertifikata.Split(',');
                string[]  p = s[1].Split('=');
                string ime = p[1];

                return ime;
            }
            else
            {
                return String.Empty;
            }     
        }
    }
}
