using ObjectValidator.Entities;
using System.Threading.Tasks;

namespace ObjectValidator.Interfaces
{
    public interface IValidator
    {
        Task<IValidateResult> ValidateAsync(ValidateContext context);
    }
}