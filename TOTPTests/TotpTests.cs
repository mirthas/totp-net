using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOTP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TOTP.Tests
{
    [TestClass()]
    public class TotpTests
    {
        

        [TestMethod()]
        public void FromBase32StringTest()
        {
            byte[] data = { 0xec, 0x02, 0x9b, 0x80, 0x27, 0x9d, 0x19, 0x25, 0x86, 0x03, 0x97, 0xb5, 0x65, 0x4c, 0x8c, 0x6d, 0x0b, 0xb8, 0x2e, 0x63 };
            string database32 = "5QBJXABHTUMSLBQDS62WKTEMNUF3QLTD";
            Totp dut = new Totp(database32);
            CollectionAssert.AreEqual(data, dut.key);
        }

        [TestMethod()]
        public void getCodeTest()
        {
            string secret = "GEZDGNBVGY3TQOJQGEZDGNBVGY3TQOJQ";
            Totp dut = new Totp(secret,30,8);
            Assert.AreEqual(94287082, dut.getCode(59));
            Assert.AreEqual(7081804, dut.getCode(1111111109));
            Assert.AreEqual(14050471, dut.getCode(1111111111));
            Assert.AreEqual(89005924, dut.getCode(1234567890));
            Assert.AreEqual(69279037, dut.getCode(2000000000));
            Assert.AreEqual(65353130, dut.getCode(20000000000));
        }

        [TestMethod()]
        public void getCodeStringTest()
        {
            string secret = "GEZDGNBVGY3TQOJQGEZDGNBVGY3TQOJQ";
            Totp dut = new Totp(secret, 30, 8);
            Assert.AreEqual("94287082", dut.getCodeString(59));
            Assert.AreEqual("07081804", dut.getCodeString(1111111109));
            Assert.AreEqual("14050471", dut.getCodeString(1111111111));
            Assert.AreEqual("89005924", dut.getCodeString(1234567890));
            Assert.AreEqual("69279037", dut.getCodeString(2000000000));
            Assert.AreEqual("65353130", dut.getCodeString(20000000000));

            dut = new Totp(secret, 30, 6);
            Assert.AreEqual("287082", dut.getCodeString(59));
            Assert.AreEqual("081804", dut.getCodeString(1111111109));
            Assert.AreEqual("050471", dut.getCodeString(1111111111));
            Assert.AreEqual("005924", dut.getCodeString(1234567890));
            Assert.AreEqual("279037", dut.getCodeString(2000000000));
            Assert.AreEqual("353130", dut.getCodeString(20000000000));
        }
    }
}
