using System;
using System.Web.UI;

namespace HIPSService
{
    public partial class Default : Page
    {
       
        public int PinFunction(string SSN) //you can change this and leave everything else.
        {
            
            return int.Parse(SSN.Substring(0, 1));
        }

        public DateTime MakeNewDobFunction(DateTime dateToUser, int daysToAdd)
        {
            return dateToUser.AddDays(daysToAdd) ;
        }

        public int StampFunction(DateTime orignal, DateTime newDate)
        {
            return int.Parse((orignal - newDate).TotalDays.ToString());
        }

        public string MakeNewSsnFunction(string ssn, int spinNumber)
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
           
            var basic = new BasicHIPS(BasicHIPS.EncryptionDirection.Decrypt, ssn, dob, key, PinFunction,
                                       MakeNewDobFunction, 
                                      StampFunction,
                                      MakeNewSsnFunction,
                                      DecryptDOB,
                                      DecryptSSN);
            this.spanPin.InnerHtml = basic.RealizedPin.ToString();
            this.spanStamp.InnerHtml = basic.Stamp.ToString();
            this.spanFullObjectDOB.InnerHtml = basic.FakeDateOfBirth.ToShortDateString();
            this.spanFullObjectSSN.InnerHtml = basic.FakeSSN;
            this.spanFullObjectRealSSN.InnerHtml = basic.RealSSN;
            this.spanFullObjectRealDOB.InnerHtml = basic.RealDateOfBirth.ToShortDateString();
            this.spanVerifyDOB.InnerHtml = basic.VerifyDateOfBirth.ToShortDateString();
            this.spanVerifySSN.InnerHtml = basic.VerifySSN;
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            RunPage();
        }
    }
}