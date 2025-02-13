using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Server.DALInterfaces;
using Server.Helpers.CustomException;
using Server.Helpers.DB;
using Server.Helpers.DBInterfaces;
using Server.Helpers.General;
using Server.Helpers.Service;
using Server.Models.Entity;
using Server.Models.Settings;
using System.Data;

namespace Server.DAL
{
    /// <summary>
    /// The class responsible for Calling the procedures and their data 
    /// According to the Register User area.
    /// </summary>
    public class RegisterUserDAL : IRegisterUserDAL
    {
        private readonly string connectionString;
        private readonly IDataHelper dataHelper;
        
        public RegisterUserDAL(IDataHelper dataHelper, IOptions<DbConfig> dbConfig)
        {
            // Get ConnectionString
            connectionString = dbConfig.Value.ConnectionString;
            // Calling and executing helper functions for SQL services.
            this.dataHelper = dataHelper;
        }

        public async Task<ResultSqlActionData<List<RegisterUser>>> RegisterUserInsert(RegisterUser registerUser)
        {
            SqlParameter[] sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@HebrewFullName", registerUser.HebrewFullName);
            sqlParameters[1] = new SqlParameter("@EnglishFullName", registerUser.EnglishFullName);
            sqlParameters[2] = new SqlParameter("@BirthdayDate", registerUser.BirthdayDate);
            sqlParameters[3] = new SqlParameter("@TazEncryption", registerUser.Taz);
            sqlParameters[4] = new SqlParameter("@PasswordHash", registerUser.Password);
            sqlParameters[5] = new SqlParameter("@CreatedAt", registerUser.CreatedAt);
            DataTable? res = await dataHelper.ExecSPWithRes(connectionString, SPNames.REGISTER_USER_INSERT, sqlParameters);
            return AppService.CheckRes<RegisterUser>(res);
        }

        public async Task<ResultSqlActionData<List<RegisterUser>>> RegisterUserGetUserByTaz(RegisterUserBasic registerUserBasic)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@TazEncryption", registerUserBasic.Taz);
            DataTable? res = await dataHelper.ExecSPWithRes(connectionString, SPNames.REGISTER_USER_GET_USER_BY_TAZ, sqlParameters);
            return AppService.CheckRes<RegisterUser>(res);
        }
    }
}
