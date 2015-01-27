using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<Player> SelectedPlayers { get; set; }
        Action UpdateView { get; set; }
        ICommand PlayCommand { get; }
        ICommand PauseCommand { get; }
        ICommand ForwardCommand { get; }
        ICommand RewindCommand { get; }
        ICommand StopCommand { get; }
        ICommand PlayerSelectedCommand { get; }
        ICommand PlayerCheckedCommand { get; }
    }
}
