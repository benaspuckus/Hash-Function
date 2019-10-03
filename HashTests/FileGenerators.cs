using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace HashTests
{
    [TestClass]
    public class FileGenerators
    {
        [TestMethod]
        public void ManyStringsGenerate()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789qwertyuiopasdfghjklzxcvbnm,./';[]";
            var random = new Random();
            for (var i = 1; i <= 1000000; i++)
            {
                var firstString = new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());

                var secondString = new string(Enumerable.Repeat(chars, 5)
               .Select(s => s[random.Next(s.Length)]).ToArray());

                var result = $"{firstString} {secondString} {Environment.NewLine}";

                File.AppendAllText(@"C:/temp/Files/manyStrings.txt", result);
            }
        }

        [TestMethod]
        public void ClearResults()
        {
            File.WriteAllText(@"C:/temp/Files/testResults.txt", "");
        }

        [TestMethod]
        public void ManyStringsGenerate1SymbolDifference()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789qwertyuiopasdfghjklzxcvbnm,./';[]";
            var random = new Random();
            for (var i = 1; i <= 100000; i++)
            {
                var randomLetter = (char)random.Next('a', 'z');

                var firstString = new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());
                var secondString = firstString;

                firstString = randomLetter + firstString;
                randomLetter = (char)random.Next('a', 'z');
                secondString = randomLetter + secondString;

                var result = $"{firstString} {secondString} {Environment.NewLine}";
                File.AppendAllText(@"C:/temp/Files/manyStrings1SymbolDifferent.txt", result);
            }
        }
    }
}