using AutoMapper;
using ControleResidencial.DTOs.Transacao.Request;
using ControleResidencial.DTOs.Transacao.Response;
using ControleResidencial.DTOs.Usuario.Response;
using ControleResidencial.Helper.Enums;
using ControleResidencial.Infra.Repository;
using ControleResidencial.Infra.Repository.Entity;
using ControleResidencial.Infra.Repository.Interfaces;
using ControleResidencial.Services.Categoria.Interface;
using ControleResidencial.Services.Transacao.Interface;
using Microsoft.AspNetCore.Identity;

namespace ControleResidencial.Services.Transacao
{
    // arquitetura de services que seguimos no meu trabalho atual,
    // utilizamos o identityresult para padronizar os retornos de sucesso e erro
    public class TransacaoServices : ITransacaoServices
    {
        private readonly IMapper _mapper;
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IBaseRepository<Infra.Repository.Entity.Transacao> _baseRepository;
        private readonly ICategoriaRepository _categoriaRepository; 
        private readonly IUsuarioRepository _usuarioRepository;
        public TransacaoServices(IBaseRepository<Infra.Repository.Entity.Transacao> baseRepository, ITransacaoRepository transacaoRepository, ICategoriaRepository categoriaRepository, IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _transacaoRepository = transacaoRepository;
            _categoriaRepository = categoriaRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        // cria uma transacao aplicando validações de negócio antes de salvar
        public async Task<TransacaoResponseDTO> CreateAsync(CreateTransacaoRequestDTO dto)
        {
            var response = new TransacaoResponseDTO();
            try
            {
                // valida se a categoria existe
                var categoria = await _categoriaRepository.GetById(dto.CategoriaId);

                if (categoria == null)
                {
                    response.IdentityResult = IdentityResult.Failed(
                        new IdentityError
                        {
                            Code = "002",
                            Description = "Categoria não localizada"
                        });

                    return response;
                }
                // tipo 1 = despesa, não pode usar categoria de receita
                if (dto.Tipo == "1" && categoria.Finalidade == FinalidadeEnum.Receita)
                {
                    response.IdentityResult = IdentityResult.Failed(
                        new IdentityError
                        {
                            Code = "003",
                            Description = "Categoria inválida para transação do tipo despesa"
                        });

                    return response;
                }
                // tipo 2 = receita, não pode usar categoria de despesa
                if (dto.Tipo == "2" && categoria.Finalidade == FinalidadeEnum.Despesa)
                {
                    response.IdentityResult = IdentityResult.Failed(
                        new IdentityError
                        {
                            Code = "004",
                            Description = "Categoria inválida para transação do tipo receita"
                        });

                    return response;
                }
                // monta a entidade e salva no banco
                var data = new Infra.Repository.Entity.Transacao();
                data.Id = Guid.NewGuid().ToString();
                data.Descricao = dto.Descricao;
                data.Valor = dto.Valor;
                data.Tipo = dto.Tipo;
                data.CategoriaId = dto.CategoriaId;
                data.UsuarioId = dto.UsuarioId;
                data.DataTransacao = DateTime.UtcNow;

                _transacaoRepository.CreateAsync(data);

                await _baseRepository.SaveChangesAsync();
                // busca o usuario para validar idade e montar o retorno
                var usuario = await _usuarioRepository.GetById(dto.UsuarioId);

                if (usuario == null)
                {
                    response.IdentityResult = IdentityResult.Failed(
                        new IdentityError
                        {
                            Code = "005",
                            Description = "Usuário não localizado"
                        });

                    return response;
                }
                // menores de idade não podem cadastrar receitas
                if (usuario.Idade < 18 && dto.Tipo == "2")
                {
                    response.IdentityResult = IdentityResult.Failed(
                        new IdentityError
                        {
                            Code = "006",
                            Description = "Usuários menores de idade não podem cadastrar receitas"
                        });

                    return response;
                }

                response.IdentityResult = IdentityResult.Success;
                // monta o dto de retorno com os dados da transacao e do usuario
                response.Data = new TransacaoResponseDataDTO
                {
                    Id = data.Id,
                    Descricao = data.Descricao,
                    Valor = data.Valor,
                    DataTransacao = data.DataTransacao,
                    UsuarioId = data.UsuarioId,
                    Usuario = usuario == null ? null : new UsuarioFilterDataDTO
                    {
                        Id = usuario.Id,
                        Nome = usuario.Nome,
                        Idade = usuario.Idade,
                        IsAtivo = usuario.IsAtivo,
                        DataInclusao = usuario.DataInclusao,
                        DataAtualizacao = usuario.DataAtualizacao
                    }
                };
                
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the transacao.", ex);
            }
            return response;
        }
        // remove uma transacao pelo id, retorna erro se não encontrar
        public async Task<TransacaoResponseDTO> DeleteAsync(DeleteTransacaoRequestDTO dto)
        {
            var response = new TransacaoResponseDTO();

            try
            {
                var data = await _transacaoRepository.GetById(dto.Id);

                if (data == null)
                {
                    response.IdentityResult = IdentityResult.Failed(
                        new IdentityError()
                        {
                            Code = "001",
                            Description = "Registro não localizado"
                        });

                    return response;
                }

                _transacaoRepository.DeleteAsync(data);

                await _baseRepository.SaveChangesAsync();

                response.IdentityResult = IdentityResult.Success;
                response.Data = _mapper.Map<TransacaoResponseDataDTO>(data);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the transacao.", ex);
            }
        }
        // busca uma transacao pelo id
        public async Task<TransacaoResponseDTO> GetByTransacaoId(string transacaoId)
        {
            var response = new TransacaoResponseDTO();

            try
            {
                var data = await _transacaoRepository.GetById(transacaoId);

                if (data == null)
                {
                    response.IdentityResult = IdentityResult.Failed(
                        new IdentityError()
                        {
                            Code = "001",
                            Description = "Registro não localizado"
                        });

                    return response;
                }

                response.IdentityResult = IdentityResult.Success;
                response.Data = _mapper.Map<TransacaoResponseDataDTO>(data);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the usuario.", ex);
            }
        }
        // busca todas as transacoes de um usuario pelo id
        public async Task<TransacaoResponseDTO> GetByTransacaoPorUsuarioId(string usuarioId)
        {
            var response = new TransacaoResponseDTO();

            try
            {
                var data = await _transacaoRepository.GetByUsuarioId(usuarioId);

                if (data == null || !data.Any())
                {
                    response.IdentityResult = IdentityResult.Failed(
                        new IdentityError()
                        {
                            Code = "001",
                            Description = "Nenhuma transação encontrada para este usuário"
                        });

                    return response;
                }

                response.IdentityResult = IdentityResult.Success;
                response.DataList = _mapper.Map<List<TransacaoResponseDataDTO>>(data);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving transacoes by usuarioId.", ex);
            }

        }
        // retorna a listagem filtrada de transacoes
        public async Task<TransacaoFilterResponseDTO> GetTransacaoFilterAsync(TransacaoFilterRequestDTO dto)
        {
            return await _transacaoRepository.ListTransacaoAsync(dto);
        }
        // atualiza descrição e valor da transacao
        public async Task<TransacaoResponseDTO> UpdateAsync(UpdateTransacaoRequestDTO dto)
        {
            var response = new TransacaoResponseDTO();
            try
            {
                var data = await _transacaoRepository.GetById(dto.Id);
                if (data == null)
                {
                    response.IdentityResult = IdentityResult.Failed(new IdentityError() { Code = "001", Description = "Registro não localizado" });
                    return response;
                }
                data.Descricao = dto.Descricao;
                data.Valor = dto.Valor;

                _transacaoRepository.UpdateAsync(data);

                await _baseRepository.SaveChangesAsync();
                response.IdentityResult = IdentityResult.Success;
                response.Data = _mapper.Map<TransacaoResponseDataDTO>(data);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the usuario.", ex);
            }
            return response;
        }
    }
}
