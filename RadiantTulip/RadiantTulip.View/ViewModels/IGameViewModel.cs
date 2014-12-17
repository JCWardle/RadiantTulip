using System.Windows.Input;

namespace RadiantTulip.View.ViewModels
{
    public interface IGameViewModel
    {
        ICommand UpdateGame { get; }
        Model.Game Game { get; set; }
    }
}
