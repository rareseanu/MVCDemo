using Domain.Base;
using System.Collections.Generic;

namespace Domain.Employees.ValueObjects
{
    public sealed class Name : ValueObject
    {
        private Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }

        public static Result<Name> Create(string firstName,
            string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return Result.Failure<Name>("First name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Result.Failure<Name>("Last name cannot be empty.");
            }

            return Result.Success(new Name(firstName, lastName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
