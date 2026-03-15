using ControleResidencial.DTOs.Categoria.Request;
using ControleResidencial.DTOs.Categoria.Response;
using ControleResidencial.DTOs.Transacao.Response;
using ControleResidencial.DTOs.Usuario.Response;
using ControleResidencial.Helper.Enums;
using ControleResidencial.Infra.Repository.Entity;
using ControleResidencial.Infra.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleResidencial.Infra.Repository
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        // chama o método create do repositório base
        public void CreateAsync(Categoria categoria)
        {
            Create(categoria);
        }
        // busca uma categoria pelo id
        public async Task<Categoria?> GetById(string id)
        {
            return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }
        // listagem com filtro, ordenação e paginação
        public async Task<CategoriaFilterResponseDTO> ListCategoriaAsync(CategoriaFilterRequestDTO dto)
        {
            var query = _context.Categorias.AsQueryable();
            // filtra por descrição se informado
            if (!string.IsNullOrWhiteSpace(dto.Pesquisa))
            {
                query = query.Where(x =>
                    x.Descricao.Contains(dto.Pesquisa));
            }

            var total = await query.CountAsync();
            // ordena por descrição conforme direção informada
            if (dto.OrdenarColunaDirecao?.ToLower() == "desc")
                query = query.OrderByDescending(x => x.Descricao);
            else
                query = query.OrderBy(x => x.Descricao);
            // aplica paginação e monta o retorno
            var lista = await query
                .Skip((dto.Page - 1) * dto.Regs)
                .Take(dto.Regs)
                .Select(x => new CategoriaFilterDataDTO
                {
                    Id = x.Id,
                    Descricao = x.Descricao,
                    Finalidade = x.Finalidade.ToString(),

                })
                .ToListAsync();

            return new CategoriaFilterResponseDTO
            {
                Total = total,
                Lista = lista
            };
        }
    }
}
