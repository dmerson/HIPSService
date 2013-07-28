using System;

namespace HIPSService
{
    public class EncryptWithHips
    {
        public EncryptWithHips(string ssn, DateTime dateOfBirth, Func<string, int> PinFunction, Func<DateTime, int, DateTime> MakeNewSSNFunction, DateTime Key, Func<DateTime, DateTime, int> StampFunction, Func<string, int, string> SpinFunction)
        {
            RealizedPin = PinFunction(ssn );
            NewDateOfBirth = MakeNewSSNFunction(dateOfBirth, RealizedPin );
            Stamp = StampFunction(NewDateOfBirth, Key);
            NewSSN = SpinFunction(ssn, Stamp);
        }

        public int RealizedPin { get; set; }
        public DateTime NewDateOfBirth { get; set; }
        public int Stamp { get; set; }
        public string NewSSN { get; set; }

        //private int GetRealizedKey(string ssn, Func<string, int> KeyFunction)
        //{
        //    return KeyFunction(ssn);
        //}

        //private DateTime GetNewDateFunc(DateTime realdate, int changeValue,
        //                                Func<DateTime, int, DateTime> NewDateFunction)
        //{
        //    return NewDateFunction(realdate, changeValue);
        //}
    }


    public class DecryptWithHIPS
    {
        public int RealizedPin { get; set; }
        public DateTime RealDateOfBirth { get; set; }
        public int Stamp { get; set; }
        public string RealSSN { get; set; }
        private int GetRealizedKey(string ssn, Func<string, int> KeyFunction)
        {
            return KeyFunction(ssn);
        }
        public DecryptWithHIPS(string encryptedSSN, DateTime encryptedDOB,Func<string, int> PinFunction )
        {
            RealizedPin = PinFunction(encryptedSSN);
        }
    }
}