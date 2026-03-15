using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ControleResidencial.Controllers
{
    public class BaseController : ControllerBase
    {
        protected ICollection<string> Errors = new List<string>();
        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
    {
        { "Messages", Errors.ToArray() }
    }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddErrorToStack(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool ValidOperation()
        {
            return !Errors.Any();
        }

        protected void AddErrorToStack(string error)
        {
            Errors.Add(error);
        }

        protected void CleanErrors()
        {
            Errors.Clear();
        }
        protected string UsuarioNome()
        {
            var usuarioNome = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "usuarionome")?.Value;
            return usuarioNome ?? "";
        }
        protected string UsuarioId()
        {
            var usuarioNome = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "usuarioid")?.Value;
            return usuarioNome ?? "";
        }
    }
}
