using HashFunction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace HashTests
{
    [TestClass]
    public class HashTestClass
    {
        [TestMethod]
        public void Compare2Files_1SymbolDifference()
        {
            var output = new List<Tuple<string, string>>();
            string searchPattern = "test*.txt";
            string[] files = Directory.GetFiles(@"C:/temp/Files/", searchPattern);

            foreach (var i in files)
            {
                string[] lines = File.ReadAllLines(i);
                var hashValue = Program.Hash(lines);
                var tempObject = Tuple.Create(i, hashValue);
                output.Add(tempObject);
            }

            Assert.AreNotEqual(output[0].Item2, output[1].Item2);

            Console.ReadKey();
        }
    }
}