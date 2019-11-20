using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashFunction
{
    public class Transaction
    {
        public User FromAddress { get; set; }
        public User ToAddress { get; set; }
        public int Amount { get; set; }
        public string TransactionHash { get; set; }
        public DateTimeOffset DateCreated { get; set; }


        public Transaction(User fromAddress, User toAddress, int amount)
        {
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Amount = amount;
            DateCreated = DateTimeOffset.UtcNow;
            TransactionHash = HashFunction.GetHash($"{FromAddress?.ToString()}{ToAddress?.ToString()}{Amount}{DateCreated}");
        }
    }
}
