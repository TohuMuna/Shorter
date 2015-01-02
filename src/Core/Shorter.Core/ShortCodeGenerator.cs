namespace Shorter.Core
{
    using System;
    using System.Security.Cryptography;

    // http://www.obviex.com/samples/password.aspx
    public static class ShortCodeGenerator
    {
        private const string LowerCase = "abcdefgijkmnopqrstwxyz";
        private const string UpperCase = "ABCDEFGHJKLMNPQRSTWXYZ";
        private const string Numbers = "0123456789";

        public static string Generate()
        {
            var rand = new Random(Guid.NewGuid().GetHashCode());
            return RandomCharacters(rand.Next(4, 7));
        }

        private static string RandomCharacters(int length)
        {
            // Create a local array containing supported short-url characters grouped by types.
            var charGroups = new[] { LowerCase.ToCharArray(), UpperCase.ToCharArray(), Numbers.ToCharArray() };

            // Use this array to track the number of unused characters in each character group.
            var charsLeftInGroup = new int[charGroups.Length];

            // Initially, all characters in each group are not used.
            for (int i = 0; i < charsLeftInGroup.Length; i++)
            {
                charsLeftInGroup[i] = charGroups[i].Length;
            }

            // Use this array to track (iterate through) unused character groups.
            int[] leftGroupsOrder = new int[charGroups.Length];

            // Initially, all character groups are not used.
            for (int i = 0; i < leftGroupsOrder.Length; i++)
            {
                leftGroupsOrder[i] = i;
            }

            // Use a 4-byte array to fill it with random bytes and convert it then
            // to an integer value.
            byte[] randomBytes = new byte[4];

            // Generate 4 random bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            // Convert 4 bytes into a 32-bit integer value.
            int seed = (randomBytes[0] & 0x7f) << 24 | randomBytes[1] << 16 | randomBytes[2] << 8 | randomBytes[3];

            // Now, this is real randomization.
            Random random = new Random(seed);

            // This array will hold short-url characters.
            var shortUrl = new char[random.Next(length, length)];

            // Index of the last non-processed group.
            int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

            // Generate short-url characters one at a time.
            for (int i = 0; i < shortUrl.Length; i++)
            {
                // If only one character group remained unprocessed, process it;
                // otherwise, pick a random character group from the unprocessed
                // group list. To allow a special character to appear in the
                // first position, increment the second parameter of the Next
                // function call by one, i.e. lastLeftGroupsOrderIdx + 1.
                int nextLeftGroupsOrderIdx = lastLeftGroupsOrderIdx == 0 ? 0 : random.Next(0, lastLeftGroupsOrderIdx);

                // Get the actual index of the character group, from which we will
                // pick the next character.
                int nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

                // Get the index of the last unprocessed characters in this group.
                int lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                // If only one unprocessed character is left, pick it; otherwise,
                // get a random character from the unused character list.
                int nextCharIdx = lastCharIdx == 0 ? 0 : random.Next(0, lastCharIdx + 1);

                // Add this character to the short-url.
                shortUrl[i] = charGroups[nextGroupIdx][nextCharIdx];

                // If we processed the last character in this group, start over.
                if (lastCharIdx == 0)
                {
                    charsLeftInGroup[nextGroupIdx] = charGroups[nextGroupIdx].Length;
                }
                else
                {
                    // Swap processed character with the last unprocessed character
                    // so that we don't pick it until we process all characters in
                    // this group.
                    if (lastCharIdx != nextCharIdx)
                    {
                        char temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] = charGroups[nextGroupIdx][nextCharIdx];
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }
                }

                // If we processed the last group, start all over.
                if (lastLeftGroupsOrderIdx == 0)
                {
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                }
                else
                {
                    // Swap processed group with the last unprocessed group
                    // so that we don't pick it until we process all groups.
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] = leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }
                }
            }

            // Convert password characters into a string and return the result.
            return new string(shortUrl);
        }
    }
}
