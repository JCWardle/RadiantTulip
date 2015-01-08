using System;
using System.Windows.Input;

namespace RadiantTulip.View.ViewModels
{
    public interface IGameViewModel
    {
        ICommand UpdateCommand { get; }
        ICommand StopCommand { get; }
        ICommand PlayCommand { get; }
        Model.Game Game { get; set; }
        TimeSpan RunTime { get; }
        TimeSpan CurrentTime { get; set; }
    }
}
