using System;
using System.Collections.Generic;
using System.Linq;

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

            //Generates Users
            for (var i = 0; i < 1000; i++)
            {
                var name = $"user{i}";
                var userHash = HashFunction.GetHash(name);
                var balance = rnd.Next(100, 1000);
                Users.Add(new User(name, userHash, balance));
            }

            //Generates Miners
            for (var i = 0; i < 10; i++)
            {
                var name = $"miner{i}";
                var userHash = HashFunction.GetHash(name);
                var balance = 0;
                Miners.Add(new User(name, userHash, balance));
            }

            //Generates random x number transactions
            for (var i = 0; i < 10000; i++)
            {
                var senderIndex = rnd.Next(Users.Count);
                var receiverIndex = rnd.Next(Users.Count);

                //If receiver is same as sender
                while (receiverIndex == senderIndex)
                {
                    receiverIndex = rnd.Next(Users.Count);
                }

                var amount = rnd.Next(10, 250);

                var sender = Users[senderIndex];
                var receiver = Users[receiverIndex];

                CreateTransaction(new Transaction(sender, receiver, amount));
            }
        }

        //Blockchain initialization
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
            var iterationLimit = 500;
            var isHashFound = false;

            Block latestBlock = GetLatestBlock();

            //Sets info for current block using previous block data
            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;

            //Using merkle root algorythm calculates merkle root
            var transactionHashes = block.Transactions.Select(x => x.TransactionHash).ToList();
            block.MerkleRoot = MerkleTree.BuildMerkleRoot(transactionHashes);

            //Creates temporary block list for miners to mine
            var blockList = new List<Block>();
            var time = DateTime.Now;

            for (var i = 0; i < 5; i++)
            {
                blockList.Add(block);
            }

            //Makes block order random
            var shuffledBlocks = blockList.OrderBy(a => Guid.NewGuid()).ToList();

            //With each iteration, iteration limit is increased by x value untill someone manages to get correct hash
            while (!isHashFound)
            {
                foreach (var i in shuffledBlocks)
                {
                    isHashFound = block.Mine(this.Difficulty, iterationLimit);

                    if (isHashFound)
                    {
                        break;
                    }
                }

                if (!isHashFound)
                {
                    iterationLimit = iterationLimit + 150;
                }
            }

            Console.WriteLine($"Block found with {iterationLimit} iteration limit");
            Chain.Add(block);

            //Calculates block transactions and calculates users current balance
            CalculateUsersBalance(block.Transactions);
        }

        private void CalculateUsersBalance(List<Transaction> transactions)
        {
            foreach (var i in transactions)
            {
                var sender = i.FromAddress;
                var receiver = i.ToAddress;

                if (sender != null)
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
            while (PendingTransactions.Count > 1)
            {
                //How many transaction per block to take
                var transactions = PendingTransactions.Take(150).ToList();
                var listOfInvalidTransactions = new List<Transaction>();

                for (var i = 0; i < transactions.Count; i++)
                {
                    var hash = HashFunction.GetHash($"{transactions[i].FromAddress?.ToString()}{transactions[i].ToAddress?.ToString()}{transactions[i].Amount}{transactions[i].DateCreated}");
                    var senderBalace = transactions[i].FromAddress?.Balance;
                    var amaountToSend = transactions[i].Amount;

                    //Catch if users are trying to send more than they have
                    if (transactions[i].TransactionHash != hash)
                    {
                        Console.WriteLine($"Hashes are not equal!");
                        listOfInvalidTransactions.Add(transactions[i]);
                    }
                    else if (senderBalace < amaountToSend)
                    {
                        Console.WriteLine($"{transactions[i].FromAddress?.Name} just tried to send more money than it has! Had {transactions[i].FromAddress?.Balance}, tried to send {transactions[i].Amount}");
                        listOfInvalidTransactions.Add(transactions[i]);
                    }
                }

                var count = transactions.Count;

                var filteredTransactions = transactions.Except(listOfInvalidTransactions).ToList();
                Block block = new Block(DateTime.Now, GetLatestBlock().Hash, filteredTransactions);

                //Removes invalid transactions
                for (var i = 0; i < count; i++)
                {
                    var transactionToRemove = transactions[i];
                    PendingTransactions.Remove(transactionToRemove);
                }

                //Adds block to chain and does the calculations
                AddBlock(block);
                Random rnd = new Random();
                var minersIndex = rnd.Next(Miners.Count);
                var miner = Miners[minersIndex];

                //Adds "miner" transaction as a reward
                CreateTransaction(new Transaction(null, miner, _reward));
                iteration++;
                Console.WriteLine($"{PendingTransactions.Count} Pending transactions left");
            }
        }
    }
}