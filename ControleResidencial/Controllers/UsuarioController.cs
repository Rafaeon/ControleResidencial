using ControleResidencial.DTOs.Usuario.Request;
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
    [Route("api/usuarios")]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioServices _usuarioServices;

        public UsuarioController(IUsuarioServices usuarioservice)
        {
            _usuarioServices = usuarioservice;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateUsuarioRequestDTO dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            return CustomResponse(await _usuarioServices.CreateAsync(dto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateUsuarioRequestDTO dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            return CustomResponse(await _usuarioServices.UpdateAsync(dto));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(DeleteUsuarioRequestDTO dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            return CustomResponse(await _usuarioServices.DeleteAsync(dto));
        }
        [HttpPost("filter")]
        public async Task<IActionResult> GetUsuarioFilterAsync([FromQuery] UsuarioFilterRequestDTO dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            return CustomResponse(await _usuarioServices.GetUsuarioFilterAsync(dto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            return CustomResponse(await _usuarioServices.GetByUsuarioId(id));
        }

        [HttpGet("totais")]
        public async Task<IActionResult> GetTotaisPorUsuario()
        {
            return Ok(await _usuarioServices.GetTotaisPorUsuarioAsync());
        }

    }
}
