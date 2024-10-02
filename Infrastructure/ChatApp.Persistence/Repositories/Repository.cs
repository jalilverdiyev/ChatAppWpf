using System.Linq.Expressions;
using ChatApp.Application.Repositories;
using ChatApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Persistence.Repositories;

public class Repository<T>(ChatAppDbContext chatAppDbContext) : IRepository<T>
		where T : BaseEntity, new()
{
	public DbSet<T> Table => chatAppDbContext.Set<T>();

	public IQueryable<T> FilterAll(Expression<Func<T, bool>> filter) => Table.Where(filter);

	public async Task<T?> FilterFirstAsync(Expression<Func<T, bool>> filter) => await Table.FirstOrDefaultAsync(filter);

	public async Task<T?> GetByIdAsync(Guid id) => await Table.FindAsync(id);

	public async Task<bool> AddAsync(T entity)
	{
		try
		{
			await Table.AddAsync(entity);
			await chatAppDbContext.SaveChangesAsync();

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool Remove(T entity)
	{
		try
		{
			Table.Remove(entity);
			chatAppDbContext.SaveChanges();

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public async Task<bool> RemoveByIdAsync(Guid id)
	{
		var entity = await GetByIdAsync(id);

		if (entity is null)
			return false;

		try
		{
			Table.Remove(entity);
			await chatAppDbContext.SaveChangesAsync();

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool Update(T entity)
	{
		try
		{
			Table.Update(entity);
			chatAppDbContext.SaveChanges();

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public async Task SaveAsync() => await chatAppDbContext.SaveChangesAsync();
}
