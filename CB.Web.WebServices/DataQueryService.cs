using System;
using System.Threading.Tasks;
using CB.Database.SqlServer;


namespace CB.Web.WebServices
{
    public class DataQueryService
    {
        #region Fields
        private readonly string _url;
        #endregion


        #region  Constructors & Destructor
        public DataQueryService(string url)
        {
            _url = url;
        }
        #endregion


        #region  Properties & Indexers
        public string Error { get; set; }
        public bool HasError => !string.IsNullOrEmpty(Error);
        #endregion


        #region Methods
        public async Task<DataResult> SendRequestsAsync(DataRequestCollection requestCollection)
        {
            try
            {
                var result = await Http.PostAsync<DataRequestCollection, DataResult>(_url, requestCollection);
                if (!result.HasError()) return result;

                Error = result.Error;
                return null;
            }
            catch (Exception exception)
            {
                Error = exception.Message;
                return null;
            }
        }
        #endregion
    }
}