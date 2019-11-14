﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashFunction
{
    public class Blockchain
    {
        public List<Block> Chain { set; get; }
        public List<User> Users { get; set; } = new List<User>();
        public List<User> Miners { get; set; } = new List<User>();

        public int Difficulty { set; get; } = 2;
        public List<Transaction> PendingTransactions = new List<Transaction>();
        private int _reward = 1;

        public void GenerateUsersAndTransactions()
        {
            Random rnd = new Random();

            for (var i = 0; i < 1000; i++)
            {
                var name = $"user{i}";
                var userHash = HashFunction.GetHash(name);
                var balance = rnd.Next(100, 10000);
                Users.Add(new User(name, userHash, balance));
            }

            for (var i = 0; i < 100; i++)
            {
                var name = $"miner{i}";
                var userHash = HashFunction.GetHash(name);
                var balance = 0;
                Miners.Add(new User(name, userHash, balance));
            }

            for (var i = 0; i < 1000; i++)
            {
                var senderIndex = rnd.Next(Users.Count);
                var receiverIndex = rnd.Next(Users.Count);

                while(receiverIndex == senderIndex)
                {
                    receiverIndex = rnd.Next(Users.Count);
                }

                var amount = rnd.Next(10, 250);

                var sender = Users[senderIndex];
                var receiver = Users[receiverIndex];

                CreateTransaction(new Transaction(sender, receiver, amount));
            }


        }

        public Blockchain()
        {
            InitializeChain();
            AddGenesisBlock();
        }

        public void InitializeChain()
        {
            Chain = new List<Block>();
        }

        public Block CreateGenesisBlock()
        {
            return new Block(DateTime.Now, null, new List<Transaction>());
        }

        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }

        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public void AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();
            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;
            block.Mine(this.Difficulty);
            Chain.Add(block);

            CalculateUsersBalance(block.Transactions);
        }

        private void CalculateUsersBalance(List<Transaction> transactions)
        {
            foreach (var i in transactions)
            {
                var sender = i.FromAddress;
                var receiver = i.ToAddress;

                if(sender != null)
                {
                    sender.SetBalance(-(i.Amount));
                }

                receiver.SetBalance(i.Amount);
            }
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }
        public void CreateTransaction(Transaction transaction)
        {
            PendingTransactions.Add(transaction);
        }

        public void ProcessPendingTransactions()
        {
            var iteration = 0;
            while(PendingTransactions.Count > 1)
            {
                var transactions = PendingTransactions.Take(100).ToList();
                Block block = new Block(DateTime.Now, GetLatestBlock().Hash, transactions);
                var count = transactions.Count;

                for (var i = 0; i < count; i++)
                {
                    var transactionToRemove = transactions[i];
                    PendingTransactions.Remove(transactionToRemove);
                }

                AddBlock(block);
                Console.WriteLine($"{iteration} block added");
                Random rnd = new Random();
                var minersIndex = rnd.Next(Miners.Count);
                var miner = Miners[minersIndex];
                CreateTransaction(new Transaction(null, miner, _reward));
                iteration++;
            }
            
        }
    }
}