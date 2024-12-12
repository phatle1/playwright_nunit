using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;
namespace PortalTalk.AutomationTest.BackendHelper.DatabaseLayer
{
    public class DatabaseSQLConnection
    {
        private readonly string _connectionString;

        public DatabaseSQLConnection()
        {
            // Build connection string from config
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "br7xlhkcoz.database.windows.net",
                InitialCatalog = "ManagementDatabaseTrunk",
                UserID = "EsCcnPortal",
                Password = "Elephant12",
                TrustServerCertificate = true,
                Encrypt = true
            };

            _connectionString = builder.ConnectionString;
        }

        public async Task<SqlConnection> CreateConnectionAsync()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Connection string is not initialized");
            }
            var connection = new SqlConnection(_connectionString);
            try
            {
                await connection.OpenAsync();
                return connection;
            }
            catch (Exception ex)
            {
                await connection.DisposeAsync();
                throw new InvalidOperationException($"Error: {ex}");
            }
        }

        public async Task<string> GetCatIdAndWorkspaceIdFromDatabase(string CatName)
        {
            using var connection = await CreateConnectionAsync();
            string query = $"select CA.*, AC.*, AC.Name from CustomerApplications CA, ApplicationCategories AC where CA.ApplicationCategoryId = AC.Id and AC.Name like '{CatName}'";
            using SqlCommand command = new(query, connection);
            var test = await command.ExecuteScalarAsync();
            return "";
        }

        public async Task<Dictionary<string, object>> ExecuteScalarAsDictionaryAsync(string sqlQuery, Dictionary<string, object> parameters = null)
        {
            var resultsAsDict = new Dictionary<string, object>();
            var conn = await CreateConnectionAsync();
            using var command = new SqlCommand(sqlQuery, conn);
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
            }
            var result = await command.ExecuteReaderAsync();

            while (await result.ReadAsync())
            {
                var row = new Dictionary<string, object>();

                for (int i = 0; i < result.FieldCount; i++)
                {
                    string columnName = result.GetName(i);
                    object value = result.IsDBNull(i) ? null : result.GetValue(i);
                    row[columnName] = value;
                }
                string key = row[row.Keys.First()].ToString() ?? "";
                resultsAsDict[key] = row;
            }
            return resultsAsDict;

        }
    }
}