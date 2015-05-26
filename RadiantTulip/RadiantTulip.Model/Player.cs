using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace RadiantTulip.Model
{
    public class Player : INotifyPropertyChanged
    {
        public Team Team { get; set; }
        private bool _visible;
        public bool Visible 
        { 
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
                OnPropertyChanged("Visible");
            }
        }
        public LinkedList<Position> Positions { get; set; }
        public LinkedListNode<Position> CurrentPosition { get; set; }
        public string Name { get; set; }
        private Color _colour;
        public Color Colour
        {
            get
            {
                return _colour;
            }
            set
            {
                _colour = value;
                OnPropertyChanged("Colour");
            }
        }
        public Size Size { get; set; }
        public PlayerShape Shape { get; set; }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
