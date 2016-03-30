using System.Data;


namespace CB.Web.WebServices
{
    public class DataResult
    {
        #region  Constructors & Destructor
        public DataResult() { }

        public DataResult(DataSet[] dataSets)
        {
            DataSets = dataSets;
        }

        public DataResult(string error)
        {
            Error = error;
        }
        #endregion


        #region  Properties & Indexers
        public DataSet[] DataSets { get; set; }
        public string Error { get; set; }
        #endregion


        #region Methods
        public DataRow GetFirstRow()
        {
            var table = GetFirstTable();
            return table == null || table.Rows.Count == 0 ? null : table.Rows[0];
        }

        public DataSet GetFirstSet()
        {
            return DataSets == null || DataSets.Length == 0 ? null : DataSets[0];
        }

        public DataTable GetFirstTable()
        {
            var set = GetFirstSet();
            return set == null || set.Tables.Count == 0 ? null : set.Tables[0];
        }

        public object GetFirstValue()
        {
            var row = GetFirstRow();
            return row == null || row.ItemArray.Length == 0 ? null : row[0];
        }

        public bool HasError() => !string.IsNullOrEmpty(Error);
        #endregion
    }
}