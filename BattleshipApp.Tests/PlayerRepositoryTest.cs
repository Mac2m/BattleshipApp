using BattleshipApp.Data.Models;
using BattleshipApp.Data.Models.IRepo;
using BattleshipApp.Data.Models.Repo;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipApp.Tests
{
    [TestFixture]
    public class PlayerRepositoryTest
    {
        private IPlayerRepository _playerRepository;
        private IBoardRepository _boardRepository;
        private FiringBoard board;

        [SetUp]
        public void Init()
        {
            _boardRepository = new BoardRepository();
            board = _boardRepository.SetUp();
            _playerRepository = new PlayerRepository(_boardRepository);
            
        }

        [Test]
        public void IsPlayerCreatedTest()
        {
            var newPlayer = _playerRepository.CreateNewPlayer("New Player", board);

            Assert.IsNotNull(newPlayer);
            Assert.IsInstanceOf<Player>(newPlayer);
        }

        [Test]
        public void ShotMissTest()
        {
            var newPlayer = _playerRepository.CreateNewPlayer("New Player", board);
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            Coordinates coords = new Coordinates(rand.Next(1, 11), (char)rand.Next(65, 74));

            board.Fields.Where(x => x.Coordinates.Column == coords.Column && x.Coordinates.Row == coords.Row)
                .First()
                .ShipType = ShipType.None;
            var result = _playerRepository.Shot(newPlayer, coords.Row, coords.Column);

            Assert.AreEqual("Miss!", result);
        }

        [Test]
        public void ShotHitTest()
        {
            var newPlayer = _playerRepository.CreateNewPlayer("New Player", board);
            var field = board.Fields.Where(x => x.IsOccupiedByShip)
                .First();

            var result = _playerRepository.Shot(newPlayer, field.Coordinates.Row, field.Coordinates.Column);

            Assert.AreEqual("Hit!", result);
        }
    }
}
