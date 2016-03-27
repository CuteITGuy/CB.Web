using System.Data;


namespace CB.Web.WebServices
{
    public class DataResult
    {
        #region  Constructors & Destructor
        public DataResult() { }

        public DataResult(DataSet dataSet)
        {
            DataSet = dataSet;
        }

        public DataResult(string error)
        {
            Error = error;
        }
        #endregion


        #region  Properties & Indexers
        public DataSet DataSet { get; set; }
        public string Error { get; set; }
        #endregion
    }
}