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
        Small = 20,
        [Description("M")]
        Medium = 30,
        [Description("L")]
        Large = 40,
        [Description("XL")]
        ExtraLarge = 50
    }
}
