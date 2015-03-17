using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TOTP;
namespace TOTPTest
{
    [TestClass]
    public class TotpTest
    {
        [TestMethod]
        public void FromBase32StringTest()
        {
            
            byte[] data = { 0xec, 0x02, 0x9b, 0x80, 0x27, 0x9d, 0x19, 0x25, 0x86, 0x03, 0x97, 0xb5, 0x65, 0x4c, 0x8c, 0x6d, 0x0b, 0xb8, 0x2e, 0x63 };
            string database32 = "5QBJXABHTUMSLBQDS62WKTEMNUF3QLTD";
            Totp dut = new Totp();
            CollectionAssert.AreEqual(dut.FromBase32String(database32), data);
           
            
        }
    }
}
