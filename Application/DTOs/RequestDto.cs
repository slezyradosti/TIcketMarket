using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Application.DTOs
{
    public class RequestDto// : IFilter
    {
        [DefaultValue(0)]
        public int PageIndex { get; set; } = 0;

        [DefaultValue(10)]
        [Range(1, 100)]
        public int PageSize { get; set; } = 10;

        [DefaultValue("CreatedAt")]
        public string? SortColumn { get; set; } = "CreatedAt";

        [DefaultValue("ASC")]
        [RegularExpression("ASC|DESC")]
        public string? SortOrder { get; set; } = "ASC";

        [DefaultValue(null)]
        public string? FilterQuery { get; set; } = null;
    }
}
