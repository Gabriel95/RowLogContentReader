using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace _21211110_Proyecto2_TBD2.Parsers
{
    public class Hexconveters
    {
        public static string Parse(string input, XType elementType)
        {
            switch (elementType)
            {
                case XType.Int:
                    return "" + ParseInt(input);
                case XType.BigInt:
                    return "" + ParseBigInt(input);
                case XType.TinyInt:
                    return "" + ParseTinyInt(input);
                case XType.Decimal:
                    return "" + ParseDecimal(input);
                case XType.Money:
                    return "" + ParseMoney(input);
                case XType.Float:
                    return "" + ParseFloat(input);
                case XType.Real:
                    return "" + ParseReal(input);
                case XType.Numeric:
                    return "" + ParseNumeric(input);
                case XType.Bit:
                    return "" + ParseBit(input);
                case XType.Binary:
                    return "" + ParseBinary(input);
                case XType.Char:
                    return ParseChar(input);
                case XType.VarChar:
                    return ParseVarChar(input);
                default:
                    return null;
            }
        }

        public static Int32 ParseInt(string input)
        {
            var stringlist = new List<string>();
            for (int i = 0; i < 8; i += 2)
            {
                stringlist.Add(input.Substring(i, 2));
            }
            stringlist.Reverse();
            string bigEndian = String.Join("", stringlist);
            
            return Int32.Parse(bigEndian, NumberStyles.HexNumber); ;
        }

        public static Int16 ParseSmallInt(string input)
        {
            var stringlist = new List<string>();
            for (int i = 0; i < 4; i += 2)
            {
                stringlist.Add(input.Substring(i, 2));
            }
            stringlist.Reverse();
            string bigEndian = String.Join("", stringlist);

            return Int16.Parse(bigEndian, NumberStyles.HexNumber); ;
        }

        public static Int64 ParseBigInt(string input)
        {
            var stringlist = new List<string>();
            for (int i = 0; i < 16; i += 2)
            {
                stringlist.Add(input.Substring(i, 2));
            }
            stringlist.Reverse();
            string bigEndian = String.Join("", stringlist);
            return Int64.Parse(bigEndian, NumberStyles.HexNumber);
        }

        public static byte ParseTinyInt(string input)
        {
            return Byte.Parse(input, NumberStyles.HexNumber);
        }

        public static decimal ParseDecimal(string input)
        {
            var stringlist = new List<string>();
            for (int i = 0; i < input.Length; i += 2)
            {
                stringlist.Add(input.Substring(i, 2));
            }
            stringlist.Reverse();
            var bytes = new byte[input.Length / 2];
            for (int i = 0; i < stringlist.Count; i++)
            {
                bytes[i] = ParseTinyInt(stringlist.ElementAt(i));
            }
            return BitConverter.ToUInt64(bytes, 0);
        }

        public static decimal ParseMoney(string input)
        {
            throw new NotImplementedException();
        }

        public static double ParseFloat(string input)
        {
            return BitConverter.Int64BitsToDouble(ParseBigInt(input));
        }

        public static Single ParseReal(string input) //float
        {
            var stringlist = new List<string>();
            for (int i = 0; i < input.Length; i += 2)
            {
                stringlist.Add(input.Substring(i, 2));
            }
            stringlist.Reverse();
            var bytes = new byte[input.Length / 2];
            for (int i = 0; i < stringlist.Count; i++)
            {
                bytes[i] = ParseTinyInt(stringlist.ElementAt(i));
            }
            return BitConverter.ToSingle(bytes, 0);
        }

        public static decimal ParseNumeric(string input)
        {
            return ParseDecimal(input);
        }

        public static bool ParseBit(string input)
        {
            return input == "01";
            //return Boolean.Parse(Int16.Parse(input, NumberStyles.AllowHexSpecifier).ToString(CultureInfo.InvariantCulture));
        }

        public static byte[] ParseBinary(string input)
        {
            var bytesList = new List<byte>();
            for (int i = 0; i < input.Length; i += 2)
            {
                bytesList.Add(ParseTinyInt(input.Substring(i, 2)));
            }
            bytesList.Reverse();
            int j = 0;
            while (bytesList.ElementAt(j) == 0)
            {
                bytesList.RemoveAt(j);
                j++;
            }
            bytesList.Reverse();
            return bytesList.ToArray();
        }

        public static string ParseChar(string input)
        {
            string result = "";
            for (int i = 0; i < input.Length; i += 2)
            {
                result += (char)Int16.Parse(input.Substring(i, 2), NumberStyles.AllowHexSpecifier);
            }
            result = result.Trim();
            return result;
        }

        public static string ParseVarChar(string input)
        {
            return ParseChar(input);
        }
    }
}