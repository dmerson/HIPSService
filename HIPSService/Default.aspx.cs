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
            var ssn = this.txtSSN.Value;
            var dob = DateTime.Parse(this.txtDOB.Value);
            var key = DateTime.Parse(this.txtKeys.Value);
            var encrypt = new EncryptWithHips(ssn, dob, FirstThree, AddValue, key, DateDifference, SpinSSN);
            var decrypt = new DecryptWithHIPS(encrypt.NewSSN, encrypt.NewDateOfBirth, key, FirstThree, DecryptDOB,
                                              DecryptSSN);
            this.spanPin.InnerHtml = encrypt.RealizedPin.ToString();
            this.spanNewDOB.InnerHtml = encrypt.NewDateOfBirth.ToShortDateString();
            this.spanStamp.InnerHtml = encrypt.Stamp.ToString();
            this.spanNewSSNs.InnerHtml = encrypt.NewSSN;
            //this.spanOldDOB.InnerHtml = DecryptDOB(encrypt.NewDateOfBirth, encrypt.RealizedPin).ToShortDateString();
            //this.spanOldSSN.InnerHtml = DecryptSSN(encrypt.NewSSN, encrypt.NewDateOfBirth, key);

        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            RunPage();
        }
    }
}