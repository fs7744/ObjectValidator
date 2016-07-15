using ObjectValidator.Entities;
using System.Collections.Generic;

namespace ObjectValidator.Interfaces
{
    public interface IValidateResult
    {
        bool IsValid { get; }

        List<ValidateFailure> Failures { get; }

        void Merge(IEnumerable<ValidateFailure> failures);
    }
}