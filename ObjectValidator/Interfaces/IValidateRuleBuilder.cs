namespace ObjectValidator.Interfaces
{
    public interface IValidateRuleBuilder
    {
        string RuleSet { get; set; }

        IValidateRule Build();
    }
}