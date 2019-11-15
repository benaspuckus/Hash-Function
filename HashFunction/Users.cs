using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashFunction
{
    public class User
    {
        public string Name { get; set; }
        public string PublicHash { get; set; }
        public float Balance { get; set; }

        public User(string name, string hash, int balance)
        {
            Name = name;
            PublicHash = hash;
            Balance = balance;
        }
        public void SetBalance(float payment)
        {
            Balance += payment;
        }
    }
}
