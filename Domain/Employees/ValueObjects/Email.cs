using Domain.Base;

namespace Domain.Employees.ValueObjects
{
    public sealed class Email : ValueObject<Email>
    {
        public readonly string Value;

        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result.Failure<Email>("Email cannot be empty.");
            }

            return Result.Success(new Email(email));
        }

        protected override bool EqualsCore(Email other)
        {
            return Value.Equals(other.Value);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static explicit operator Email(string email)
        {
            return Create(email).Value;
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }
    }
}
