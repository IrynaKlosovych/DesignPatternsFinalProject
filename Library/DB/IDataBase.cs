using System.Data.SqlClient;
using System.Data;

namespace Library.DB
{
    public interface IDataBase
    {
        DataTable SelectData(string sqlQuery, SqlParameter[]? parameters = null);
        void InsertUpdateDeleteData(string query, SqlParameter[]? parameters = null);
    }
}
