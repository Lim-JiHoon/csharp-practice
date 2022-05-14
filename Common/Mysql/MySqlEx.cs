using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WinFormsApp3.Common.Mysql
{
    internal class MySqlEx : IDisposable
    {
        private MySqlConnection? _conn = null;
        public MySqlConnection? Conn { get => _conn; }
        internal MySqlEx()
        {
            if (_conn == null)
            {
                var set = Tasking.Properties.Settings.Default;                
                _conn = new MySqlConnection(connectionString: $"Server={set.DB_SERVER};Database={set.DB_DATABASE};Uid={set.DB_UID};Pwd={set.DB_PWD};Port={set.DB_PORT};");
            }
            try
            {
                _conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터베이스 연결 실패 \n {ex}");
            }
        }

        public void Dispose()
        {
            if (Conn != null)
                Conn!.Close();
            Conn!.Dispose();
        }

        public async Task<DataTable> GetDT(string query)
        {
            DataTable dt = new();
            using (MySqlCommand cmd = new(query, Conn))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    await adapter.FillAsync(dt);
                }
            }

            return dt;
        }

        public async Task<int> Execute(string query)
        {
            using (MySqlCommand cmd = new(query, Conn))
            {
                return await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}

