using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace HIPSService
{
    /// <summary>
    /// Summary description for BasicHips
    /// </summary>
    [WebService(Namespace = "http://hips.arizona.edu/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class BasicHips : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ReturnEncrypted Encrypt(string ssn, DateTime dob)
        {
            var key = new DateTime(1990, 1, 1);
            var basic = new BasicHIPS(BasicHIPS.EncryptionDirection.Encrypt, ssn, dob, key, PinFunction,
                                    MakeNewDobFunction,
                                   StampFunction,
                                   MakeNewSsnFunction,
                                   DecryptDOB,
                                   DecryptSSN);
            return new ReturnEncrypted(basic.FakeSSN, basic.FakeDateOfBirth);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ReturnEncrypted Decrypt(string ssn, DateTime dob)
        {
            var key = new DateTime(1990, 1, 1);
            var basic = new BasicHIPS(BasicHIPS.EncryptionDirection.Decrypt, ssn, dob, key, 
                                    PinFunction,
                                    MakeNewDobFunction,
                                   StampFunction,
                                   MakeNewSsnFunction,
                                   DecryptDOB,
                                   DecryptSSN);
            
            return new ReturnEncrypted(basic.RealSSN, basic.RealDateOfBirth);
           
        }
        public int PinFunction(string SSN) //you can change this and leave everything else.
        {
            return int.Parse(SSN.Substring(0, 3));
        }

        public DateTime MakeNewDobFunction(DateTime dateToUser, int daysToAdd)
        {
            return dateToUser.AddDays(daysToAdd);
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
    }
    public class ReturnEncrypted
    {
        public string SSN { get; set; }
        public string DOB { get; set; }

        public ReturnEncrypted(string ssn, DateTime dob)
        {
            SSN = ssn;
            DOB = dob.ToShortDateString();
        }

        public ReturnEncrypted()
        {
            
        }
    }
}
