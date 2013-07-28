using System;

namespace HIPSService
{
    public class EncryptWithHips
    {
        public EncryptWithHips(string ssn, DateTime dateOfBirth, 
            Func<string, int> PinFunction, 
            Func<DateTime, int, DateTime> MakeNewDateOfBirthFunction, 
            DateTime Key, Func<DateTime, DateTime, int> StampFunction,
            Func<string, int, string> MakeNewSSNFunction)
        {
            RealizedPin = PinFunction(ssn );
            NewDateOfBirth = MakeNewDateOfBirthFunction(dateOfBirth, RealizedPin );
            Stamp = StampFunction(NewDateOfBirth, Key);
            NewSSN = MakeNewSSNFunction(ssn, Stamp);
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
    public class BasicHIPS
    {
       public enum EncryptionDirection
        {
            Encrypt,
            Decrypt
        }

       public EncryptionDirection CurrentDirection { get; set; }
       public string RealSSN { get; set; }
       public DateTime RealDateOfBirth { get; set; }
       public string FakeSSN { get; set; }
       public DateTime FakeDateOfBirth { get; set; }
       public DateTime Key { get; set; }
       public int RealizedPin { get; set; }
       public int Stamp { get; set; }
       
        public BasicHIPS(EncryptionDirection whichWay, string ssn, DateTime dateOfBirth, DateTime key,
            Func<string, int> PinFunction,
            Func<DateTime, int, DateTime> MakeNewDateOfBirthFunction,
            DateTime Key, Func<DateTime, DateTime, int> StampFunction,
            Func<string, int, string> MakeNewSSNFunction)
        {
            CurrentDirection = whichWay;
            Key = key;
            switch (whichWay)
            {
                case EncryptionDirection.Encrypt:
                    {
                        this.RealSSN = ssn;
                        this.RealDateOfBirth = dateOfBirth;
                        RealizedPin = PinFunction(ssn);
                        FakeDateOfBirth = MakeNewDateOfBirthFunction(dateOfBirth, RealizedPin);
                        Stamp = StampFunction(FakeDateOfBirth, Key);
                        FakeSSN = MakeNewSSNFunction(ssn, Stamp);
                        break;
                    }
                case EncryptionDirection.Decrypt:
                    {
                        this.FakeDateOfBirth = dateOfBirth;
                        this.FakeSSN = ssn;
                        break;
                    }
                default:
                    {
                        throw new Exception("No direction is coded");
                    }
            }

        }
    }
}