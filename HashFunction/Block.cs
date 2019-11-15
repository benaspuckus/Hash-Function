using System;
using System.Collections.Generic;

namespace HashFunction
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public List<Transaction> Transactions { get; set; }
        public int Nonce { get; set; } = 0;

        public Block(DateTime timeStamp, string previousHash, List<Transaction> transactions)
        {
            Index = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            Transactions = transactions;
            Hash = CalculateHash();
        }

        public string CalculateHash()
        {
            var data = $"{TimeStamp.ToString()}{PreviousHash}{Transactions}{Nonce}";
            var hash = HashFunction.GetHash(data);

            return hash;
        }

        public bool Mine(int difficulty, int iterationLimit)
        {
            var leadingZeros = new string('0', difficulty);
            var iterations = 0;
            while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeros)
            {
                this.Nonce++;
                this.Hash = this.CalculateHash();
                iterations++;
                if (iterations >= iterationLimit)
                    break;
            }

            if (this.Hash.Substring(0, difficulty) == leadingZeros)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}