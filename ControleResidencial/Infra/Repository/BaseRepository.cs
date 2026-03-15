using ControleResidencial.Infra.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControleResidencial.Infra.Repository
{
    // padrão de repositório base que utilizamos no meu trabalho atual,
    // centraliza as operações comuns do entity framework para não repetir código em cada repositório
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        
        protected readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // adiciona a entidade no contexto
        public void Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        // remove a entidade do contexto
        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        // retorna todos os registros sem rastreamento
        public IQueryable<T> FindAll()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        // retorna registros filtrados por uma expressão
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>()
                   .Where(expression).AsNoTracking();
        }

        // salva as alterações no banco
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        // atualiza a entidade no contexto
        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
