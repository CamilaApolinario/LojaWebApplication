﻿using WebApplicationOrcamento.Domain.Entities;
using WebApplicationOrcamento.Domain.Interfaces;
using WebApplicationOrcamento.Infra.Data.Repository;

namespace WebApplicationOrcamento.Service.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly BaseRepository<TEntity> _baseRepository;

        public BaseService(BaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public TEntity Add<TEntity1>(TEntity obj)
        {
            _baseRepository.Insert(obj);
            return obj;
        }

        public void Delete(int id) => _baseRepository.Delete(id);

        public IList<TEntity> Get() => _baseRepository.Select().ToList();

        public TEntity GetById(int id) => _baseRepository.Select(id);

        public TEntity Update<TEntity1>(TEntity obj)
        {
            _baseRepository.Update(obj);
            return obj;
        }
    }
}