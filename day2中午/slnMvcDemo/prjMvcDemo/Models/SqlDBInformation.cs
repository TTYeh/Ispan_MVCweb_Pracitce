using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ispan.Estore.SqlDataLayer
{
    public class SqlDBInformation
    {

        public IEnumerable<DbColumnInfoEntity> GetColumnsInf(int? categoryId = null, string prodName = null)
        {
            // 方便debug用的
            SqlDB.ApplicationName = "Demo:search products";
            // return null;

            #region 生成 sql statement
            string sql = $@"
SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS
";

            #endregion
            return SqlDB.Search(SqlDB.GetConnection, DbColumnInfoEntity.GetInstance, sql);
        }

        public IEnumerable<DbColumnInfoEntity> GetDistTableInf(int? categoryId = null, string prodName = null)
        {
            // 方便debug用的
            SqlDB.ApplicationName = "Demo:search products";
            // return null;

            #region 生成 sql statement
            string sql = $@"
SELECT distinct TABLE_NAME
FROM INFORMATION_SCHEMA.COLUMNS
";

            #endregion
            return SqlDB.Search(SqlDB.GetConnection, DbColumnInfoEntity.GetInstance, sql);
        }

        public static void SaveFile(string content, string fileName)
        {
            FileStream fs = new FileStream($@"..\..\{fileName}.txt", FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fs, Encoding.Default);
            streamWriter.Write(content);
            streamWriter.Close();
            fs.Close();
        }

        public void InfoAnalysis() {
            var dbInfos = GetColumnsInf().ToList();
            //Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
            // 1. 先得到所有的table名稱
            List<string> tableDistict = new List<string>();
            foreach (var dbInfo in dbInfos)
            {
                if (! tableDistict.Contains(dbInfo.TABLE_NAME))
                {
                    tableDistict.Add(dbInfo.TABLE_NAME);
                }
            }
            // 2. 讀取樣本檔案
            var a = dbInfos;
            string fileName = $@"C:\Users\TTyeh\Documents\GitHub\ADO_NET_Practice\ADO_NET_Sol\ispan.Estore.SqlDataLayer\templateRepository.txt";
            string fileContent = File.ReadAllText(fileName);

            // 3. 單一table的塞資料
            string tempTable = tableDistict[0];
            List<string> columnsDistict = new List<string>();
            foreach (var dbInfo in dbInfos)
            {
                if (tempTable == dbInfo.TABLE_NAME)
                {
                    columnsDistict.Add(dbInfo.TABLE_NAME);
                }
            }
            // 3.1 改變tableName
            // 3.2 改變parameters
            // 3.3 改變entity的參數
            fileContent.Replace("UserRepository", $"{tempTable}Repository");
            fileContent.Replace("Users", $"{tempTable}");
            //(Name, Account, Password, DateBirth, Height, Email)
            // 4. 塞N次
            // 5. 存檔
            SaveFile(fileContent, $"{tempTable}Repository");




        }

    }
    public class DbColumnInfoEntity {
        public string TABLE_NAME { get; set; }
        public string COLUMN_NAME { get; set; }
        public string DATA_TYPE { get; set; }
        public int CHARACTER_MAXIMUM_LENGTH { get; set; }
            
        public static DbColumnInfoEntity GetInstance(SqlDataReader reader)
        {
            var entity = new DbColumnInfoEntity();
            entity.TABLE_NAME = reader.GetFieldValue<string>("TABLE_NAME");
            entity.COLUMN_NAME = reader.GetFieldValue<string>("COLUMN_NAME");
            entity.DATA_TYPE = reader.GetFieldValue<string>("DATA_TYPE");
            entity.CHARACTER_MAXIMUM_LENGTH = reader.GetFieldValue<int>("CHARACTER_MAXIMUM_LENGTH");


            return entity;
        }

    }

}
