using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using MVCApp.Core.Domain;
using MVCApp.Infrastructure.Interfaces;

namespace MVCApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string GetConnectionString()
            => ConfigurationManager.ConnectionStrings["ExileRota"].ConnectionString;

        public async Task<User> GetByIdAsync(Guid userId)
        {
            string query = $"SELECT * FROM [dbo].[Users] WHERE [UserId] = '{userId}'";
            User user;
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                user = await connection.QuerySingleOrDefaultAsync<User>(query);
                connection.Close();
            }

            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            string query = $"SELECT * FROM [dbo].[Users] WHERE [Email] = '{email}'";
            User user;
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                user = await connection.QuerySingleOrDefaultAsync<User>(query);
                connection.Close();
            }

            return user;
        }

        public async Task<User> GetByIgnAsync(string ign)
        {
            string query = $"SELECT * FROM [dbo].[Users] WHERE [Ign] = '{ign}'";
            User user;
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                try
                {
                    user = await connection.QuerySingleOrDefaultAsync<User>(query);
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    connection.Close();
                }
            }

            return user;
        }

        // TODO : Add pagination
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            string query = $"SELECT * FROM [dbo].[Users]";
            IEnumerable<User> users;
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                users = await connection.QueryAsync<User>(query);
                connection.Close();
            }

            return users;
        }

        public async Task AddAsync(User user)
        {
            string query = "INSERT INTO [dbo].[Users]" +
                           "([UserId], [Ign], [Email], [Password], [Salt] ,[Role], [CreatedAt], [UpdatedAt]) " +
                           "VALUES " +
                           $"('{user.UserId}', '{user.Ign}', '{user.Email}', '{user.Password}', '{user.Salt}', '{user.Role}', '{user.CreatedAt}', '{user.UpdatedAt}') ";

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                await connection.QueryAsync(query);
                connection.Close();
            }
        }

        public async Task UpdateAsync(User user)
        {
            string query = $"UPDATE [dbo].[users] SET [UserId] = '{user.UserId}', " +
                           $"[Ign] = '{user.Ign}' " +
                           $"[Email] = '{user.Email}'" +
                           $"[Password] = '{user.Password}'" +
                           $"[Salt] = ''{user.Salt}" +
                           $"[Role] = '{user.Role}'" +
                           $"[CreatedAt] = '{user.CreatedAt}'" +
                           $"[UpdatedAt] = '{user.UpdatedAt}'" +
                           $"WHERE [userId] = '{user.UserId}'";

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                await connection.QueryAsync(query);
                connection.Close();
            }
        }

        public async Task DeleteAsync(User user)
        {
            string query = $"DELETE FROM [dbo].[Users] WHERE [UserId] = '{user.UserId}'";

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                await connection.QueryAsync(query);
                connection.Close();
            }
        }
    }
}