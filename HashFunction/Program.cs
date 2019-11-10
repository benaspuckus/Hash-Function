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
            var output = new List<Tuple<string, string>>();
            string searchPattern = "program.txt";
            string[] files = Directory.GetFiles(@"C:/temp/Files/", searchPattern);

            foreach (var i in files)
            {
                var lines = File.ReadAllText(i);
                var hashValue = Hash(lines);
                var tempObject = Tuple.Create(i, hashValue);
                output.Add(tempObject);
            }

            foreach (var i in output)
            {
                Console.WriteLine($"Name: {i.Item1}, Hash: {i.Item2}");
            }

            Console.ReadKey();
        }

        public static string Hash(string data)
        {
            byte[] array1 = new byte[8];
            byte[] array2 = new byte[8];
            byte[] array3 = new byte[8];
            byte[] array4 = new byte[8];
            var amplifier = 1;

            for (var i = 0; i < 8; i++)
            {
                array1[i] = 48;
                array2[i] = 48;
                array3[i] = 48;
                array4[i] = 48;
            }
            foreach (var letter in data)
            {
                for (var i = 0; i < 8; i++)
                {
                    int letterIntValue = letter;
                    byte arrayByteValue = array1[i];
                    arrayByteValue = (byte)(letterIntValue * amplifier + arrayByteValue);

                    if (i == 2)
                    {
                        amplifier = amplifier + 3;
                    }
                    while (!(arrayByteValue >= 48 && arrayByteValue <= 57) && !(arrayByteValue >= 65 && arrayByteValue <= 90) && !(arrayByteValue >= 97 && arrayByteValue <= 122))
                    {
                        arrayByteValue = (byte)(arrayByteValue * amplifier + arrayByteValue + 1);
                        amplifier++;
                    }
                    array1[i] = arrayByteValue;
                    amplifier++;
                }
            }

            foreach (var letter in data)
            {
                for (var i = 0; i < 8; i++)
                {
                    int letterIntValue = letter;
                    byte arrayByteValue = array2[i];
                    arrayByteValue = (byte)(letterIntValue * amplifier + arrayByteValue);

                    if (i == 4)
                    {
                        amplifier = amplifier + 7;
                    }

                    while (!(arrayByteValue >= 48 && arrayByteValue <= 57) && !(arrayByteValue >= 65 && arrayByteValue <= 90) && !(arrayByteValue >= 97 && arrayByteValue <= 122))
                    {
                        arrayByteValue = (byte)(arrayByteValue * amplifier + arrayByteValue + 1);
                        amplifier++;
                    }
                    array2[i] = arrayByteValue;
                    amplifier++;
                }
            }

            foreach (var letter in data)
            {
                for (var i = 0; i < 8; i++)
                {
                    int letterIntValue = letter;
                    byte arrayByteValue = array3[i];
                    arrayByteValue = (byte)(letterIntValue * amplifier + arrayByteValue);

                    if (i == 6)
                    {
                        amplifier = amplifier + 13;
                    }

                    while (!(arrayByteValue >= 48 && arrayByteValue <= 57) && !(arrayByteValue >= 65 && arrayByteValue <= 90) && !(arrayByteValue >= 97 && arrayByteValue <= 122))
                    {
                        arrayByteValue = (byte)(arrayByteValue * amplifier + arrayByteValue + 1);
                        amplifier++;
                    }
                    array3[i] = arrayByteValue;
                    amplifier++;
                }
            }

            foreach (var letter in data)
            {
                for (var i = 0; i < 8; i++)
                {
                    int letterIntValue = letter;
                    byte arrayByteValue = array4[i];
                    arrayByteValue += (byte)(letterIntValue * amplifier + arrayByteValue);

                    if (i == 8)
                    {
                        amplifier = amplifier + 11;
                    }

                    while (!(arrayByteValue >= 48 && arrayByteValue <= 57) && !(arrayByteValue >= 65 && arrayByteValue <= 90) && !(arrayByteValue >= 97 && arrayByteValue <= 122))
                    {
                        arrayByteValue = (byte)(arrayByteValue * amplifier + arrayByteValue + 1);
                        amplifier++;
                    }
                    array4[i] = arrayByteValue;
                    amplifier++;
                }
            }

            IEnumerable<byte> hashedArray = array1.Concat(array2).Concat(array3).Concat(array4);

            var hashedString = Encoding.ASCII.GetString(hashedArray.ToArray());

            return hashedString;
        }
    }
}