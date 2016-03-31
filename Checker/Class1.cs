using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newegg.Po.Common;
using ObjectValidator;
using ObjectValidator.Interfaces;

namespace Checker
{
    public class Class1 : ServiceBase<Class1>
    {
        public DateTime? Time { get; set; }

        public override IValidatorBuilder<Class1> InitValidation()
        {
            var b = this.NewValidatorBuilder();
            b.RuleFor(i => i.Time).NotNull();
            return base.InitValidation();
        }
    }
}