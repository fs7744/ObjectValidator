namespace ObjectValidator.Entities
{
    public class ValidateFailure
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public string Error { get; set; }
    }
}