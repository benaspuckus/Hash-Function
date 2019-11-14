using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HashFunction
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var startTime = DateTime.Now;

            Blockchain blockchain = new Blockchain();

            blockchain.GenerateUsersAndTransactions();
            blockchain.ProcessPendingTransactions();

            var endTime = DateTime.Now;

            Console.WriteLine($"Duration: {endTime - startTime}");
            Console.WriteLine("=========================");
            Console.WriteLine("Users:");
            foreach(var i in blockchain.Users)
            {
                Console.WriteLine($"{i.Name}: {i.Balance}");
            }

            Console.WriteLine("Miners:");
            foreach (var i in blockchain.Miners)
            {
                Console.WriteLine($"{i.Name}: {i.Balance}");
            }


            Console.ReadKey();
        }
    }
}