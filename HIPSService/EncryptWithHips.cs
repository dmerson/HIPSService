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
       /// <summary>
       /// Basic Hips is a SSN/ DOB object. 
       /// </summary>
       /// <param name="whichWay">Encrypt it or Decrypt it</param>
       /// <param name="ssn">the ssn to encrypt or decrypt</param>
       /// <param name="dateOfBirth">the date of birth to encrypt or decrypt</param>
       /// <param name="key">this is a date which will used to compare the new dob to get a number to change the ssn</param>
       /// <param name="pinFunction">this is a function takes the ssn and create a new number that will change the dob.It returns an int.</param>
       /// <param name="makeNewDateOfBirthFunction">this is the function that takes a date, int and returns a new date. It takes the real dob and the pin and creates a new dob. It gets decrypted by getOldDateOfBirthFunction</param>
   /// <param name="stampFunction">the stamp function takes 2 dates and compares them to get a int. This int will change the ssn.</param>
       /// <param name="makeNewSsnFunction">this function takes a ssn and the stamp (int) and creates the new SSN function. It is decrypted by getOldSsnFunction</param>
       /// <param name="getOldDateOfBirthFunction">this function takes the encrypted dob and the realized pin and gets the old dob. It is encrypted by makeNewDateOfBirthFunction</param>
       /// <param name="getOldSsnFunction">this function takes the encrypted ssn, the encrypted dob and the key and gets the real ssn. It is encrypted by makeNewSsnFunction</param>
        public BasicHIPS(EncryptionDirection whichWay, string ssn, DateTime dateOfBirth, DateTime key,
            Func<string, int> pinFunction, 
            Func<DateTime, int, DateTime> makeNewDateOfBirthFunction,
            Func<DateTime, DateTime, int> stampFunction,
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
                        FakeDateOfBirth = makeNewDateOfBirthFunction(RealDateOfBirth, RealizedPin);
                        Stamp = stampFunction(FakeDateOfBirth, Key);
                        FakeSSN = makeNewSsnFunction(RealSSN, Stamp);
                        VerifyDateOfBirth = getOldDateOfBirthFunction(FakeDateOfBirth, RealizedPin);
                        VerifySSN = getOldSsnFunction(FakeSSN, FakeDateOfBirth, key);
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
                        break;
                    }
                case EncryptionDirection.Decrypt:
                    {
                        this.FakeDateOfBirth = dateOfBirth;
                        this.FakeSSN = ssn;
                        RealizedPin = pinFunction(ssn);
                        RealDateOfBirth = getOldDateOfBirthFunction(FakeDateOfBirth, RealizedPin);
                        RealSSN = getOldSsnFunction(FakeSSN, FakeDateOfBirth, key);
                        VerifyDateOfBirth = makeNewDateOfBirthFunction(RealDateOfBirth, RealizedPin);
                        Stamp = stampFunction(FakeDateOfBirth, Key);
                        VerifySSN = makeNewSsnFunction(RealSSN, Stamp);
                        if (VerifyDateOfBirth != FakeDateOfBirth)
                        {
                            throw new Exception("The date of birth function is not working");
                        }
                        else
                        {
                            if (VerifySSN != FakeSSN)
                            {
                                throw new Exception("The ssn function is not working");
                            }
                        }
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