using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRegister.Common
{
    public class MySqlCom
    {
        public MySqlCom(string connstr)
        {
            ConnectionStr = connstr;

            log4net.Config.XmlConfigurator.Configure();
            logger = log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            SelectData = new DataTable();
        }

        private log4net.ILog logger;
        public string ConnectionStr { get; private set; }

        /// <summary>
        /// SELECT取得データ
        /// </summary>
        public DataTable SelectData { get; private set; }

        public bool Select(string sql)
        {
            if(string.IsNullOrEmpty(ConnectionStr) 
                || string.IsNullOrEmpty(sql))
            {
                return false;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionStr))
                {
                    conn.Open();

                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(SelectData);
                } 
            }
            catch (Exception ex)
            {
                logger.Error("SELECT時に例外発生.", ex);
                return false;
            }

            return true;
        }

        public bool Execute(string sql)
        {
            using (MySqlCommand command = new MySqlCommand())
            {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = ConnectionStr;

                // トランザクションを開始します。
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    command.CommandText = sql;
                    command.Connection = conn;
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    logger.Error("登録/更新処理に失敗.", ex);
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return true;
        }
    }
}
