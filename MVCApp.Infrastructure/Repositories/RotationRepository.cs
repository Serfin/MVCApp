using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using MVCApp.Core.Domain;
using MVCApp.Core.Enums;
using MVCApp.Infrastructure.Interfaces;

namespace MVCApp.Infrastructure.Repositories
{
    // TODO : Add limit to queries, right now it will query all database
    public class RotationRepository : IRotationRepository
    {
        private string GetConnectionString()
            => ConfigurationManager.ConnectionStrings["ExileRota"].ConnectionString;

        public async Task AddAsync(Rotation rotation)
        {
            string query = "INSERT INTO[dbo].[Rotations] " +
                           "([RotationId], [Creator], [League], [Type], [Spots], [CreatedAt], [CreatorIgn])" +
                           "VALUES" +
                           $"('{rotation.RotationId}', " +
                           $"'{rotation.Creator}', " +
                           $"'{rotation.Type}', " +
                           $"'{rotation.League}', " +
                           $"'{rotation.Spots}', " +
                           $"'{rotation.CreatedAt}', " +
                           $"'{rotation.CreatorIgn}')";

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                await connection.QueryAsync(query);
                connection.Close();
            }
        }

        public async Task<IEnumerable<Rotation>> GetPageAsync(int page, int pageSize)
        {
            string query = $"SELECT * FROM [dbo].[Rotations] ORDER BY Spots DESC OFFSET({(page - 1) * pageSize}) ROWS FETCH NEXT({pageSize}) ROWS ONLY";
            IEnumerable<Rotation> rotations;

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                rotations = await connection.QueryAsync<Rotation>(query);
                connection.Close();
            }

            return rotations;
        }

        public async Task<IEnumerable<User>> GetRotationMembersAsync(Guid rotationId)
        {
            string query = $"SELECT UserId FROM [dbo].[RotationMembers] WHERE RotationId = '{rotationId}'";
            IEnumerable<User> rotationMembers;

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                rotationMembers = await connection.QueryAsync<User>(query);
                connection.Close();
            }

            return rotationMembers;
        }

        public async Task<Rotation> GetByRotationId(Guid rotationId)
        {
            string query = $"SELECT * FROM [dbo].[Rotations] WHERE [RotationId] = '{rotationId}'";
            Rotation rotation;
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                rotation = await connection.QuerySingleOrDefaultAsync<Rotation>(query);
                connection.Close();
            }

            return rotation;
        }


        public async Task<IEnumerable<Rotation>> GetByUserId(Guid creator)
        {
            string query = $"SELECT * FROM [dbo].[Rotations] WHERE [Creator] = '{creator}'";
            IEnumerable<Rotation> rotations;
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                rotations = await connection.QueryAsync<Rotation>(query);
                connection.Close();
            }

            return rotations;
        }

        public async Task<IEnumerable<Rotation>> GetByTypeAsync(RotationType type)
        {
            string query = $"SELECT * FROM [dbo].[Rotations] WHERE [Type] = '{type.ToString()}'";
            IEnumerable<Rotation> rotations;
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                rotations = await connection.QueryAsync<Rotation>(query);
                connection.Close();
            }

            return rotations;
        }

        public async Task JoinRotationAsync(Guid userId, Guid rotationId)
        {
            string query = "INSERT INTO [dbo].[RotationMembers] ([RotationId] ,[UserId])" +
                           $"VALUES('{rotationId}', '{userId}')";

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                await connection.QueryAsync(query);
                connection.Close();
            }
        }

        public async Task UpdateRotationAsync(Rotation rotation)
        {
            string query = $"UPDATE [dbo].[Rotations] SET [RotationId] = '{rotation.RotationId}', " +
                           $"[Creator] = '{rotation.Creator}'," +
                           $"[League] = '{rotation.League}'," +
                           $"[Type] = '{rotation.Type}'," +
                           $"[Spots] = '{rotation.Spots}'," +
                           $"[CreatedAt] = '{rotation.CreatedAt}'," +
                           $"[CreatorIgn] = '{rotation.CreatorIgn}'" +
                           $"WHERE [RotationId] = '{rotation.RotationId}'";

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                await connection.QueryAsync(query);
                connection.Close();
            }
        }

        public async Task DeleteRotationAsync(Guid rotationId)
        {
            string query = $"DELETE FROM [dbo].[Rotations] WHERE [RotationId] = '{rotationId}'";
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                await connection.QueryAsync(query);
                connection.Close();
            }
        }
    }
}