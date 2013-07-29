using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;

namespace HIPSService.Tests
{
    [TestFixture]
    public class BasicHipsTest
    {
        #region PassedFunctions

        
        public int PinFunction(string SSN) //you can change this and leave everything else.
        {

            return int.Parse(SSN.Substring(0, 1));
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
        #endregion
        #region SetUp / TearDown

        private BasicHIPS encrypt;
        private string ssn;
        private DateTime dob;
        private DateTime key;
        [SetUp]
        public void Init()
        {
             ssn = "123456789";
             dob = new DateTime(1990,1,1);
             key = new DateTime(1991,1,2);

             encrypt = new BasicHIPS(BasicHIPS.EncryptionDirection.Encrypt, ssn, dob, key, PinFunction,
                                       MakeNewDobFunction,
                                      StampFunction,
                                      MakeNewSsnFunction,
                                      DecryptDOB,
                                      DecryptSSN);
            
        }

        [TearDown]
        public void Dispose()
        { }

        #endregion

        #region Tests

        [Test]
        public void Test()
        {
            Assert.IsNotNull(encrypt);
            Assert.AreEqual(ssn, encrypt.RealSSN);
            Assert.AreEqual(dob, encrypt.RealDateOfBirth);
            Assert.AreEqual("123456424", encrypt.FakeSSN);
            
        }

        #endregion
    }
}
