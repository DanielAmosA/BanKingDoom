using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using Server.Helpers.CustomException;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.CompilerServices;
using Server.Models.Settings;

namespace Server.Helpers.Service
{
    /// <summary>
    /// The class responsible for helper action.
    /// </summary>
    public class AppService
    {
        #region Hash
        //  dummy user => no storing data
        public class UserHasher { }

        // We will convert the received password to a hash.
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new FieldsException("Password cannot be null or empty");
            PasswordHasher<UserHasher> passwordHasher = new PasswordHasher<UserHasher>();
            var userHasher = new UserHasher();
            string hashedPassword = passwordHasher.HashPassword(userHasher, password);
            return hashedPassword;
        }

        // After conversion,
        // we will check if the entered password matches the password stored in the hash.
        public static bool VerifyPassword(string currentPassword, string storedHash)
        {
            if (string.IsNullOrEmpty(currentPassword))
                throw new FieldsException("Password cannot be null or empty");
            if (string.IsNullOrEmpty(storedHash))
                throw new FieldsException("Password cannot be null or empty");

            PasswordHasher<UserHasher> passwordHasher = new PasswordHasher<UserHasher>();
            var UserHasher = new UserHasher();
            var result = passwordHasher.VerifyHashedPassword(UserHasher, storedHash, currentPassword);

            return result == PasswordVerificationResult.Success;
        }

        #endregion

        #region Generic

        // Converts the DataTable data to a list of TModel objects.
        // It does this by searching each column in the DataTable
        // And filling in the appropriate properties in the TModel,
        // Including handling values ​​found as DBNull.

        public static List<TModel> ConvertDataTableToList<TModel>(DataTable dataTable) where TModel : new()
        {
            var list = new List<TModel>();

            foreach (DataRow row in dataTable.Rows)
            {
                TModel obj = new TModel();

                foreach (DataColumn column in dataTable.Columns)
                {
                    // Get the property from the class (including base classes)
                    var property = typeof(TModel).GetProperty(column.ColumnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                    if (property != null)
                    {
                        var value = row[column];

                        // Check if the value is DBNull
                        if (value == DBNull.Value)
                        {
                            property.SetValue(obj, null);
                        }
                        else
                        {
                            // If the property is an Enum, a suitable conversion
                            if (property.PropertyType.IsEnum)
                            {
                                // Parse the enum value properly from string or number
                                if (value is string stringValue)
                                {
                                    // Try parsing from string name
                                    property.SetValue(obj, Enum.Parse(property.PropertyType, stringValue, true));
                                }
                                else
                                {
                                    // If it's a number, get the enum name first then parse it
                                    string enumName = Enum.GetName(property.PropertyType, Convert.ToInt32(value))!;
                                    if (enumName != null)
                                    {
                                        property.SetValue(obj, Enum.Parse(property.PropertyType, enumName));
                                    }
                                }
                            }
                            else
                            {
                                property.SetValue(obj, Convert.ChangeType(value, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType));
                            }
                        }
                    }
                }

                // Add the object to the list
                list.Add(obj);
            }
            return list;
        }

        // Get results from the database.
        // A list of data (List<TModel>) on success.
        // An error message (string) on ​​failure.
        public static ResultSqlActionData<List<TModel>> CheckRes<TModel>(DataTable? res) 
                                                                        where TModel : new()
        {
            if(res != null)
            {
                if(res.Columns.Contains("ErrorMessage"))
                {
                    return ResultSqlActionData<List<TModel>>.InError(res.Rows[0]["ErrorMessage"].ToString()!);
                }
                return ResultSqlActionData<List<TModel>>.InSuccess(AppService.ConvertDataTableToList<TModel>(res));
            }
            return ResultSqlActionData<List<TModel>>.InError("Unexpected error");

        }

        // Get a result from a list and return the first row if it exists.

        public static ResultSqlActionData<TResult> ProcessResGetFirstRow<TResult>(ResultSqlActionData<List<TResult>> result)
                where TResult : class
        {
            return result.Data != null && result.Data.Count>0
                ? ResultSqlActionData<TResult>.InSuccess(result.Data.First())
                : ResultSqlActionData<TResult>.InError(result.Error!);
        }

        // We will create a generic function
        // that converts a Model
        // into a array of params of the desired model.
        public static SqlParameter[] GenerateSqlParameters<T>(T model)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            var parameters = new List<SqlParameter>();
            foreach (var property in properties)
            {
                var value = property.GetValue(model);

                if (value != null)
                {
                    string paramName = $"@{property.Name}";
                    parameters.Add(new SqlParameter(paramName, value));
                }
            }
            return parameters.ToArray();
        }

        // General customized log messages.
        public static void CreateLogInfo(string section, string desc)
        {
            Serilog.Log.Information($"{section} - {desc}");
        }

        public static void CreateErrorInfo(string section, string desc)
        {
            Serilog.Log.Error($"{section} - {desc}");
        }

        #endregion

    }
}
