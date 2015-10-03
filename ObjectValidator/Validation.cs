using ObjectValidator.Interfaces;

namespace ObjectValidator
{
    public static class Validation
    {
        public static IValidatorBuilder<T> NewValidatorBuilder<T>()
        {
            return Container.Resolve<IValidatorBuilder<T>>();
        }
    }
}