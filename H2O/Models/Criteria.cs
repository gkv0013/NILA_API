using Newtonsoft.Json;
using System.Data;
using static H2O.Enumerators.CrudTypes;

namespace H2O.Models
{
    public class Criteria
    {
        public int Mode { get; set; }
        public CrudType CrudType { get; set; }=CrudType.None;
        public DataTable? FetchData { get; set; }
        public DataSet? SaveData { get; set; }
    }
}
