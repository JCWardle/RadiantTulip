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
        [Description("Small")]
        Small = 5,
        [Description("Medium")]
        Medium = 10,
        [Description("Large")]
        Large = 15,
        [Description("Extra-Large")]
        ExtraLarge = 20
    }
}
