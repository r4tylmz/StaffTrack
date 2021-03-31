using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StaffTrack.Core.Repositories;
using StaffTrack.Core.Services;
using StaffTrack.Core.UnitOfWorks;

namespace StaffTrack.Service.Services
{
    public class Service<TEntity>:IService<TEntity> where TEntity:class
    {
            public readonly IUnitOfWork UnitOfWork;
            private readonly IRepository<TEntity> _repository;

            public Service(IRepository<TEntity> repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                UnitOfWork = unitOfWork;
            }

            public async Task<TEntity> GetByIdAsync(int id)
            {
                return await _repository.GetByIdAsync(id);
            }

            public async Task<IEnumerable<TEntity>> GetAllAsync()
            {
                return await _repository.GetAllAsync();
            }

            public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
            {
                return await _repository.Where(predicate);
            }

            public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
            {
                return await _repository.SingleOrDefaultAsync(predicate);
            }

            public async Task<TEntity> AddAsync(TEntity entity)
            {
                await _repository.AddAsync(entity);
                await UnitOfWork.CommitAsync();
                return entity;

            }

            public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
            {
                await _repository.AddRangeAsync(entities);
                await UnitOfWork.CommitAsync();
                return entities;
            }

            public void Remove(TEntity entity)
            {
                _repository.Remove(entity);
                UnitOfWork.Commit();
            }

            public void RemoveRange(IEnumerable<TEntity> entities)
            {
                _repository.RemoveRange(entities);
                UnitOfWork.Commit();
            }

            public TEntity Update(TEntity entity)
            {
                var updatedProduct = _repository.Update(entity);
                UnitOfWork.Commit();
                return updatedProduct;
            }
    }
}