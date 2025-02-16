using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace MinesweeperPP.Scripts.Utils
{
    /// <summary>
    /// Provides extension methods for list operations.
    /// </summary>
    internal static class ListExtensions
    {
        /// <summary>
        /// Shuffles the elements of a list using a cryptographically secure random number generator.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to shuffle.</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            using var rng = RandomNumberGenerator.Create();
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var bytes = new byte[4];
                rng.GetBytes(bytes);
                var k = BitConverter.ToInt32(bytes, 0) % (n + 1);
                if (k < 0)
                {
                    k = -k; // Handle negative numbers from modulo
                }

                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}
