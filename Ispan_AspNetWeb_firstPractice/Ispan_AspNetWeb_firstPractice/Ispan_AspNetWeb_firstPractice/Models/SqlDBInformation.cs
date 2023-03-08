//using system;
//using system.collections.generic;
//using system.data.sqlclient;
//using system.io;
//using system.linq;
//using system.security.principal;
//using system.text;
//using system.threading.tasks;
//using system.xml.linq;

//namespace ispan.estore.sqldatalayer
//{
//    public class sqldbinformation
//    {

//        public ienumerable<dbcolumninfoentity> getcolumnsinf(int? categoryid = null, string prodname = null)
//        {
//            // 方便debug用的
//            sqldb.applicationname = "demo:search products";
//            // return null;

//            #region 生成 sql statement
//            string sql = $@"
//select table_name, column_name, data_type, character_maximum_length
//from information_schema.columns
//";

//            #endregion
//            return sqldb.search(sqldb.getconnection, dbcolumninfoentity.getinstance, sql);
//        }

//        public ienumerable<dbcolumninfoentity> getdisttableinf(int? categoryid = null, string prodname = null)
//        {
//            // 方便debug用的
//            sqldb.applicationname = "demo:search products";
//            // return null;

//            #region 生成 sql statement
//            string sql = $@"
//select distinct table_name
//from information_schema.columns
//";

//            #endregion
//            return sqldb.search(sqldb.getconnection, dbcolumninfoentity.getinstance, sql);
//        }

//        public static void savefile(string content, string filename)
//        {
//            filestream fs = new filestream($@"..\..\{filename}.txt", filemode.create);
//            streamwriter streamwriter = new streamwriter(fs, encoding.default);
//            streamwriter.write(content);
//            streamwriter.close();
//            fs.close();
//        }

//        public void infoanalysis()
//        {
//            var dbinfos = getcolumnsinf().tolist();
//            //dictionary<string, list<string>> dic = new dictionary<string, list<string>>();
//            // 1. 先得到所有的table名稱
//            list<string> tabledistict = new list<string>();
//            foreach (var dbinfo in dbinfos)
//            {
//                if (!tabledistict.contains(dbinfo.table_name))
//                {
//                    tabledistict.add(dbinfo.table_name);
//                }
//            }
//            // 2. 讀取樣本檔案
//            var a = dbinfos;
//            string filename = $@"c:\users\ttyeh\documents\github\ado_net_practice\ado_net_sol\ispan.estore.sqldatalayer\templaterepository.txt";
//            string filecontent = file.readalltext(filename);

//            // 3. 單一table的塞資料
//            string temptable = tabledistict[0];
//            list<string> columnsdistict = new list<string>();
//            foreach (var dbinfo in dbinfos)
//            {
//                if (temptable == dbinfo.table_name)
//                {
//                    columnsdistict.add(dbinfo.table_name);
//                }
//            }
//            // 3.1 改變tablename
//            // 3.2 改變parameters
//            // 3.3 改變entity的參數
//            filecontent.replace("userrepository", $"{temptable}repository");
//            filecontent.replace("users", $"{temptable}");
//            //(name, account, password, datebirth, height, email)
//            // 4. 塞n次
//            // 5. 存檔
//            savefile(filecontent, $"{temptable}repository");




//        }

//    }
//    public class dbcolumninfoentity
//    {
//        public string table_name { get; set; }
//        public string column_name { get; set; }
//        public string data_type { get; set; }
//        public int character_maximum_length { get; set; }

//        public static dbcolumninfoentity getinstance(sqldatareader reader)
//        {
//            var entity = new dbcolumninfoentity();
//            entity.table_name = reader.getfieldvalue<string>("table_name");
//            entity.column_name = reader.getfieldvalue<string>("column_name");
//            entity.data_type = reader.getfieldvalue<string>("data_type");
//            entity.character_maximum_length = reader.getfieldvalue<int>("character_maximum_length");


//            return entity;
//        }

//    }

//}
