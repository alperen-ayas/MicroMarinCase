namespace MicroMarinCase.Application.Filters
{
    public class FilterParameter
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string FilterType { get; set; } = string.Empty; // Örneğin: "Equals", "GreaterThan", "LessThan"
    }

    public class FilterParameters
    {
        public List<FilterParameter> Filters { get; set; } = new List<FilterParameter>();
    }
}
