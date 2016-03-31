using ObjectValidator.Common;
using ObjectValidator.Interfaces;
using System.Collections.Generic;

namespace ObjectValidator.Entities
{
    public class ValidateResult : IValidateResult
    {
        public List<ValidateFailure> Failures { get; protected set; }

        public bool IsValid
        {
            get
            {
                return Failures.IsEmptyOrNull();
            }
        }

        public ValidateResult()
        {
            Failures = new List<ValidateFailure>();
        }

        public ValidateResult(IEnumerable<ValidateFailure> failures) : this()
        {
            Merge(failures);
        }

        public void Merge(IEnumerable<ValidateFailure> failures)
        {
            if (!failures.IsEmptyOrNull())
                Failures.AddRange(failures);
        }
    }
}