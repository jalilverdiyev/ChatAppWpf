using System.Linq.Expressions;
using ChatApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Application.Repositories;

public interface IRepository<T> where T : BaseEntity, new()
{
	DbSet<T> Table { get; }
	IQueryable<T> FilterAll(Expression<Func<T, bool>> filter);
	Task<T?> FilterFirstAsync(Expression<Func<T, bool>> filter);
	Task<T?> GetByIdAsync(Guid id);
	Task<bool> AddAsync(T entity);
	bool Remove(T entity);
	Task<bool> RemoveByIdAsync(Guid id);
	bool Update(T entity);
	Task SaveAsync();
}
