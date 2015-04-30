﻿using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RadiantTulip.View.Game.VisualAffects
{
    public class Tadpole : IVisualAffect
    {
        private const int MIN_POINT_INCREMENT = 10; //Milliseconds
        private const int TADPOLE_LENGTH = 10;
        private Player _player;
        private Ground _ground;

        public Tadpole(Player player, Ground ground)
        {
            _player = player;
            _ground = ground;
        }

        public void Draw(Canvas canvas)
        {
            var currentPosition = _player.CurrentPosition;

            for (var i = 0; i < TADPOLE_LENGTH; i++)
            {
                currentPosition = _player.Positions.OrderByDescending(p => p.TimeStamp.TotalMilliseconds)
                    .FirstOrDefault(p => p.TimeStamp.TotalMilliseconds < currentPosition.TimeStamp.TotalMilliseconds - MIN_POINT_INCREMENT);
                if (currentPosition == null)
                    return;

                var transformedPosition = currentPosition.TransformToCanvas(_ground, canvas);
                var x = transformedPosition.X - (double)_player.Size / 2;
                var y = transformedPosition.Y - (double)_player.Size / 2;

                var color = _player.Colour;
                color.A = 50;

                var circle = new Ellipse
                {
                    Width = (int)_player.Size,
                    Height = (int)_player.Size,
                    Margin = new Thickness { Left = x, Top = y },
                    Fill = new SolidColorBrush(color)
                };

                canvas.Children.Add(circle);
            }
        }


        public bool AffectFor(List<Player> players, GroupAffect affect)
        {
            return false;
        }

        public bool AffectFor(List<Player> players, PlayerAffect affect)
        {
            return players.Contains(_player) && affect == PlayerAffect.Tadpole;
        }
    }
}
