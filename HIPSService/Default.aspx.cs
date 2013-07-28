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
            return dateToUser.AddDays(daysToAdd) ;
        }

        public int DateDifference(DateTime orignal, DateTime newDate)
        {
            return int.Parse((orignal - newDate).TotalDays.ToString());
        }

        public string SpinSSN(string ssn, int spinNumber)
        {
            return (int.Parse(ssn) + spinNumber).ToString();
        }

        public DateTime DecryptDOB(DateTime newDOB, int pin)
        {
            return newDOB.AddDays(-pin);
        }

        public string DecryptSSN(string newSSN, DateTime newDOB, DateTime Stamp)
        {
            double toSubtractFromSSN = (newDOB - Stamp).TotalDays;
            return (int.Parse(newSSN) - toSubtractFromSSN).ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RunPage();
        }

        private void RunPage()
        {
            string ssn = txtSSN.Value;
            DateTime dob = DateTime.Parse(txtDOB.Value);
            DateTime key = DateTime.Parse(txtKeys.Value);
           
            var basic = new BasicHIPS(BasicHIPS.EncryptionDirection.Encrypt, ssn, dob, key, FirstThree, AddValue, key,
                                      DateDifference, SpinSSN, DecryptDOB,
                                              DecryptSSN);
            this.spanPin.InnerHtml = basic.RealizedPin.ToString();
            this.spanStamp.InnerHtml = basic.Stamp.ToString();
            this.spanFullObjectDOB.InnerHtml = basic.FakeDateOfBirth.ToShortDateString();
            this.spanFullObjectSSN.InnerHtml = basic.FakeSSN;
            this.spanFullObjectRealSSN.InnerHtml = basic.RealSSN;
            this.spanFullObjectRealDOB.InnerHtml = basic.RealDateOfBirth.ToShortDateString();
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            RunPage();
        }
    }
}