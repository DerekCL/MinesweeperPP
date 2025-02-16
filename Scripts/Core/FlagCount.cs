using System;
using System.Runtime.InteropServices;

namespace MinesweeperPP.Scripts.Core
{
    /// <summary>
    /// Represents the number of flags placed in a Minesweeper game.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct FlagCount
    {
        /// <summary>
        /// Gets the number of flags.
        /// </summary>
        public int Value
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlagCount"/> struct.
        /// </summary>
        /// <param name="count">The number of flags.</param>
        /// <exception cref="ArgumentException">Thrown when count is negative.</exception>
        public FlagCount(int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("Flag count cannot be negative", nameof(count));
            }

            this.Value = count;
        }

        /// <summary>
        /// Decrements the flag count by one.
        /// </summary>
        /// <returns>A new <see cref="FlagCount"/> instance with the decremented value.</returns>
        public FlagCount Decrement()
        {
            return new FlagCount(this.Value - 1);
        }

        /// <summary>
        /// Increments the flag count by one.
        /// </summary>
        /// <returns>A new <see cref="FlagCount"/> instance with the incremented value.</returns>
        public FlagCount Increment()
        {
            return new FlagCount(this.Value + 1);
        }

        /// <summary>
        /// Converts the flag count to a string representation.
        /// </summary>
        /// <returns>A string representation of the flag count (padded to 3 digits).</returns>
        public override string ToString()
        {
            return $"{this.Value:D3}";
        }
    }
}
