using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Data.Core.Repositories.Base
{
    public class clsCommon
    {
        public static DataTable GetDataTable(string sSQL, string strConnection)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sSQL, strConnection);
                DataTable dt = new DataTable();
                int row = da.Fill(dt);
                return dt;
            }
            catch { return null; }
        }

        public static DataSet GetdataSet(string sSQL, string strConnection)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sSQL, strConnection);
                DataSet dt = new DataSet();
                int row = da.Fill(dt);
                return dt;
            }
            catch(Exception ex) { var s = ex.Message; return null; }
        }

        public static Boolean CheckMail(string strMail)
        {
            try
            {
                string match = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                Regex reg = new Regex(match);
                if (reg.IsMatch(strMail))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool ExcuteSQL(string pstrSQL, string strConnection)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection SQLconn;
            SQLconn = new SqlConnection(strConnection);
            try
            {
                SQLconn.Open();
                cmd.Connection = SQLconn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = pstrSQL;
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                SQLconn.Close();
                SQLconn.Dispose();
                return true;
            }
            catch
            {
                cmd.Dispose();
                SQLconn.Close();
                SQLconn.Dispose();
                return false;
            }
        }
    }
}
