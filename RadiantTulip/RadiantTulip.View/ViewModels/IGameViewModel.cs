using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace RadiantTulip.View.ViewModels
{
    public interface IGameViewModel
    {
        ICommand StopCommand { get; }
        ICommand PlayCommand { get; }
        Model.Game Game { get; }
        TimeSpan RunTime { get; }
        TimeSpan CurrentTime { get; set; }
        List<Player> SelectedPlayers { get; set; }
    }
}
