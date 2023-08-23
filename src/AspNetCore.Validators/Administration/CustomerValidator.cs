using AspNetCore.Components.Security;
using AspNetCore.Data;
using AspNetCore.Objects;
using AspNetCore.Resources;

namespace AspNetCore.Validators.Administration
{
    public class CustomerValidator : AValidator
    {
        private IHasher Hasher { get; }

        public CustomerValidator(IUnitOfWork unitOfWork, IHasher hasher) : base(unitOfWork)
        {
            Hasher = hasher;
        }

        public Boolean CanCreate(CustomerCreateView view)
        {
            Boolean isValid = IsUniqueUsername(0, view.Username);
            isValid &= IsUniqueEmail(0, view.Email);
            isValid &= ModelState.IsValid;

            return isValid;
        }

        public Boolean CanEdit(CustomerEditView view)
        {
            Boolean isValid = IsUniqueUsername(view.Id, view.Username);
            isValid &= IsUniqueEmail(view.Id, view.Email);
            isValid &= ModelState.IsValid;

            return isValid;
        }

        private Boolean IsAuthenticated(String username, String password)
        {
            String? passhash = UnitOfWork
                .Select<Customer>()
                .Where(customer => customer.Username == username)
                .Select(customer => customer.Passhash)
                .SingleOrDefault();

            Boolean isCorrect = Hasher.VerifyPassword(password, passhash);

            if (!isCorrect)
                Alerts.AddError(Validation.For<CustomerView>("IncorrectAuthentication"));

            return isCorrect;
        }

        private Boolean IsCorrectPassword(Int64 id, String password)
        {
            String? passhash = UnitOfWork
                .Select<Customer>()
                .Where(customer => customer.Id == id)
                .Select(customer => customer.Passhash)
                .SingleOrDefault();

            Boolean isCorrect = Hasher.VerifyPassword(password, passhash);

            return isCorrect;
        }

        private Boolean IsUniqueUsername(Int64 id, String username)
        {
            Boolean isUnique = !UnitOfWork
                .Select<Customer>()
                .Any(customer =>
                    customer.Id != id &&
                    customer.Username == username);

            if (!isUnique)
                ModelState.AddModelError(nameof(CustomerView.Username), Validation.For<CustomerView>("UniqueUsername"));

            return isUnique;
        }

        private Boolean IsUniqueEmail(Int64 id, String email)
        {
            Boolean isUnique = !UnitOfWork
                .Select<Customer>()
                .Any(customer =>
                    customer.Id != id &&
                    customer.Email == email);

            if (!isUnique)
                ModelState.AddModelError(nameof(CustomerView.Email), Validation.For<CustomerView>("UniqueEmail"));

            return isUnique;
        }

        private Boolean IsActive(String username)
        {
            Boolean isActive = UnitOfWork
                .Select<Customer>()
                .Any(customer =>
                    !customer.IsLocked &&
                    customer.Username == username);

            if (!isActive)
                Alerts.AddError(Validation.For<CustomerView>("LockedAccount"));

            return isActive;
        }
    }
}
