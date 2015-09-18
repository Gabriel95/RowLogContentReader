using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _21211110_Proyecto2_TBD2.Parsers;
using _21211110_Proyecto2_TBD2;

namespace UnitTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void IntParserBigNumberTest()
        {
            string value = "FEFFFF7F";
            int expected = 2147483646;
            RowLogContentsParser parser = new RowLogContentsParser();
            int actual = Hexconveters.ParseInt(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BigIntParserBigNumberTest()
        {
            string value = "FEFFFF7F00000000";
            long expected = 2147483646;
            RowLogContentsParser parser = new RowLogContentsParser();
            long actual = Hexconveters.ParseBigInt(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TinyIntParserTest()
        {
            string value = "01";
            short expected = 1;
            RowLogContentsParser parser = new RowLogContentsParser();
            short actual = Hexconveters.ParseTinyInt(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DecimalParserBigNumberTest()
        {
            string value = "01FEFFFF7F00000000";
            decimal expected = 2147483646;
            RowLogContentsParser parser = new RowLogContentsParser();
            decimal actual = Hexconveters.ParseDecimal(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FloatParserTest()
        {
            string value = "3333333333D35C40";
            double expected = 115.3;
            RowLogContentsParser parser = new RowLogContentsParser();
            double actual = Hexconveters.ParseFloat(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RealParserTest()
        {
            string value = "0000004F";
            Single expected = 2147483646;
            RowLogContentsParser parser = new RowLogContentsParser();
            Single actual = Hexconveters.ParseReal(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NumericParserTest()
        {
            string value = "01FEFFFF7F00000000";
            decimal expected = 2147483646;
            RowLogContentsParser parser = new RowLogContentsParser();
            decimal actual = Hexconveters.ParseNumeric(value); 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MoneyParserTest()
        {
            string value = "E0B1FFFF87130000";
            decimal expected = 2147483646;
            RowLogContentsParser parser = new RowLogContentsParser();
            decimal actual = Hexconveters.ParseMoney(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BinaryParserTest()
        {
            string value = "8d7e6a5d4b3e2e1f";
            string expected = "8d7e6a5d4b3e2e1f".ToUpper();
            RowLogContentsParser parser = new RowLogContentsParser();
            string actual = BitConverter.ToString(Hexconveters.ParseBinary(value)).Replace("-", string.Empty);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BitParserTest()
        {
            string value = "01";
            bool expected = true;
            RowLogContentsParser parser = new RowLogContentsParser();
            bool actual = Hexconveters.ParseBit(value);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CharParserTest()
        {
            string value = "48656C6C6F2C204920616D204A6F736570682120";
            string expected = "Hello, I am Joseph!";
            RowLogContentsParser parser = new RowLogContentsParser();
            string actual = Hexconveters.ParseChar(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void VarCharParserTest()
        {
            string value = "48656C6C6F2C204920616D204A6F7365706821";
            string expected = "Hello, I am Joseph!";
            RowLogContentsParser parser = new RowLogContentsParser();
            string actual = Hexconveters.ParseVarChar(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TransactionLogTest()
        {
            var expected = new List<TransactionLogItem>();
            var tlparser = new TransactionLogParser();
            var hvparser = new RowLogContentsParser();
            var values = tlparser.GetMetadata("Testing","testintable2");
            for (int i = 0, x = 0; i < values.Count - x; i++)
            {
                if (values.ElementAt(i).Type == XType.VarChar)
                {
                    var value = values.ElementAt(i);
                    values.RemoveAt(i);
                    values.Add(value);
                    i = -1;
                    x++;
                }
            }
            var values2 = tlparser.GetRowLogContents("Testing","testintable2");
            var ceja = values2;
            var parsedLogs = hvparser.ParseRowLogContents(values2.ElementAt(0), values);
            Assert.AreEqual(expected, values2);
        }
    }
}
