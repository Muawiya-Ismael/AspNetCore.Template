using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AspNetCore.Objects;

namespace AspNetCore.Data.Migrations;

public sealed class Configuration : IDisposable
{
    private DbContext Context { get; }
    private IUnitOfWork UnitOfWork { get; }

    public Configuration(DbContext context, IMapper mapper)
    {
        UnitOfWork = new AuditedUnitOfWork(context, mapper, 0);
        Context = context;
    }

    public void Migrate()
    {
        Context.Database.Migrate();

        Seed();
    }
    public void Seed()
    {
        SeedPermissions();
        SeedRoles();

        SeedAccounts();
    }

    private void SeedPermissions()
    {
        List<Permission> permissions = new()
        {
            new() { Area = "Administration", Controller = "Accounts", Action = "Create" },
            new() { Area = "Administration", Controller = "Accounts", Action = "Edit" },
            new() { Area = "Administration", Controller = "Accounts", Action = "Index" },
            new() { Area = "Administration", Controller = "Roles", Action = "Create" },
            new() { Area = "Administration", Controller = "Roles", Action = "Delete" },
            new() { Area = "Administration", Controller = "Roles", Action = "Edit" },
            new() { Area = "Administration", Controller = "Roles", Action = "Index" },
            new() { Area = "Administration", Controller = "Customers", Action = "Create" },
            new() { Area = "Administration", Controller = "Customers", Action = "Delete" },
            new() { Area = "Administration", Controller = "Customers", Action = "Edit" },
            new() { Area = "Administration", Controller = "Customers", Action = "Index" }
        };

        foreach (Permission permission in UnitOfWork.Select<Permission>().ToArray())
            if (permissions.RemoveAll(p => p.Area == permission.Area && p.Controller == permission.Controller && p.Action == permission.Action) == 0)
            {
                UnitOfWork.DeleteRange(UnitOfWork.Select<RolePermission>().Where(role => role.PermissionId == permission.Id));
                UnitOfWork.Delete(permission);
            }

        UnitOfWork.InsertRange(permissions);
        UnitOfWork.Commit();
    }

    private void SeedRoles()
    {
        if (!UnitOfWork.Select<Role>().Any(role => role.Title == "Sys_Admin"))
        {
            UnitOfWork.Insert(new Role { Title = "Sys_Admin", Permissions = new List<RolePermission>() });
            UnitOfWork.Commit();
        }

        Role admin = UnitOfWork.Select<Role>().Single(role => role.Title == "Sys_Admin");
        Int64[] permissions = admin.Permissions.Select(role => role.PermissionId).ToArray();

        foreach (Permission permission in UnitOfWork.Select<Permission>())
            if (!permissions.Contains(permission.Id))
                UnitOfWork.Insert(new RolePermission { RoleId = admin.Id, PermissionId = permission.Id });

        UnitOfWork.Commit();
    }

    private void SeedAccounts()
    {
        Account[] accounts =
        {
            new()
            {
                Username = "admin",
                Passhash = "$2b$13$614ARGmxeOZvgRvmtWYV6OmHne6ChYhxI/14gH9swPMqe8RtvBwF.",
                Email = "admin@test.domains.com",
                IsLocked = false,

                RoleId = UnitOfWork.Select<Role>().Single(role => role.Title == "Sys_Admin").Id
            }
        };

        foreach (Account account in accounts)
        {
            if (UnitOfWork.Select<Account>().FirstOrDefault(model => model.Username == account.Username) is Account currentAccount)
            {
                currentAccount.IsLocked = account.IsLocked;
                currentAccount.RoleId = account.RoleId;

                UnitOfWork.Update(currentAccount);
            }
            else
            {
                UnitOfWork.Insert(account);
            }
        }

        UnitOfWork.Commit();
    }

    public void Dispose()
    {
        UnitOfWork.Dispose();
        Context.Dispose();
    }
}
