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
        public DecryptWithHIPS(string encryptedSSN, DateTime encryptedDOB, DateTime key, Func<string, int> PinFunction, Func<DateTime,int,DateTime> GetOldDateOfBirthFunction, Func<string,DateTime, DateTime,string> GetOldSSNFunction   )
        {
            RealizedPin = PinFunction(encryptedSSN);
            RealDateOfBirth = GetOldDateOfBirthFunction(encryptedDOB, RealizedPin);
            RealSSN = GetOldSSNFunction(encryptedSSN, encryptedDOB, key);


        }
    }
}