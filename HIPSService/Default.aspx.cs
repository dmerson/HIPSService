using System;
using System.Web.UI;

namespace HIPSService
{
    public partial class Default : Page
    {
        public int FirstThree(string SSN)
        {
            return int.Parse(SSN.Substring(0, 3));
        }

        public DateTime AddValue(DateTime dateToUser, int daysToAdd)
        {
            return dateToUser.AddDays(daysToAdd);
        }

        public int DateDifference(DateTime orignal, DateTime newDate)
        {
            return int.Parse((orignal -newDate).TotalDays.ToString());
               
        }
        public string SpinSSN(string ssn, int spinNumber)
        {
            return (int.Parse(ssn) + spinNumber).ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var ssn = "216821211";
            var encrypt = new EncryptWithHips(ssn, new DateTime(1991, 1, 2), FirstThree, AddValue, new DateTime(1991, 1, 1), DateDifference, SpinSSN);
            Response.Write(ssn + "<br>");
            Response.Write(encrypt.RealizedPin +"<br>");
            Response.Write(encrypt.NewDateOfBirth + "<br>");
            Response.Write(encrypt.Stamp + "<br>");
            
            Response.Write(encrypt.NewSSN + "<br>");
        }
    }
}