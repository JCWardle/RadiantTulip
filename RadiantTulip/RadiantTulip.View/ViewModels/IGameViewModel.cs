using RadiantTulip.Model;
using RadiantTulip.View.Drawing;
using RadiantTulip.View.Drawing.VisualAffects;
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
        ObservableCollection<Group> Groups { get; }
        List<IVisualAffect> VisualAffects { get; }
        Group SelectedGroup { get; set; }
        double CurrentTimeMilliseconds { get; set; }
        double FrameIncrement { get; }
        bool Playing { get; }
        //Speed Tuner
        TimeSpan SpeedTuner { get; }
        Action UpdateView { get; set; }
        IEnumerable<PlayerAffect> PlayerAffects { get; }
        ICommand PlayCommand { get; }
        ICommand PauseCommand { get; }
        ICommand ForwardCommand { get; }
        ICommand RewindCommand { get; }
        ICommand StopCommand { get; }
        ICommand PlayerSelectedCommand { get; }
        ICommand PlayerCheckedCommand { get; }
        ICommand PlayerUncheckedCommand { get; }
        ICommand ColourChangedCommand { get; }
        ICommand CreateGroupCommand { get; }
        ICommand ShapeChangedCommand { get; }
        ICommand ResizeCommand { get; }
        ICommand SelectionTabLoadedCommand { get; }
        ICommand VisibilityChangedCommand { get; }
        ICommand VisualAffectOptionSelectedCommand { get; }
    }
}
