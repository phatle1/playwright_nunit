using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;
using PortalTalk.AutomationTest.BackendHelper.DatabaseLayer;
using Newtonsoft.Json;

namespace PortalTalk.AutomationTest.BackendHelper.DatabaseLayer.DAO
{
    public class MAC_DAO
    {
        public async Task<Dictionary<string, object>> GetCatIdAndWorkspaceIdFromDatabase(SqlConnection sqlConnection, string CategoryName)
        {
            DatabaseSQLConnection sqlConn = new();

            string query = $"select CA.ID as catID, AC.ID as workspaceID, AC.Name from CustomerApplications CA, ApplicationCategories AC where CA.ApplicationCategoryId = AC.Id and AC.Name like '{CategoryName}'";
            using SqlCommand command = new(query, sqlConnection);
            var resultAsDict = await sqlConn.ExecuteScalarAsDictionaryAsync(query);
            return resultAsDict;
        }

        public async Task<string> GetTop1UnblockedUser(SqlConnection sqlConnection)
        {
            DatabaseSQLConnection sqlConn = new();
            // exclude owner of the created workspace
            string query = "select top 1 * from ccnusers where userstatus in ('Active','Inactive','Invited') ";
            using SqlCommand command = new(query, sqlConnection);
            var resultAsDict = await sqlConn.ExecuteScalarAsDictionaryAsync(query);
            var resultAsJson = JsonConvert.SerializeObject(resultAsDict, Formatting.Indented);
            return resultAsJson;
        }
    }
}