using EmployeeRegister.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRegister.Model
{
    public class DepartmentInfo
    {
        public DepartmentInfo()
        {
            DEPARTMENT_CODE = -1;
        }

        public int DEPARTMENT_CODE { get; set; }
        public string DEPARTMENT_NAME { get; set; }

        public List<DepartmentInfo> GetAllInfoFromDb()
        {
            var constr = Settings.Default.DB_CONNECTION_STR;
            var db = new Common.MySqlCom(constr);
            var sql = CreateSelAllSql();
            var selRet = db.Select(sql);
            var ret = new List<DepartmentInfo>();

            if (!selRet) { return null; }

            var dt = db.SelectData;

            if(dt.Rows.Count == 0) { return ret; }

            foreach (DataRow row in dt.Rows)
            {
                var departmentCd = int.Parse(row[nameof(DEPARTMENT_CODE)].ToString());
                var departmentNm = row[nameof(DEPARTMENT_NAME)] == null ? string.Empty : row[nameof(DEPARTMENT_NAME)].ToString();

                ret.Add(new DepartmentInfo
                {
                    DEPARTMENT_CODE = departmentCd,
                    DEPARTMENT_NAME = departmentNm
                });
            }

            return ret;
        }

        private string CreateSelAllSql()
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT ");
            sql.AppendLine(nameof(DEPARTMENT_CODE) + ',');
            sql.AppendLine("NAME " + nameof(DEPARTMENT_NAME));
            sql.AppendLine(" FROM");
            sql.AppendLine(    "mst_department;");

            return sql.ToString();
        }
    }
}
