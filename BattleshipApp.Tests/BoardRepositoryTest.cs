using BattleshipApp.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipApp.Tests
{
    [TestFixture]
    public class BoardRepositoryTest
    {
        private IBoardRepository _boardRepository;
        private IPlayerRepository _playerRepository;

        [SetUp]
        public void Init()
        {
            _boardRepository = new BoardRepository();
            _playerRepository = new PlayerRepository(_boardRepository);
        }

        [Test]
        public void IsThereAFiringBoardTest()
        {
            var firingBoard = _boardRepository.SetUp();

            Assert.IsNotNull(firingBoard);
            Assert.IsInstanceOf<FiringBoard>(firingBoard);
        }

        [Test]
        public void IsShipsRandomlyPlacedTest()
        {
            var firingBoard = _boardRepository.SetUp();

            bool shipsOnBoard = firingBoard.Fields.Any(x => x.ShipType != ShipType.None);

            Assert.IsTrue(shipsOnBoard);
        }

        [Test]
        public void IsGameOverTest()
        {
            var firingBoard = _boardRepository.SetUp();
            var player = _playerRepository.CreateNewPlayer("TestPlayer", firingBoard);

            firingBoard.Ships.ForEach(s => s.Hits = s.Width);
            bool isGameOver = _boardRepository.CheckGameOver(player);

            Assert.IsTrue(isGameOver);
        }
    }
}
