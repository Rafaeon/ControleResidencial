using ControleResidencial.DTOs.Transacao.Request;
using ControleResidencial.DTOs.Usuario.Request;
using ControleResidencial.Services.Transacao.Interface;
using ControleResidencial.Services.Usuario;
using ControleResidencial.Services.Usuario.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ControleResidencial.Controllers
{
    // as controllers seguem o padrão que utilizamos no meu trabalho atual:
    // herdam o basecontroller que centraliza o customresponse,
    // validam o modelstate antes de chamar a service,
    // e delegam toda a regra de negócio para a camada de service

    //no meu ponto de vista, organizar o projeto em camadas facilita na hora de debugar
    [ApiController]
    [Route("api/transacoes")]
    public class TransacaoController : BaseController
    {
        private readonly ITransacaoServices _transacaoService;
        public TransacaoController(ITransacaoServices transacaoService)
        {
            _transacaoService = transacaoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateTransacaoRequestDTO dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            return CustomResponse(await _transacaoService.CreateAsync(dto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateTransacaoRequestDTO dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            return CustomResponse(await _transacaoService.UpdateAsync(dto));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(DeleteTransacaoRequestDTO dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            return CustomResponse(await _transacaoService.DeleteAsync(dto));
        }
        [HttpPost("filter")]
        public async Task<IActionResult> GetUsuarioFilterAsync([FromQuery] TransacaoFilterRequestDTO dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            return CustomResponse(await _transacaoService.GetTransacaoFilterAsync(dto));
        }
        [HttpGet("getbytransacaoporusuarioid")]
        public async Task<IActionResult> GetByTransacaoPorUsuarioId(string usuarioId)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            return CustomResponse(await _transacaoService.GetByTransacaoPorUsuarioId(usuarioId));
        }

    }
}
