using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace _21211110_Proyecto2_TBD2.Parsers
{
    public class TransactionLogParser
    {
        private readonly string _connectionString;
        public TransactionLogParser()
        {
            _connectionString = "Server=WIN-PT2J23OAFO0\\MSSQLSERVER2014;Database=Testing;Trusted_Connection=True;MultipleActiveResultSets=True;";
        }

        public List<ColumnMetadataItem> GetMetadata(string dbname, string tableName)
        {
            var primaryKey = GetPk(dbname, tableName);
            var toReturn = new List<ColumnMetadataItem>();
            var query =
                "USE " + dbname + " SELECT [name], [xtype], [length] FROM syscolumns WHERE id = (SELECT id FROM sysobjects WHERE xtype = 'u' and name = '" +
                tableName + "');";
            var cnn = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand(query, cnn);
            cnn.Open();
            var reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                var toAdd = new ColumnMetadataItem(Convert.ToInt32(reader[1]), Convert.ToInt16(reader[2]),
                    reader[0].ToString(), primaryKey.Contains(reader[0].ToString()));
                toReturn.Add(toAdd);
            }
            reader.Close();
            cnn.Close();
            return toReturn;
        }

        public string GetPk(string dbname, string tableName)
        {
            var query = "USE " + dbname + " SELECT column_name FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1 AND table_name = '" + tableName + "'";
            var cnn = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand(query, cnn);
            cnn.Open();
            var reader = myCommand.ExecuteReader();
            var toReturn = "";
            if (reader.Read())
            toReturn = reader[0].ToString();
            reader.Close();
            cnn.Close();
            return toReturn;
        }

        public List<string> GetRowLogContents(string dbname, string tableName)
        {
            var toReturn = new List<string>(); ;
            var query = "USE " + dbname + " SELECT [RowLog Contents 0] FROM fn_dblog(null, null) WHERE Operation = 'LOP_DELETE_ROWS' AND AllocUnitName = 'dbo." + tableName + "'";
            var cnn = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand(query, cnn);
            cnn.Open();
            var reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                var bytes = reader[0] as byte[];

                if (bytes == null) continue;
                var hex = BitConverter.ToString(bytes).Replace("-", string.Empty);
                if (!String.IsNullOrEmpty(hex))
                {
                    toReturn.Add(hex);
                }
            }
            reader.Close();
            cnn.Close();
            return toReturn;
        }
    }
}
