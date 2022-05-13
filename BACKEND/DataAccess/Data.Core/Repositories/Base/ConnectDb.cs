using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Data.Core.Repositories.Base
{
  
    public abstract class ConnectDb
    {

        internal IDbConnection ConnDungChung => new SqlConnection(ConfigurationManager.ConnectionStrings["DUNGCHUNG.ConnString"].ConnectionString);
        internal IDbConnection ConnSoLaoDong => new SqlConnection(ConfigurationManager.ConnectionStrings["SOXD_DVC.ConnString"].ConnectionString);
        internal IDbConnection ConnHanhChinh => new SqlConnection(ConfigurationManager.ConnectionStrings["HANHCHINH.ConnString"].ConnectionString);
        internal IDbConnection ConnPortal => new SqlConnection(ConfigurationManager.ConnectionStrings["Portal.ConnString"].ConnectionString);

    }
}
