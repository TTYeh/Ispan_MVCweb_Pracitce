using ispan.Estore.SqlDataLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;

namespace Ispan_AspNetWeb_firstPractice.Models
{
    public class CCustomerRepo
    {
        private readonly string _tableName = "Customers";
        public Func<SqlConnection> funConn = SqlDB.GetConnection;
        public Func<SqlDataReader, CCustomerEntity> funcAssembler = CCustomerRepo.GetInstance;
        public List<CCustomerEntity> GetAll()
        //public string GetAll()
        {
            var result = new List<CCustomerEntity>();
            string temp = string.Empty;
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT * FROM Customers";
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CCustomerEntity c = new CCustomerEntity()
                        {
                            fId = (int)reader["fId"],
                            fName = reader["fName"].ToString(),
                            fPhone = reader["fPhone"].ToString(),
                            fEmail = reader["fEmail"].ToString(),
                            fAddress = reader["fAddress"].ToString(),
                            fPassword = reader["fPassword"].ToString()
                        };
                        result.Add(c);
                        temp += c.ToString();
                        temp += "<br>";
                    }
                }
            }
            return result;
        }
        public static CCustomerEntity GetInstance(SqlDataReader reader)
        {
            var entity = new CCustomerEntity();
            entity.fId = reader.GetFieldValue<int>("fId");
            entity.fName = reader.GetFieldValue<string>("fName");
            entity.fPhone = reader.GetFieldValue<string>("fPhone");
            entity.fEmail = reader.GetFieldValue<string>("fEmail");
            entity.fAddress = reader.GetFieldValue<string>("fAddress");
            entity.fPassword = reader.GetFieldValue<string>("fPassword");

            return entity;
        }
        public int Create(CCustomerEntity entity)
        {
            SqlDB.ApplicationName = "有尊嚴的_create";
            //SQL參數化
            string sql = $@"insert into {_tableName}
( fName, fPhone, fEmail,fAddress ,fPassword )
VALUES
( @fName, @fPhone, @fEmail,@fAddress ,@fPassword )";

            var parameters = SqlParameterBuilder.Create()
            //.AddInt("@fId", entity.fId)
            .AddNVarchar("@fName", entity.fName, 50)
            .AddNVarchar("@fPhone", entity.fPhone, 50)
            .AddNVarchar("@fEmail", entity.fEmail, 50)
            .AddNVarchar("@fAddress", entity.fAddress, 50)
            .AddNVarchar("@fPassword", entity.fPassword, 50)
            .Build();
            int newId = 0;
            try
            {
                newId = SqlDB.Create(SqlDB.GetConnection, sql, parameters);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Ix_Users"))
                {
                    throw new Exception("您新增的帳號已存在，請修改後再試一次", ex);
                }
            }
            return newId;
        }
        public CCustomerEntity GetById(int userId)
        {
            string sql = $"SELECT * FROM {_tableName} " +
                $"WHERE fId={userId}";
            return SqlDB.Get(funConn, funcAssembler, sql);
        }
        public int Delete(int userid)
        {
            //string sql = $@"DELETE FROM {_tableName} Where Id = {userId}";
            //int rowAffected = SqlDB.UpdateOrDelete(funConn, sql);
            //return rowAffected;

            string sql = $@"DELETE FROM {_tableName} Where fId = @fId";
            var parameters = SqlParameterBuilder.Create()
            .AddInt("@fId", userid)
            .Build();
            int rowAffected = SqlDB.UpdateOrDelete(funConn, sql, parameters);
            return rowAffected;
        }

        internal int Update(CCustomerEntity entity)
        {
            var sql = $@"UPDATE {_tableName} SET
fName=@fName,
fPhone = @fPhone,
fEmail = @fEmail,
fAddress = @fAddress,
fPassword = @fPassword
WHERE fId={entity.fId}";

            var parameters = SqlParameterBuilder.Create()
            .AddNVarchar("@fName", entity.fName, 50)
            .AddNVarchar("@fPhone", entity.fPhone, 50)
            .AddNVarchar("@fEmail", entity.fEmail, 50)
            .AddNVarchar("@fAddress", entity.fAddress, 50)
            .AddNVarchar("@fPassword", entity.fPassword, 50)
            .Build();

            int rowsAffected = SqlDB.UpdateOrDelete(funConn, sql, parameters);
            return rowsAffected;
        }

    }
}