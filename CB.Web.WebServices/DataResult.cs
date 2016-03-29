using System.Data;


namespace CB.Web.WebServices
{
    public class DataResult
    {
        #region Fields
        public DataSet[] _dataSets;
        #endregion


        #region  Constructors & Destructor
        public DataResult() { }

        public DataResult(DataSet[] dataSets)
        {
            _dataSets = dataSets;
        }

        public DataResult(string error)
        {
            Error = error;
        }
        #endregion


        #region  Properties & Indexers
        public string Error { get; set; }
        public DataRow FirstRow => FirstTable.Rows[0];
        public DataSet FirstSet => _dataSets[0];
        public DataTable FirstTable => FirstSet.Tables[0];
        public object FirstValue => FirstRow[0];
        public bool HasError => !string.IsNullOrEmpty(Error);
        public DataSet this[int index] => _dataSets[index];
        #endregion
    }
}