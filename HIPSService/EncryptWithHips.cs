using System;

namespace HIPSService
{
    
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
       public string VerifySSN { get; set; }
       public DateTime VerifyDateOfBirth { get; set; }
       
        public BasicHIPS(EncryptionDirection whichWay, string ssn, DateTime dateOfBirth, DateTime key,
            Func<string, int> PinFunction,
            Func<DateTime, int, DateTime> MakeNewDateOfBirthFunction,
            DateTime Key, Func<DateTime, DateTime, int> StampFunction,
            Func<string, int, string> MakeNewSSNFunction,
            Func<DateTime, int, DateTime> GetOldDateOfBirthFunction,
            Func<string, DateTime, DateTime, string> GetOldSSNFunction)
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
                        VerifyDateOfBirth = GetOldDateOfBirthFunction(FakeDateOfBirth, RealizedPin);
                        VerifySSN = GetOldSSNFunction(FakeSSN, FakeDateOfBirth, key);
                        break;
                    }
                case EncryptionDirection.Decrypt:
                    {
                        this.FakeDateOfBirth = dateOfBirth;
                        this.FakeSSN = ssn;
                        RealizedPin = PinFunction(ssn);
                        RealDateOfBirth = GetOldDateOfBirthFunction(dateOfBirth, RealizedPin);
                        RealSSN = GetOldSSNFunction(ssn, dateOfBirth, key);
                        FakeDateOfBirth = MakeNewDateOfBirthFunction(RealDateOfBirth, RealizedPin);
                        FakeSSN = MakeNewSSNFunction(RealSSN, Stamp);
                        break;
                    }
                default:
                    {
                        throw new Exception("No direction is coded");
                    }
            }
            if (VerifyDateOfBirth != RealDateOfBirth)
            {
                throw new Exception("The date of birth function is not working");
            }
            else
            {
                if (VerifySSN != RealSSN)
                {
                    throw new Exception("The ssn function is not working");
                }
            }
       

        }
    }
}