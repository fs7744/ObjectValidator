﻿namespace ObjectValidator.Interfaces
{
    public interface IFluentRuleBuilder<T, out TProperty>
    {
        Validation Validation { get; }
    }
}