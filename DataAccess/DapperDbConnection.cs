using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class DapperDbConnection
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperDbConnection(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = "Server=DESKTOP-PJ13SRC;Initial Catalog=ECommerceOrderManagement;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Pooling=True;Max Pool Size=100;";
            //_connectionString = "Server=DESKTOP-PJ13SRC;Initial Catalog=ECommerceOrderManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            //_connectionString = _configuration.GetConnectionString("Server=DESKTOP-PJ13SRC;Initial Catalog=ECommerceOrderManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        public IDbConnection CreateConnection()
        {
            try
            {
                return new SqlConnection(_connectionString);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}