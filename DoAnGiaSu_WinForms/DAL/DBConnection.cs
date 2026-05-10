using Microsoft.Data.SqlClient;

namespace DoAnGiaSu_WinForms.DAL
{
    public class DBConnection
    {
        private string connectionString = @"Server=lpc:hehe;Database=DoAn_GiaSu_Final;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}