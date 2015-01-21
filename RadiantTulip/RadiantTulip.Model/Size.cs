using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model
{
    public enum Size
    {
        [Description("S")]
        Small = 3,
        [Description("M")]
        Medium = 5,
        [Description("L")]
        Large = 7,
        [Description("XL")]
        ExtraLarge = 9
    }
}
