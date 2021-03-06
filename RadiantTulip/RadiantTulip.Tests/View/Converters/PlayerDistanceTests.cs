﻿using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.View.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests.View.Converters
{
    [TestFixture]
    public class PlayerDistanceTests
    {
        [Test]
        public void Player_Has_No_Positions()
        {
            var player = new Player { Positions = new LinkedList<Position>() };
            var converter = new PlayerDistance();
            var selectedPlayers = new ObservableCollection<Player> { player };
            var parameters = new object[] { selectedPlayers };

            var result = (double)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void Player_Without_Current_Position()
        {
            var player = new Player { CurrentPosition = null };
            var converter = new PlayerDistance();
            var selectedPlayers = new ObservableCollection<Player> { player };
            var parameters = new object[] { selectedPlayers };

            var result = (double)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void Player_With_Two_Positions()
        {
            var player = new Player()
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 10, Y = 10, TimeStamp = TimeSpan.Zero });
            player.Positions.AddLast(new Position { X = 20, Y = 20, TimeStamp = TimeSpan.FromMilliseconds(10) });
            player.CurrentPosition = player.Positions.Last;
            var converter = new PlayerDistance();
            var selectedPlayers = new ObservableCollection<Player> { player };
            var parameters = new object[] { selectedPlayers };

            var result = (double)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(0.14, result);
        }

        [Test]
        public void Ignores_Positions_After_Current_Time()
        {
            var player = new Player()
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 10, Y = 10, TimeStamp = TimeSpan.Zero });
            player.Positions.AddLast(new Position { X = 20, Y = 20, TimeStamp = TimeSpan.FromMilliseconds(10) });
            player.Positions.AddLast(new Position { X = 30, Y = 30, TimeStamp = TimeSpan.FromMilliseconds(20) });
            player.CurrentPosition = player.Positions.First.Next;
            var converter = new PlayerDistance();
            var selectedPlayers = new ObservableCollection<Player> { player };
            var parameters = new object[] { selectedPlayers };

            var result = (double)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(0.14, result);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Player_Distance_Convert_Back_Not_Implemented()
        {
            var converter = new PlayerDistance();
            converter.ConvertBack(null, null, null, null);
        }

        [Test]
        public void Player_Distance_No_Players_Selected()
        {
            var converter = new PlayerDistance();
            var selectedPlayers = new ObservableCollection<Player>();
            var parameters = new object[] { selectedPlayers, 10d };

            var result = (double)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(0, result);
        }
    }
}
