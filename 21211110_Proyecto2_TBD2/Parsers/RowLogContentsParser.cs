using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace _21211110_Proyecto2_TBD2.Parsers
{
    public class RowLogContentsParser
    {

        public List<string> ParseRowLogContents(string rowlogcontentzero, List<ColumnMetadataItem> metadata)
        {
            var toReturn = new List<string>();

            //starts with 3 = varlenght fields exists in hex
            if (rowlogcontentzero.StartsWith("3"))
            {
                //where is the variable length offset is located
                var vLengthOffset = (Hexconveters.ParseSmallInt(rowlogcontentzero.Substring( 4, 4 )) * 2 ) - 2;

                //remove the first 4 bytes
                rowlogcontentzero = rowlogcontentzero.Substring(8);
                
                //get the number of varlength columns
                var vlengthn = Hexconveters.ParseSmallInt(rowlogcontentzero.Substring(vLengthOffset, 4));
                
                //get data from varlengthcolumns
                var vlengthColumns = rowlogcontentzero.Substring(vLengthOffset + 4);

                //convert fixed length values
                var cont = 0;
                for (var i = 0; i < metadata.Count - vlengthn; i++)
                {
                    toReturn.Add(Hexconveters.Parse(rowlogcontentzero.Substring(cont, metadata[i].Length*2), metadata[i].Type));
                    cont = cont + metadata.ElementAt(i).Length*2;
                }
                
                //get delimiters from varchar
                cont = 0;
                var delimiters = new List<int> {(vlengthn*4)};
                for (var i = 0; i < vlengthn; i++)
                {
                    delimiters.Add(((Hexconveters.ParseSmallInt(vlengthColumns.Substring(cont, 4)) * 2)) - (vLengthOffset + 12));
                    cont = cont + 4;
                }
               
                //get varlength values
                for (var i = 0; i < vlengthn; i++)
                {
                    if (i != vlengthn - 1)
                    {

                        var toAdd = Hexconveters.Parse( vlengthColumns.Substring(delimiters[i], delimiters[i + 1] - (delimiters[i])), metadata.ElementAt(metadata.Count - vlengthn + i).Type);
                        toReturn.Add(toAdd);
                    }
                    else
                    {
                        var toAdd = Hexconveters.Parse(vlengthColumns.Substring(delimiters[i]), metadata.ElementAt(metadata.Count - 1).Type);
                        toReturn.Add(toAdd);
                    }
                }
            }
            else
            {
                rowlogcontentzero = rowlogcontentzero.Substring(8);
                
                for (int i = 0, n = 0; i < metadata.Count; i++)
                {
                    toReturn.Add(Hexconveters.Parse(rowlogcontentzero.Substring(n, metadata.ElementAt(i).Length*2), metadata.ElementAt(i).Type));
                    n += metadata.ElementAt(i).Length*2;
                }
            }
            return toReturn;
        }
    }
}
