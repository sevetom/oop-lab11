using System;

namespace ComplexAlgebra
{
    /// <summary>
    /// A type for representing Complex numbers.
    /// </summary>
    ///
    /// TODO: Model Complex numbers in an object-oriented way and implement this class.
    /// TODO: In other words, you must provide a means for:
    /// TODO: * instantiating complex numbers
    /// TODO: * accessing a complex number's real, and imaginary parts
    /// TODO: * accessing a complex number's modulus, and phase
    /// TODO: * complementing a complex number
    /// TODO: * summing up or subtracting two complex numbers
    /// TODO: * representing a complex number as a string or the form Re +/- iIm
    /// TODO:     - e.g. via the ToString() method
    /// TODO: * checking whether two complex numbers are equal or not
    /// TODO:     - e.g. via the Equals(object) method
    public class Complex
    {
        public double Real { get; }
        public double Imaginary { get; }

        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public double Modulus => Math.Sqrt(Math.Pow(Real, 2) + Math.Pow(Imaginary, 2));
        public double Phase => Math.Atan2(Imaginary, Real);

        public Complex Complement() => new Complex(Real, -Imaginary);

        public Complex Plus(Complex num) => new Complex(Real + num.Real, Imaginary + num.Imaginary);

        public Complex Minus(Complex num) => new Complex(Real - num.Real, Imaginary - num.Imaginary);
    
        public override String ToString()
        {
            string retstr;
            retstr = Real != 0 ? $"{Real} " : "";
            if (Imaginary != 0)
            {   
                if (retstr.Equals(""))
                {
                    retstr = Math.Abs(Imaginary) != 1 ? $"{Imaginary}i" : Imaginary > 0 ? "i" : "-i";
                }
                else
                {   
                    retstr += Imaginary > 0 ? "+ " : "- ";
                    retstr += Math.Abs(Imaginary) != 1 ? $"i{Imaginary}" : "i";
                }
            }
            else if (retstr == "") retstr = "0";
            return retstr;
        }

        public bool Equals(Complex num) => Real.Equals(num.Real) && Imaginary.Equals(num.Imaginary);
    
        public override bool Equals(object obj) => obj is Complex num && Equals(num);
    
        public override int GetHashCode() => HashCode.Combine(Real, Imaginary);
    }
}