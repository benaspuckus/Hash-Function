using System.Collections.Generic;
using System.Linq;

namespace HashFunction

{
    public class MerkleTree
    {
        public static string BuildMerkleRoot(List<string> merkelLeaves)
        {
            if (merkelLeaves == null || !merkelLeaves.Any())

                return string.Empty;

            if (merkelLeaves.Count() == 1)
            {
                return merkelLeaves.First();
            }

            if (merkelLeaves.Count() % 2 > 0)
            {
                merkelLeaves.Add(merkelLeaves.Last());
            }

            var merkleBranches = new List<string>();

            for (int i = 0; i < merkelLeaves.Count(); i += 2)
            {
                var leafPair = string.Concat(merkelLeaves[i], merkelLeaves[i + 1]);
                //double hash
                merkleBranches.Add(HashFunction.GetHash(HashFunction.GetHash(leafPair)));
            }
            return BuildMerkleRoot(merkleBranches);
        }

        private static string SwapAndReverse(string input)
        {
            string newString = string.Empty; ;
            for (int i = 0; i < input.Count(); i += 2)
            {
                newString += string.Concat(input[i + 1], input[i]);
            }
            return ReverseString(newString);
        }

        private static string ReverseString(string original)
        {
            return new string(original.Reverse().ToArray());
        }
    }
}