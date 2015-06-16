using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model
{
    public static class Size
    {
        private const string SMALL = "Small";
        private const string MEDIUM = "Medium";
        private const string LARGE = "Large";
        private const string EXTRA_LARGE = "Extra Large";

        private static Dictionary<string, int> _sizes = new Dictionary<string, int>()
            {
                { SMALL, 20 },
                { MEDIUM, 30 },
                { LARGE, 40 },
                { EXTRA_LARGE, 50 }
            };
        public static int Small
        {
            get { return _sizes[SMALL]; }
        }

        public static int Medium
        {
            get {return _sizes[MEDIUM]; }
        }

        public static int Large
        {
            get { return  _sizes[LARGE]; }
        }

        public static int ExtraLarge
        {
            get { return _sizes[EXTRA_LARGE]; }
        }

        internal static void SetupSizes(int? small = 20, int? medium = 30, int? large = 40, int? extraLarge = 50)
        {
            if (small.HasValue)
                _sizes[SMALL] = small.Value;
            if (medium.HasValue)
                _sizes[MEDIUM] = medium.Value;
            if (large.HasValue)
                _sizes[LARGE] = large.Value;
            if (extraLarge.HasValue)
                _sizes[EXTRA_LARGE] = extraLarge.Value;
        }

        public static IList<String> GetOptions()
        {
            return _sizes.Keys.ToList();
        }

        public static int GetSize(string size)
        {
            return _sizes[size];
        }
    }
}
