using EmployeeRegister.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRegister.Model
{
    public class EmployeeInfo
    {
        public int EMPLOYEE_NO { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string FULL_NAME { get; set; }
        public int DEPARTMENT_CODE { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string POSTAL_CODE { get; set; }
        public string ADDRESS { get; set; }
        public string TEL { get; set; }
        public DateTime BIRTHDAY { get; set; }
        public string SEX { get; set; }
        public string REMARKS { get; set; }
        public DateTime REGIST_DATE { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public DateTime DELETE_DATE { get; set; }

        public EmployeeInfo()
        {
            EMPLOYEE_NO = -1;
        }

        /// <summary>
        /// SELECT WHERE employno
        /// </summary>
        /// <param name="employeeNo"></param>
        /// <returns></returns>
        public bool GetInfoFromDb(int employeeNo)
        {
            var constr = Settings.Default.DB_CONNECTION_STR;
            var db = new Common.MySqlCom(constr);
            var sql = CreateSelSql(employeeNo);
            var selRet = db.Select(sql);

            if (!selRet) { return false; }

            var dt = db.SelectData;
            
            if(dt.Rows.Count == 0) { return true; }

            var selRow = dt.Rows[0];

            EMPLOYEE_NO = int.Parse(selRow[nameof(EMPLOYEE_NO)].ToString());
            FIRST_NAME = selRow[nameof(FIRST_NAME)] == null ? null : selRow[nameof(FIRST_NAME)].ToString();
            LAST_NAME = selRow[nameof(LAST_NAME)] == null ? null : selRow[nameof(LAST_NAME)].ToString();
            FULL_NAME = selRow[nameof(FULL_NAME)] == null ? null : selRow[nameof(FULL_NAME)].ToString();
            DEPARTMENT_CODE = int.Parse(selRow[nameof(DEPARTMENT_CODE)].ToString());
            DEPARTMENT_NAME = selRow[nameof(DEPARTMENT_NAME)] == null ? null : selRow[nameof(DEPARTMENT_NAME)].ToString();
            POSTAL_CODE = selRow[nameof(POSTAL_CODE)] == null ? null : selRow[nameof(POSTAL_CODE)].ToString();
            ADDRESS = selRow[nameof(ADDRESS)] == null ? null : selRow[nameof(ADDRESS)].ToString();
            TEL = selRow[nameof(TEL)] == null ? null : selRow[nameof(TEL)].ToString();
            BIRTHDAY = selRow[nameof(BIRTHDAY)] == null ? new DateTime() : DateTime.Parse(selRow[nameof(BIRTHDAY)].ToString());
            SEX = selRow[nameof(SEX)] == null ? null : selRow[nameof(SEX)].ToString();
            REMARKS = selRow[nameof(REMARKS)] == null ? null : selRow[nameof(REMARKS)].ToString();
            REGIST_DATE = selRow[nameof(REGIST_DATE)].ToString() == string.Empty ? new DateTime() : DateTime.Parse(selRow[nameof(REGIST_DATE)].ToString());
            UPDATE_DATE = selRow[nameof(UPDATE_DATE)].ToString() == string.Empty ? new DateTime() : DateTime.Parse(selRow[nameof(UPDATE_DATE)].ToString());
            DELETE_DATE = selRow[nameof(DELETE_DATE)].ToString() == string.Empty ? new DateTime() : DateTime.Parse(selRow[nameof(DELETE_DATE)].ToString());

            return true;
        }

        public bool Insert()
        {
            var constr = Settings.Default.DB_CONNECTION_STR;
            var db = new Common.MySqlCom(constr);
            var sql = CreateInsSql();
            var selRet = db.Execute(sql);
            return true;
        }

        public bool Update()
        {
            var constr = Settings.Default.DB_CONNECTION_STR;
            var db = new Common.MySqlCom(constr);
            var sql = CreateUpdSql(EMPLOYEE_NO);
            var selRet = db.Execute(sql);
            return true;
        }

        private string CreateSelSql(int whereEmpNo)
        {
            var sql = new StringBuilder();
            var employeeTbl = "emp.";
            var departmentTbl = "dep.";

            sql.AppendLine("SELECT ");
            sql.AppendLine(employeeTbl + nameof(EMPLOYEE_NO) + ",");
            sql.AppendLine(employeeTbl + nameof(FIRST_NAME) + ",");
            sql.AppendLine(employeeTbl + nameof(LAST_NAME) + ",");
            sql.AppendLine(employeeTbl + nameof(FULL_NAME) + ",");
            sql.AppendLine(employeeTbl + nameof(DEPARTMENT_CODE) + ",");
            sql.AppendLine(departmentTbl + "NAME AS " + nameof(DEPARTMENT_NAME) + ",");
            sql.AppendLine(employeeTbl + nameof(POSTAL_CODE) + ",");
            sql.AppendLine(employeeTbl + nameof(ADDRESS) + ",");
            sql.AppendLine(employeeTbl + nameof(TEL) + ",");
            sql.AppendLine(employeeTbl + nameof(BIRTHDAY) + ",");
            sql.AppendLine(employeeTbl + nameof(SEX) + ",");
            sql.AppendLine(employeeTbl + nameof(REMARKS) + ",");
            sql.AppendLine(employeeTbl + nameof(REGIST_DATE) + ",");
            sql.AppendLine(employeeTbl + nameof(UPDATE_DATE) + ",");
            sql.AppendLine(employeeTbl + nameof(DELETE_DATE));

            sql.AppendLine(" FROM");
            sql.AppendLine("    mst_employeeInfo emp");
            sql.AppendLine(" LEFT JOIN");
            sql.AppendLine("    mst_department dep");
            sql.AppendLine(" ON");
            sql.AppendLine("    emp.DEPARTMENT_CODE = dep.DEPARTMENT_CODE");

            sql.AppendLine(" WHERE");
            sql.AppendLine("    emp.EMPLOYEE_NO = " + whereEmpNo);

            return sql.ToString();
        }

        private string CreateUpdSql(int whereEmpNo)
        {
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE mst_employeeInfo SET ");
            sql.AppendLine(nameof(FIRST_NAME) + " = '" + FIRST_NAME + "',");
            sql.AppendLine(nameof(LAST_NAME) + " = '" + LAST_NAME + "',");
            sql.AppendLine(nameof(FULL_NAME) + " = '" + FULL_NAME + "',");
            sql.AppendLine(nameof(DEPARTMENT_CODE) + " = " + DEPARTMENT_CODE + ",");
            sql.AppendLine(nameof(POSTAL_CODE) + " = '" + POSTAL_CODE + "',");
            sql.AppendLine(nameof(ADDRESS) + " = '" + ADDRESS + "',");
            sql.AppendLine(nameof(TEL) + " = '" + TEL + "',");
            sql.AppendLine(nameof(BIRTHDAY) + " = '" + BIRTHDAY + "',");
            sql.AppendLine(nameof(SEX) + " = '" + SEX + "',");
            sql.AppendLine(nameof(REMARKS) + " = '" + REMARKS + "',");
            sql.AppendLine(nameof(UPDATE_DATE) + " = '" + UPDATE_DATE + "'");
            sql.AppendLine(" WHERE ");
            sql.AppendLine(nameof(EMPLOYEE_NO) + " = " + EMPLOYEE_NO);

            return sql.ToString();
        }

        private string CreateInsSql()
        {
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO mst_employeeInfo(");
            sql.AppendLine(nameof(FIRST_NAME) + ",");
            sql.AppendLine(nameof(LAST_NAME) + ",");
            sql.AppendLine(nameof(FULL_NAME) + ",");
            sql.AppendLine(nameof(DEPARTMENT_CODE) + ",");
            sql.AppendLine(nameof(POSTAL_CODE) + ",");
            sql.AppendLine(nameof(ADDRESS) + ",");
            sql.AppendLine(nameof(TEL) + ",");
            sql.AppendLine(nameof(BIRTHDAY) + ",");
            sql.AppendLine(nameof(SEX) + ",");
            sql.AppendLine(nameof(REMARKS) + ",");
            sql.AppendLine(nameof(REGIST_DATE));
            sql.AppendLine(") VALUES (");
            sql.AppendLine("'" + FIRST_NAME + "',");
            sql.AppendLine("'" + LAST_NAME + "',");
            sql.AppendLine("'" + FULL_NAME + "',");
            sql.AppendLine(" " + DEPARTMENT_CODE + ",");
            sql.AppendLine("'" + POSTAL_CODE + "',");
            sql.AppendLine("'" + ADDRESS + "',");
            sql.AppendLine("'" + TEL + "',");
            sql.AppendLine("'" + BIRTHDAY + "',");
            sql.AppendLine("'" + SEX + "',");
            sql.AppendLine("'" + REMARKS + "',");
            sql.AppendLine("'" + REGIST_DATE + "'");
            sql.AppendLine(")");

            return sql.ToString();
        }
    }
}
