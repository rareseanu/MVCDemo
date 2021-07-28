using System;
using Domain.Base;

namespace Domain.Companies.ValueObjects
{
    public sealed class CompanyName : ValueObject<CompanyName>
    {
        public readonly string Value;

        private CompanyName(string value)
        {
            Value = value;
        }

        public static Result<CompanyName> Create(string companyName)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                return Result.Failure<CompanyName>("Company name was not provided");
            }

            return Result.Success(new CompanyName(companyName));
        }

        protected override bool EqualsCore(CompanyName other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static explicit operator CompanyName(string companyName)
        {
            return Create(companyName).Value;
        }

        public static implicit operator string(CompanyName companyName)
        {
            return companyName.Value;
        }
    }
}
