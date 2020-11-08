using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Core.Dapper
{
    /// <summary>
    /// Dapper帮助类
    /// </summary>
    public class DapperHelper
    {
        private readonly string _connectionString;

        /// <summary>
        /// 使用连接字符串初始化
        /// </summary>
        /// <param name="connectionString"></param>
        public DapperHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                return cn.Query<T>(sql, param);
            }
        }

        /// <summary>
        /// QueryFirst
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public T QueryFirst<T>(string sql, object param = null)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                return cn.QueryFirst<T>(sql, param);
            }
        }

        /// <summary>
        /// QueryFirstOrDefault
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public T QueryFirstOrDefault<T>(string sql, object param = null)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                return cn.QueryFirstOrDefault<T>(sql, param);
            }
        }

        /// <summary>
        /// QueryAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                return cn.QueryAsync<T>(sql, param);
            }
        }

        /// <summary>
        /// QueryFirstAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<T> QueryFirstAsync<T>(string sql, object param = null)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                return cn.QueryFirstAsync<T>(sql, param);
            }
        }

        /// <summary>
        /// QueryFirstOrDefaultAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                return cn.QueryFirstOrDefaultAsync<T>(sql, param);
            }
        }
    }
}
