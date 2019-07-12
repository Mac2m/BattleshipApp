using BattleshipApp.Data.Models.IRepo;
using BattleshipApp.Data.Models.Repo;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipApp
{
    public class DIConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IBoardRepository>().To<BoardRepository>().InSingletonScope();
            Bind<IPlayerRepository>().To<PlayerRepository>();
        }
    }
}
