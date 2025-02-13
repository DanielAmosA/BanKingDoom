using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Server.Helpers.DBInterfaces;
using Server.Helpers.Service;
using Server.Models.Settings;
using System.Data;

namespace Server.Helpers.DB
{
    /// <summary>
    /// The class responsible for Sql action.
    /// </summary>
    public class DataHelper : IDataHelper
    {

        /// <summary>
        /// Executing a procedure that does return scaler
        /// </summary>
        /// <param name="storedProcedureName">The stored Procedure Name.</param>
        /// <param name="parameters">The stored Procedure Paramters.</param>
        public async Task<object?> ExecSPAsScalar(string connectionString, string storedProcedureName, SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        //Checking if parameters exist and adding them accordingly
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                            // Retrieving all parameters as a string.

                            List<string> paramsList = parameters.Select(p => $"{p.ParameterName}={p.Value}").ToList();
                            string paramsString = string.Join(", ", paramsList);
                            AppService.CreateLogInfo("DataHelper", $"Executing stored procedure {storedProcedureName} with parameters: {paramsString}");
                        }

                        else
                        {
                            AppService.CreateLogInfo("DataHelper", $"Executing stored procedure {storedProcedureName}");
                        }

                        //Executing the procedure and check if get value
                        return command.ExecuteScalar();

                    }
                }
            }
            catch (Exception ex)
            {
                AppService.CreateLogInfo("DataHelper", ex.Message);
                return null;
            }

        }

        /// <summary>
        /// Executing a procedure that does not return data
        /// </summary>
        /// <param name="connectionString">The Connection String DB.</param>
        /// <param name="storedProcedureName">The stored Procedure Name.</param>
        /// <param name="parameters">The stored Procedure Paramters.</param>
        public async Task<bool> ExecSPWithoutRes(string connectionString, string storedProcedureName, SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        //Checking if parameters exist and adding them accordingly
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                            // Retrieving all parameters as a string.

                            List<string> paramsList = parameters.Select(p => $"{p.ParameterName}={p.Value}").ToList();
                            string paramsString = string.Join(", ", paramsList);
                            AppService.CreateLogInfo("DataHelper", $"Executing stored procedure {storedProcedureName} with parameters: {paramsString}");
                        }

                        else
                        {
                            AppService.CreateLogInfo("DataHelper", $"Executing stored procedure {storedProcedureName}");
                        }

                        //Executing the procedure and check if affected
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            AppService.CreateLogInfo("DataHelper", $"Procedure executed successfully. Rows affected: {rowsAffected}");
                            return true;
                        }
                        else
                        {

                            AppService.CreateLogInfo("DataHelper", "No rows were affected by the procedure.");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppService.CreateLogInfo("DataHelper", ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Executing a procedure that does return data
        /// </summary>
        /// <param name="storedProcedureName">The stored Procedure Name.</param>
        /// <param name="parameters">The stored Procedure Paramters.</param>

        public async Task<DataTable?> ExecSPWithRes(string connectionString, string storedProcedureName, SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        string paramsString = "";

                        //Checking if parameters exist and adding them accordingly
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);

                            // Retrieving all parameters as a string.
                            List<string> paramsList = parameters.Select(p => $"{p.ParameterName}={p.Value}").ToList();
                            paramsString = string.Join(", ", paramsList);
                            AppService.CreateLogInfo("DataHelper", $"Executing stored procedure {storedProcedureName} with parameters: {paramsString}");
                        }

                        else
                        {
                            AppService.CreateLogInfo("DataHelper", $"Executing stored procedure {storedProcedureName}");
                        }

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();

                            //We will fill a general table to represent the data we received
                            await Task.Run(() => dataAdapter.Fill(dataTable));
                            if (paramsString != "")
                            {
                                AppService.CreateLogInfo("DataHelper", $"Procedure {storedProcedureName} completed successfully. Parameters used: {paramsString}");
                            }

                            else
                            {
                                AppService.CreateLogInfo("DataHelper", $"Procedure {storedProcedureName} completed successfully");
                            }

                            return dataTable;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                AppService.CreateLogInfo("DataHelper", ex.Message);
                return null;
            }

        }
    }
}
