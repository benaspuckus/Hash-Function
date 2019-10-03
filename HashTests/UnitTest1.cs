using HashFunction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            string searchPattern = "manySymbols1Difference*.txt";
            string[] files = Directory.GetFiles(@"C:/temp/Files/", searchPattern);

            foreach (var i in files)
            {
                var lines = File.ReadAllText(i);
                var hashValue = Program.Hash(lines);
                var fileName = Path.GetFileName(i);
                var tempObject = Tuple.Create(fileName, hashValue);
                output.Add(tempObject);
            }
            var isEqual = string.Equals(output[0].Item2, output[1].Item2) ? "FAIL" : "PASS";
            var result = $"1.Comparing 2 files with 100 000 symbols each and one symbol different: {Environment.NewLine} {output[0].Item1} file hash: {output[0].Item2} {Environment.NewLine} {output[1].Item1} file hash: {output[1].Item2} - {isEqual} {Environment.NewLine}";

            File.AppendAllText(@"C:/temp/Files/testResults.txt", result);
            File.AppendAllText(@"C:/temp/Files/testResults.txt", $"--------------------------------------------------------- {Environment.NewLine}");

            Assert.AreNotEqual(output[0].Item2, output[1].Item2);
        }

        [TestMethod]
        public void Compare2Files_AllSymbolsDifferent()
        {
            var output = new List<Tuple<string, string>>();
            string searchPattern = "randomSymbols*.txt";
            string[] files = Directory.GetFiles(@"C:/temp/Files/", searchPattern);

            foreach (var i in files)
            {
                var lines = File.ReadAllText(i);
                var hashValue = Program.Hash(lines);
                var fileName = Path.GetFileName(i);
                var tempObject = Tuple.Create(fileName, hashValue);
                output.Add(tempObject);
            }
            var isEqual = string.Equals(output[0].Item2, output[1].Item2) ? "FAIL" : "PASS";
            var result = $"2.Comparing 2 files with 100 000 random symbols: {Environment.NewLine} {output[0].Item1} file || hash: {output[0].Item2} {Environment.NewLine} {output[1].Item1} file hash: {output[1].Item2} - {isEqual} {Environment.NewLine}";

            File.AppendAllText(@"C:/temp/Files/testResults.txt", result);
            File.AppendAllText(@"C:/temp/Files/testResults.txt", $"--------------------------------------------------------- {Environment.NewLine}");

            Assert.AreNotEqual(output[0].Item2, output[1].Item2);
        }

        [TestMethod]
        public void Compare2Files_Only1Symbol()
        {
            var output = new List<Tuple<string, string>>();
            string searchPattern = "oneSymbol*.txt";
            string[] files = Directory.GetFiles(@"C:/temp/Files/", searchPattern);

            foreach (var i in files)
            {
                var lines = File.ReadAllText(i);
                var hashValue = Program.Hash(lines);
                var fileName = Path.GetFileName(i);
                var tempObject = Tuple.Create(fileName, hashValue);
                output.Add(tempObject);
            }
            var isEqual = string.Equals(output[0].Item2, output[1].Item2) ? "FAIL" : "PASS";
            var result = $"3.Comparing 2 files 1 different symbol: {Environment.NewLine} {output[0].Item1} file hash: {output[0].Item2} {Environment.NewLine} {output[1].Item1} file hash: {output[1].Item2} - {isEqual} {Environment.NewLine}";

            File.AppendAllText(@"C:/temp/Files/testResults.txt", result);
            File.AppendAllText(@"C:/temp/Files/testResults.txt", $"--------------------------------------------------------- {Environment.NewLine}");

            Assert.AreNotEqual(output[0].Item2, output[1].Item2);
        }

        [TestMethod]
        public void Konstitucija()
        {
            string line;
            var lines = new string[1];
            var output = new List<Tuple<string, string>>();
            var watch = new Stopwatch();
            StreamReader file = new StreamReader(@"C:/temp/Files/konstitucija.txt");
            while ((line = file.ReadLine()) != null)
            {
                watch.Start();
                Program.Hash(line);
                watch.Stop();
            }

            var elapsed = watch.ElapsedMilliseconds;
            var result = $"Elapsed time while reading konstitucija: {elapsed.ToString()} ms {Environment.NewLine}";

            File.AppendAllText(@"C:/temp/Files/testResults.txt", result);
            File.AppendAllText(@"C:/temp/Files/testResults.txt", $"--------------------------------------------------------- {Environment.NewLine}");
        }

        [TestMethod]
        public void ManyStrings()
        {
            string[] lines = File.ReadAllLines(@"C:/temp/Files/manyStrings.txt");
            var output = new List<Tuple<string, string>>();
            var counter = 0;
            foreach (var line in lines)
            {
                string[] words = line.Split(' ');
                var hashValue1 = Program.Hash(words[0]);
                var hashValue2 = Program.Hash(words[1]);

                var areEqual = string.Equals(hashValue1, hashValue2);

                if (areEqual && !string.Equals(hashValue1, hashValue2))
                {
                    var tempObject = Tuple.Create(hashValue1, hashValue2);
                    output.Add(tempObject);
                    counter++;
                }
            }

            var result = $"Same strings while reading million random string pairs: {counter} {Environment.NewLine}";
            File.AppendAllText(@"C:/temp/Files/testResults.txt", result);
            foreach (var str in output)
            {
                var text = $"{str.Item1} and {str.Item2} {Environment.NewLine}";
                File.AppendAllText(@"C:/temp/Files/testResults.txt", text);
            }

            File.AppendAllText(@"C:/temp/Files/testResults.txt", $"--------------------------------------------------------- {Environment.NewLine}");

            Assert.AreNotEqual(output, new List<Tuple<string, string>>());
        }

        [TestMethod]
        public void ManyStrings_1SymbolDifference()
        {
            string[] lines = File.ReadAllLines(@"C:/temp/Files/manyStrings1SymbolDifferent.txt");
            var output = new List<Tuple<string, string>>();
            var counter = 0;
            foreach (var line in lines)
            {
                string[] words = line.Split(' ');
                var hashValue1 = Program.Hash(words[0]);
                var hashValue2 = Program.Hash(words[1]);

                var areEqual = string.Equals(hashValue1, hashValue2);

                if (areEqual && !string.Equals(hashValue1, hashValue2))
                {
                    var tempObject = Tuple.Create(words[0], words[1]);
                    output.Add(tempObject);
                    counter++;
                }
            }

            var result = $"Same strings while reading 100 thousand same strings with 1 symbol different: {counter} {Environment.NewLine}";
            File.AppendAllText(@"C:/temp/Files/testResults.txt", result);
            foreach (var str in output)
            {
                var text = $"{str.Item1} and {str.Item2} {Environment.NewLine}";
                File.AppendAllText(@"C:/temp/Files/testResults.txt", text);
            }

            File.AppendAllText(@"C:/temp/Files/testResults.txt", $"--------------------------------------------------------- {Environment.NewLine}");

            Assert.AreNotEqual(output, new List<Tuple<string, string>>());
        }
    }
}