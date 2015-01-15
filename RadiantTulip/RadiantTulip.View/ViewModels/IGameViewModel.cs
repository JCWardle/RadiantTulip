using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace RadiantTulip.View.ViewModels
{
    public interface IGameViewModel
    {
        ICommand StopCommand { get; }
        ICommand PlayCommand { get; }
        Model.Game Game { get; set; }
        TimeSpan RunTime { get; }
        TimeSpan CurrentTime { get; }
        double CurrentTimeMilliseconds { get; set; }
        List<Player> SelectedPlayers { get; set; }
    }
}
