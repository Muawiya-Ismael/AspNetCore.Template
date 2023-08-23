using AspNetCore.Components.Security;
using AspNetCore.Components.Tree;
using AspNetCore.Data;
using AspNetCore.Objects;
using AspNetCore.Resources;

namespace AspNetCore.Services.Administration
{
    public class CustomerService : AService
    {
        private IHasher Hasher { get; }

        public CustomerService(IUnitOfWork unitOfWork , IHasher hasher) : base(unitOfWork)
        {
            Hasher = hasher;
        }

        public TView? Get<TView>(Int64 id) where TView : AView
        {
            return UnitOfWork.GetAs<Customer, TView>(id);
        }

        public IQueryable<CustomerView> GetViews()
        {
            return UnitOfWork
                .Select<Customer>()
                .To<CustomerView>()
                .OrderByDescending(customer => customer.Id);
        }

        public Boolean IsActive(Int64 id)
        {
            return UnitOfWork.Select<Customer>().Any(customer => customer.Id == id && !customer.IsLocked);
        }

        public void Create(CustomerCreateView view)
        {
            Customer customer = UnitOfWork.To<Customer>(view);
            customer.Passhash = Hasher.HashPassword(view.Password);
            customer.Email = view.Email.ToLower();

            UnitOfWork.Insert(customer);
            UnitOfWork.Commit();
        }

        public void Edit(CustomerEditView view)
        {
            Customer customer = UnitOfWork.Get<Customer>(view.Id)!;
            customer.Username = view.Username;
            customer.FirstName = view.FirstName!;
            customer.LastName = view.LastName!;
            customer.Email = view.Email;
            customer.Address = view.Address!;
            customer.DateOfBirth = view.DateOfBirth;
            customer.Phone = view.Phone!;
            customer.Country = view.Country!;
            customer.Gender = view.Gender!;
            customer.IsLocked = view.IsLocked;
            customer.RoleId = view.RoleId;

            UnitOfWork.Update(customer);
            UnitOfWork.Commit();
        }

        public void Delete(Int64 id)
        {
            UnitOfWork.Delete<Customer>(id);
            UnitOfWork.Commit();
        }
    }
}
