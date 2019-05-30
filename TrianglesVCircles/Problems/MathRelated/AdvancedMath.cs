using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TrianglesVCircles.Helpers;

namespace TrianglesVCircles.Problems.MathRelated
{
    public static class AdvancedMath
    {
        private static Random _rand = new Random(GlobalRandom.Next(877,1444));
        public static Tuple<string, string>[] TrigProblems =
        {
            Tuple.Create("sin(x)/cos(x)","tan"),
            Tuple.Create("1/csc(x)","sin"),
            Tuple.Create("1/cos(x)","sec"),
            Tuple.Create("opp/hyp","sin"),
            Tuple.Create("adj/hyp","cos"),
            Tuple.Create("opp/adj","tan"),
            Tuple.Create("sin^2(x) + cos^2(x)","1"),
        };

        public static Tuple<string, string>[] DerivativeTrig =
        {
            Tuple.Create("(d/dx)sin(x)","cos"),
            Tuple.Create("(d/dx)cosh(x)","sinh"),
            Tuple.Create("(d/dx)sinh(x)","cosh"),


        };

        public static Tuple<string, string> GetDerivative()
        {
            var coeff = _rand.Next(1, 12);
            var power = _rand.Next(2, 6);

            var aCoeff = coeff*power;
            var aPower = power-1;

            var q = string.Format("(d/dx)({0}x^{1})", coeff, power);
            var a = string.Format("{0}x{1}", aCoeff, 
                aPower == 0 ? "" : aPower.ToString(CultureInfo.InvariantCulture));
            
            return Tuple.Create(q, a);
        }

        public static Tuple<string, string> PickAny()
        {
            var p = _rand.Next(0, 3);
            switch (p)
            {
                case 0: return TrigProblems.Pick();
                case 1: return DerivativeTrig.Pick();
                default: return GetDerivative();
            }
        }

        public static Tuple<string, string> Pick(this Tuple<string, string>[] src)
        {
            var p = _rand.Next(0, src.Length);
            return src[p];
        }
    }
}
