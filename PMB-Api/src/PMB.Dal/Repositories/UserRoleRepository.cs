using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using PMB.Dal.Models;
using PMB.Dal.Models.Dals;

namespace PMB.Dal.Repositories
{
    public class UserRoleRepository: PmbRepository
    {
        public UserRoleRepository(string connectionString) : base(connectionString) { }

        public async Task<UserRoleDal> CreateUserRole(UserRoleDal dal, IDbTransaction transaction, CancellationToken token)
        {
            var sql = @"
                INSERT INTO user_roles
                    (login, role)
                VALUES(@Login, @Role)
                returning
                 login, role";

            var param = new
            {
                dal.Login,
                dal.Role
            };

            var c = await GetConnAsync();
            var command = new CommandDefinition(sql, param, transaction, cancellationToken: token);
            return await c.QueryFirstOrDefaultAsync<UserRoleDal>(command);
        }
    }
}