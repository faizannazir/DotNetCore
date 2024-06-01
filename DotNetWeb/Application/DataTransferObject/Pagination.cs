

namespace DataTransferObject
{
    public class Pagination
    {
        public int draw { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        public string sortColumnDirection { get; set; }
        public string searchValue { get; set; }
        public string columnNum { get; set; }
        public string sortColumn { get; set; }
    }
}
