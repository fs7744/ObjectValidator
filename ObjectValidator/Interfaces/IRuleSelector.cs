namespace ObjectValidator.Interfaces
{
    public interface IRuleSelector
    {
        bool CanExecute(IValidateRule rule, ValidateContext context);
    }
}