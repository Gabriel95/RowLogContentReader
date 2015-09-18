using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21211110_Proyecto2_TBD2
{
    public enum XType
    {
        TinyInt = 48,
        Int = 56,
        SmallDateTime = 58,
        Real = 59,
        Money = 60,
        DateTime = 61,
        Float = 62,
        Bit = 104,
        Decimal = 106,
        Numeric = 108,
        BigInt = 127,
        VarChar = 167,
        Binary = 173,
        Char = 175
    }

    public class ColumnMetadataItem
    {
        public XType Type { get; set; }
        public short Length { get; set; }
        public string ColumnName { get; set; }
        public bool IsPrimaryKey { get; set; }

        public ColumnMetadataItem(int xtype, short length, string columnName, bool isPrimaryKey)
        {
            Type = (XType)xtype;
            Length = length;
            ColumnName = columnName;
            IsPrimaryKey = isPrimaryKey;
        }
    }
}
