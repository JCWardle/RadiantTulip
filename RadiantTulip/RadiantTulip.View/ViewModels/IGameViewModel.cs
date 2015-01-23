﻿using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace RadiantTulip.View.ViewModels
{
    public interface IGameViewModel
    {
        Model.Game Game { get; }
        TimeSpan RunTime { get; }
        string CurrentTime { get; }
        List<Player> SelectedPlayers { get; set; }
    }
}
