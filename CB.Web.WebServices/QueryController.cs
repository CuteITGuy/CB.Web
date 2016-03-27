using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using CB.Database.SqlServer;


namespace CB.Web.WebServices
{
    public class QueryController: ApiController
    {
        #region Fields
        private readonly string _connectionString;
        #endregion


        #region  Constructors & Destructor
        public QueryController(string connectionStringSetting)
        {
            _connectionString = GetConnectionString(connectionStringSetting);
        }

        public QueryController(): this("queryConnectionString") { }
        #endregion


        #region Methods
        [HttpPost]
        public DataResult FetchData(DataRequestCollection requestCollection)
        {
            switch (requestCollection.QueryStrategy)
            {
                case QueryStrategy.Sequential:
                    return QueryDataSequentially(requestCollection);
                case QueryStrategy.Parallel:
                    return QueryDataParallely(requestCollection);
                case QueryStrategy.Transactional:
                    return QueryDataTransactionally(requestCollection);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion


        #region Implementation
        private DataSet FetchData(DataRequestCollection requestCollection, SqlConnection con, SqlTransaction trans) { }

        private static string GetConnectionString(string connectionStringSetting)
        {
            return ConfigurationManager.ConnectionStrings[connectionStringSetting].ConnectionString;
        }

        private SqlConnection OpenConnection(string databaseName = null)
        {
            var csBuilder = new SqlConnectionStringBuilder(_connectionString);
            if (!string.IsNullOrEmpty(databaseName)) csBuilder.InitialCatalog = databaseName;
            var con = new SqlConnection(csBuilder.ToString());
            con.Open();
            return con;
        }

        private DataResult QueryDataParallely(DataRequestCollection requestCollection) { }

        private DataResult QueryDataSequentially(DataRequestCollection requestCollection) { }

        private DataResult QueryDataTransactionally(DataRequestCollection requestCollection)
        {
            using (var con = OpenConnection())
            {
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        var ds = FetchData(requestCollection, con, trans);
                        trans.Commit();
                        return new DataResult(ds);
                    }
                    catch (Exception exception)
                    {
                        trans.Rollback();
                        return new DataResult(exception.Message);
                    }
                }
            }
        }
        #endregion
    }
}