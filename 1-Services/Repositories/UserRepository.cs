using Dapper;
using Data.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private IConfiguration _config;

        public UserRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<List<User>> Get()
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                string sql = "SELECT * FROM [User]";
                var retorno = await conexao.QueryAsync<User>(sql);
                return retorno.ToList();
            }
        }

        public async Task<User>  Post(User user)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {                
                string sql = @"DECLARE @IdUser int = 0;
                SELECT @IdUser = Id FROM [user]
                WHERE EMAIL = @Email
                IF @IdUser = 0
                    BEGIN
                        INSERT INTO[dbo].[User]([Name], [Email]) VALUES(@Name, @Email);
                    END
                 SELECT * FROM [user]
                 WHERE EMAIL = @Email";
                return await conexao.QueryFirstAsync<User>(sql, user);              
            }
        }

    }   
}
