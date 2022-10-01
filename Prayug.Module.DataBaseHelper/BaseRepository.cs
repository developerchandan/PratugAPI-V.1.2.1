using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Prayug.Module.DataBaseHelper
{
    public class BaseRepository
    {
        private readonly IConfiguration _config;
        public readonly DateTime created_date = DateTime.Now;
        public readonly DateTime created_date_utc = DateTime.UtcNow;
        public readonly string last_modified_date_time = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        public BaseRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(_config.GetConnectionString("DatabaseConnectionString"));
            }
        }

        public IDbConnection NotificationConnection
        {
            get
            {
                return new MySqlConnection(_config.GetConnectionString("NotificationDatabaseConnectionString"));
            }
        }
        public IDbConnection LoggerConnection
        {
            get
            {
                return new MySqlConnection(_config.GetConnectionString("DatabaseLoggerConnectionString"));
            }
        }
        public IDbConnection ECommConnection
        {
            get
            {
                return new MySqlConnection(_config.GetConnectionString("DatabaseECommConnectionString"));
            }
        }
    }
}