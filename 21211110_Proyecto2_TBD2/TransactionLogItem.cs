using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21211110_Proyecto2_TBD2
{
    public class TransactionLogItem
    {
        public string TransactionId { get; set; }
        public string BeginTime { get; set; }
        public string TransactionName { get; set; }
        public List<string> RowLogContents { get; set; }
        
        public TransactionLogItem(string transactionId, string beginTime, string transactionName, List<string> rowLogContents)
        {
            TransactionId = transactionId;
            BeginTime = beginTime;
            TransactionName = transactionName;
            RowLogContents = rowLogContents;
        }
    }
}
