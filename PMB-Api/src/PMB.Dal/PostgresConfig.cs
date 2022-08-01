using Npgsql;
using Npgsql.NameTranslation;
using PMB.Dal.Models;
using PMB.Dal.Models.Dals;

namespace PMB.Dal
{
    public class PostgresConfig
    {
        private static readonly INpgsqlNameTranslator Translator = new NpgsqlSnakeCaseNameTranslator();

        public static void ConfigureTypeMapping()
        {
            var mapper = NpgsqlConnection.GlobalTypeMapper;
            mapper.MapComposite<V1ForkDal>("fork_dal_v1", Translator);
            mapper.MapComposite<V1BetDal>("bet_dal_v1", Translator);
        }
    }
}
