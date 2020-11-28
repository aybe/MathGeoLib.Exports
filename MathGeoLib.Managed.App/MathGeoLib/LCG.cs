using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace MathGeoLib
{
    /// <summary>
    ///     Represents an LCG.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class LCG
    {
        #region Native
        private static class NativeMethods
        {
#if UNITY || UNITY_EDITOR
            private const string DllName = "MathGeoLib.Exports";
#else
            private const string DllName = "MathGeoLib.Exports.dll";
#endif

            [DllImport(DllName)]
            public static extern void lcg_seed([In] [Out] LCG lcg,
                uint seed, uint multiplier, uint increment, uint modulus
            );

            [DllImport(DllName)]
            public static extern uint lcg_int_fast([In] [Out] LCG lcg);

            [DllImport(DllName)]
            public static extern int lcg_int([In] [Out] LCG lcg);

            [DllImport(DllName)]
            public static extern int lcg_int_range([In] [Out] LCG lcg, int a, int b);

            [DllImport(DllName)]
            public static extern float lcg_float([In] [Out] LCG lcg);

            [DllImport(DllName)]
            public static extern float lcg_float_01_incl([In] [Out] LCG lcg);

            [DllImport(DllName)]
            public static extern float lcg_float_neg([In] [Out] LCG lcg);

            [DllImport(DllName)]
            public static extern float lcg_float_range([In] [Out] LCG lcg, float a, float b);

            [DllImport(DllName)]
            public static extern float lcg_float_incl([In] [Out] LCG lcg, float a, float b);
        }
        #endregion

        #region Fields
        public uint multiplier;
        public uint increment;
        public uint modulus;
        public uint lastNumber;
        #endregion

        #region Constructors
        public LCG()
        {
            this.lastNumber = 59263;
            this.multiplier = 69621;
            this.increment = 0;
            this.modulus = 0x7FFFFFFF /* 2^31 - 1 */;
        }

        /// Initializes the generator using a custom seed.
        public LCG(uint seed, uint multiplier, uint increment, uint modulus)
        {
            this.lastNumber = seed;
            this.multiplier = multiplier;
            this.increment = increment;
            this.modulus = modulus;
            NativeMethods.lcg_seed(this, seed, multiplier, increment, modulus);
        }
        #endregion

        #region Instance

        /// <summary>
        ///  Returns a random integer picked uniformly in the range [0, 2^32-1].
        ///  </summary>
        ///  <remarks> The configurable modulus and increment are not used by this function, but are always increment == 0, modulus=2^32. </remarks>
        public uint IntFast()
        {
            return NativeMethods.lcg_int_fast(this);
        }

        /// <summary>
        /// Returns a random integer picked uniformly in the range [a, b]
        /// </summary>
        /// <param name="a"> Lower bound, inclusive. </param>
        /// <param name="b"> Upper bound, inclusive. </param>
        /// <returns> A random integer picked uniformly in the range [a, b] </returns>
        public int Int()
        {
            return NativeMethods.lcg_int(this);
        }

        /// <summary>
        /// Returns a random integer picked uniformly in the range [a, b]
        /// </summary>
        /// <param name="a"> Lower bound, inclusive. </param>
        /// <param name="b"> Upper bound, inclusive. </param>
        /// <returns> A random integer picked uniformly in the range [a, b] </returns>
        public int Int(int a, int b)
        {
            return NativeMethods.lcg_int_range(this, a, b);
        }

        /// <summary> Returns a random float picked uniformly in the range [0, 1]. </summary>
        public float Float()
        {
            return NativeMethods.lcg_float(this);
        }

        /// <summary> Returns a random float picked uniformly in the range [a, b]. </summary>
        /// <param name="a"> Lower bound, inclusive. </param>
	    /// <param name="b"> Upper bound, exclusive. </param>
	    /// <returns> A random float picked uniformly in the range [a, b] </returns>
	    /// <remarks> This function is slower than LCG::FloatIncl(). If you do not care about the open/closed interval, prefer calling FloatIncl() instead. </remarks>
	    /// <see cref="LCG.Float"/> 
        /// <see cref="LCG.FloatIncl"/>
        public float Float(float a, float b)
        {
            return NativeMethods.lcg_float_range(this, a, b);
        }

        /// <summary>Returns a random float picked uniformly in the range [0, 1].</summary>
        /// <remarks>This is much slower than Float()! Prefer that function instead if possible.</remarks>
        public float Float01Incl()
        {
            return NativeMethods.lcg_float_01_incl(this);
        }
        
        /// <summary> Returns a random float picked uniformly in the range [-1, 1]. </summary>
        /// <remarks> 
        /// This function has one more bit of randomness compared to Float(), but has a theoretical bias
        /// towards 0.0, since floating point has two representations for 0 (+0 and -0).
        /// </remarks>
        float FloatNeg1_1()
        {
            return NativeMethods.lcg_float_neg(this);
        }

        /// <summary> Returns a random float picked uniformly in the range [a, b]. </summary>
        /// <param name="a"> Lower bound, inclusive. </param>
	    /// <param name="b"> Upper bound, inclusive. </param>
	    /// <returns> A random float picked uniformly in the range [a, b] </returns>
        float FloatIncl(float a, float b)
        {
            return NativeMethods.lcg_float_incl(this, a, b);
        }
        #endregion
    }
}
