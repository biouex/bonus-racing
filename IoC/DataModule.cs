using Ninject.Modules;
using Ninject.Extensions;
using Ninject.Extensions.Factory;
using Ninject.Extensions.Conventions;
using BonusRacing.DataAccess;
using BonusRacing.DataAccess.Repositories;

namespace BonusRacing.IoC
{
    public class DataModule : NinjectModule
    {
        private readonly string _connectionString;

        public DataModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override void Load()
        {
            var databaseProvider = new MongoDatabaseProvider(_connectionString, "BonusRacing");

            Bind<MongoDatabaseProvider>().ToConstant(databaseProvider).InSingletonScope();

            Kernel.Bind(
                x => x.From(typeof(BaseRepository).Assembly)
                .Select(t => t.Name.EndsWith("Repository"))
                .BindAllInterfaces()
                .Configure(i => i.InTransientScope())
                );
        }

        
    }
}
