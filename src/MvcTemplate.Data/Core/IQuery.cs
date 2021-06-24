using System;
using System.Linq;
using System.Linq.Expressions;

namespace MvcTemplate.Data
{
    public interface IQuery<TModel> : IQueryable<TModel>
    {
        IQuery<TModel> Where(Expression<Func<TModel, Boolean>> predicate);

        IQueryable<TView> To<TView>();
    }
}
