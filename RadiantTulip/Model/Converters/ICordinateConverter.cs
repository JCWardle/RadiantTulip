﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model.Converters
{
    internal interface ICordinateConverter
    {
        internal Position Convert(Position position);
    }
}