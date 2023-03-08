//using ispan.Estore.Bll.Core;
//using ispan.Estore.Bll.Interface;
//using ispan.Estore.SqlDataLayer;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;

//namespace ispan.Estore.SqlDataLayer
//{
//    public class UserRepository : IUserRepository
//    {
//        private readonly string _tableName = "Users";
//        public Func<SqlConnection> funConn = SqlDB.GetConnection;
//        public Func<SqlDataReader, UserEntity> funcAssembler = UserRepository.GetInstance;

//        public static UserEntity GetInstance(SqlDataReader reader)
//        {
//            var name = reader.GetFieldValue<string>("Name");
//            var account = reader.GetFieldValue<string>("Account");
//            var password = reader.GetFieldValue<string>("Password");

//            var entity = new UserEntity(name, account, password);
//            entity.Id = reader.GetFieldValue<int>("Id");
//            entity.DateBirth = reader.GetFieldValue<DateTime?>("DateBirth");
//            entity.Height = reader.GetFieldValue<int?>("Height");
//            entity.Email = reader.GetFieldValue<string>("Email");

//            return entity;
//        }



//        public int Create(UserEntity entity)
//        {
//            SqlDB.ApplicationName = "有尊嚴的_create";
//            SQL參數化
//            string sql = $@"insert into {_tableName}
//(Name, Account, Password, DateBirth,Height ,Email )
//VALUES
//(@Name, @Account, @Password, @DateBirth,@Height ,@Email )";

//            var parameters = SqlParameterBuilder.Create()
//            .AddNVarchar("@Name", entity.Name, 50)
//            .AddNVarchar("@Account", entity.Account, 50)
//            .AddNVarchar("@Password", entity.Password, 50)
//            .AddDateTime("@DateBirth", entity.DateBirth)
//            .AddInt("@Height", entity.Height)
//            .AddNVarchar("@Email", entity.Email, 50)
//            .Build();
//            int newId = 0;
//            try
//            {
//                SqlDB.Create(SqlDB.GetConnection, sql, parameters);
//            }
//            catch (SqlException ex)
//            {
//                if (ex.Message.Contains("Ix_Users"))
//                {
//                    throw new Exception("您新增的帳號已存在，請修改後再試一次", ex);
//                }

//            }


//            return newId;
//        }


//        public int Delete(int userId)
//        {
//            string sql = $@"DELETE FROM {_tableName} Where Id = {userId}";
//            int rowAffected = SqlDB.UpdateOrDelete(funConn, sql);
//            return rowAffected;

//            string sql = $@"DELETE FROM {_tableName} Where Id = @Id";
//            var parameters = SqlParameterBuilder.Create()
//            .AddInt("@Id", userId)
//            .Build();
//            int rowAffected = SqlDB.UpdateOrDelete(funConn, sql, parameters);
//            return rowAffected;

//        }


//        public int Update(UserEntity entity)
//        {
//            string sql = $@"DELETE FROM {_tableName} Where Id = {userId}";
//            int rowAffected = SqlDB.UpdateOrDelete(funConn, sql);
//            return rowAffected;

//            string sql = $@"Update {_tableName} Set
//Name=@Name,
//Account=@Account,
//Password=@Password,
//DateBirth=@DateBirth,
//Height=@Height,
//Email=@Email
//Where Id = {entity.Id}";

//            var parameters = SqlParameterBuilder.Create()
//            .AddNVarchar("@Name", entity.Name, 50)
//            .AddNVarchar("@Account", entity.Account, 50)
//            .AddNVarchar("@Password", entity.Password, 50)
//            .AddInt("@Height", entity.Height)
//            .AddDateTime("@DateBirth", entity.DateBirth)
//            .AddNVarchar("@Email", entity.Email, 256)
//            .Build();
//            int rowAffected = SqlDB.UpdateOrDelete(funConn, sql, parameters);
//            return rowAffected;

//        }

//        public IEnumerable<UserEntity> Search(string name, string account, string email)
//        {
//            var data = new UserEntity("Temp", "Temp", "Temp");

//            #region 生成 sql statement
//            string sql = $@"
//SELECT *
//FROM {_tableName}";

//            #region 生成 where
//            string where = string.Empty;
//            var parameters = new List<SqlParameter>();

//            if (string.IsNullOrEmpty(name) == false)
//            {
//                where += $" AND Name LIKE '%' +@Name +'%'";
//                parameters.Add(new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 50) { Value = name });
//            }
//            if (string.IsNullOrEmpty(account) == false)
//            {
//                where += $" AND Account LIKE '%' +@Account +'%'";
//                parameters.Add(new SqlParameter("@Account", System.Data.SqlDbType.NVarChar, 50) { Value = account });
//            }
//            if (string.IsNullOrEmpty(email) == false)
//            {
//                where += $" AND Email LIKE '%' +@Email +'%'";
//                parameters.Add(new SqlParameter("@Email", System.Data.SqlDbType.NVarChar, 50) { Value = email });
//            }
//            where = where == string.Empty ? where : " WHERE " + where.Substring(5);
//            sql += where;
//            #endregion
//            #endregion
//            return SqlDB.Search(funConn, funcAssembler, sql, parameters.ToArray());
//        }

//        public UserEntity GetUser(int userId)
//        {
//            string sql = $"SELECT * FROM {_tableName} " +
//                $"WHERE Id={userId}";
//            return SqlDB.Get(funConn, funcAssembler, sql);
//        }

//        public UserEntity GetByAccount(string account)
//        {
//            string sql = $"SELECT * FROM {_tableName} " +
//    $"WHERE Account=@Account";
//            SqlParameter[] parameters = new SqlParameter[] {
//            new SqlParameter("@Account", System.Data.SqlDbType.NVarChar, 50) { Value = account}
//            };
//            var parameters = SqlParameterBuilder.Create()
//.AddNVarchar("@Account", account, 50)
//.Build();
//            return SqlDB.Get(funConn, funcAssembler, sql, parameters);
//        }
//    }

//    public class UserEntity
//    {
//        public int Id { get; set; }
//        public string Name { get; }
//        public string Account { get; }
//        public string Password { get; }
//        public DateTime? DateOfBirth { get; set; }
//        public int? Height { get; set; }
//        public string Email { get; set; }

//        public UserEntity(string name, string account, string password)
//        {
//            pre conditions
//            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
//            if (string.IsNullOrEmpty(account)) throw new ArgumentNullException(nameof(account));
//            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

//            this.Name = name;
//            this.Account = account;
//            this.Password = password;
//        }

//    }


//}
