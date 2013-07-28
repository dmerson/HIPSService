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
            Func<string, int> pinFunction,
            Func<DateTime, int, DateTime> makeNewDateOfBirthFunction,
            DateTime Key, Func<DateTime, DateTime, int> stampFunction,
            Func<string, int, string> makeNewSsnFunction,
            Func<DateTime, int, DateTime> getOldDateOfBirthFunction,
            Func<string, DateTime, DateTime, string> getOldSsnFunction)
        {
            CurrentDirection = whichWay;
            Key = key;
            switch (whichWay)
            {
                case EncryptionDirection.Encrypt:
                    {
                        this.RealSSN = ssn;
                        this.RealDateOfBirth = dateOfBirth;
                        RealizedPin = pinFunction(ssn);
                        FakeDateOfBirth = makeNewDateOfBirthFunction(dateOfBirth, RealizedPin);
                        Stamp = stampFunction(FakeDateOfBirth, Key);
                        FakeSSN = makeNewSsnFunction(ssn, Stamp);
                        VerifyDateOfBirth = getOldDateOfBirthFunction(FakeDateOfBirth, RealizedPin);
                        VerifySSN = getOldSsnFunction(FakeSSN, FakeDateOfBirth, key);
                        break;
                    }
                case EncryptionDirection.Decrypt:
                    {
                        this.FakeDateOfBirth = dateOfBirth;
                        this.FakeSSN = ssn;
                        RealizedPin = pinFunction(ssn);
                        RealDateOfBirth = getOldDateOfBirthFunction(dateOfBirth, RealizedPin);
                        RealSSN = getOldSsnFunction(ssn, dateOfBirth, key);
                        FakeDateOfBirth = makeNewDateOfBirthFunction(RealDateOfBirth, RealizedPin);
                        FakeSSN = makeNewSsnFunction(RealSSN, Stamp);
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