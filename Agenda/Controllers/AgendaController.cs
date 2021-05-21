using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.EntityFramework;
using Agenda.EtityFramework;
using Agenda.Models;
using Agenda.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private WorkSpace _workSpace;
        public AgendaController(Context context)
        {
            _workSpace = new WorkSpace(context);
        }

        #region GetAll
        [HttpGet("personas/lista")]
        public async Task<ActionResult<Result<IEnumerable<Persona>>>> GetAllAsyncPersona()
        {
            Result<IEnumerable<Persona>> result = new Result<IEnumerable<Persona>>();
            try
            {
                var personas = await _workSpace.PersonaRepo.GetAllAsync();
                result.Response = personas;
                return Ok(result);

            } catch (Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(result);
            }
        }

        [HttpGet("direcciones/lista")]
        public async Task<ActionResult<IEnumerable<Direcciones>>> GetAllAsyncDirecciones()
        {
            Result<IEnumerable<Direcciones>> result = new Result<IEnumerable<Direcciones>>();
            try
            {
                var direcciones = await _workSpace.DireccionesRepo.GetAllAsync();
                result.Response = direcciones;
                return Ok(result);

            } catch (Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("contactos/lista")]
        public async Task<ActionResult<IEnumerable<Contactos>>> GetAllAsyncContactos()
        {
            Result<IEnumerable<Contactos>> result = new Result<IEnumerable<Contactos>>();
            try
            {
                var contactos = await _workSpace.ContactosRepo.GetAllAsync();
                result.Response = contactos;
                return Ok(result);
            } catch (Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(result);
            }
        }

        #endregion

        #region GetById
        [HttpGet("persona/{id}")]
        public async Task<ActionResult<Result<Persona>>> GetByIdAsyncPersona(int id)
        {
            Result<Persona> result = new Result<Persona>();
            try
            {
                var persona = await _workSpace.PersonaRepo.GetByIdAsync(id);

                if (persona == null)
                {
                    result.MessageError = "La persona no fue encontrada";
                    return NotFound(result);
                }

                result.Response = persona;
                return Ok(result);

            } catch (Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(result);
            }
        }

        [HttpGet("direccion/{id}")]
        public async Task<ActionResult<Result<Direcciones>>> GetByIdAsyncDireccion(int id)
        {
            Result<Direcciones> result = new Result<Direcciones>();
            try
            {
                var direccion = await _workSpace.DireccionesRepo.GetByIdAsync(id);
                if (direccion == null)
                {
                    result.MessageError = "La direccion no fue encontrada";
                    return NotFound(result);
                }

                return Ok(result);
            } catch (Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("contacto/{id}")]
        public async Task<ActionResult<Result<Contactos>>> GetByIdAsyncContacto(int id)
        {
            Result<Contactos> result = new Result<Contactos>();
            try
            {
                var contacto = await _workSpace.ContactosRepo.GetByIdAsync(id);
                if (contacto == null)
                {
                    result.MessageError = "El contacto no fue encontrado";
                    return NotFound(result);
                }

                return Ok(result);

            } catch (Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(ex);
            }
        }

        #endregion

        #region PostAsync
        [HttpPost("persona/crear")]
        public async Task<ActionResult<Result<Persona>>> PostAsyncPersona([FromBody] PersonaRequest request)
        {
            Result<Persona> result = new Result<Persona>();
            try
            {
                var model = new Persona
                {
                    Apellido = request.Apellido,
                    Created = DateTime.Now,
                    Nombre = request.Nombre,
                    Status = true
                };
                result.Response = await _workSpace.PersonaRepo.CreateAsync(model);
                await _workSpace.PersonaRepo.SaveAsync();
                return Created("", result);

            } catch (Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(result);
            }
        }

        [HttpPost("direccion/crear")]
        public async Task<ActionResult<Result<Direcciones>>> PostAsyncDireccion([FromBody] DireccionRequest request)
        {
            Result<Direcciones> result = new Result<Direcciones>();
            try
            {
                var model = new Direcciones
                {
                    Ciudad = request.Ciudad,
                    Created = DateTime.Now,
                    Status = true,
                    Direccion = request.Direccion,
                    Pais = request.Pais,
                    PersonaId = request.PersonaId
                };
                result.Response = await _workSpace.DireccionesRepo.CreateAsync(model);
                await _workSpace.DireccionesRepo.SaveAsync();
                return Created("", result);

            }
            catch (Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(result);
            }
        }

        [HttpPost("contacto/crear")]
        public async Task<ActionResult<Result<Direcciones>>> PostAsyncContacto([FromBody] ContactoRequest request)
        {
            Result<Contactos> result = new Result<Contactos>();
            try
            {
                var model = new Contactos
                {
                    Created = DateTime.Now,
                    Status = true,
                    PersonaId = request.PersonaId,
                    Contacto = request.Contacto,
                    Tipo = request.Tipo
                };
                result.Response = await _workSpace.ContactosRepo.CreateAsync(model);
                await _workSpace.ContactosRepo.SaveAsync();
                return Created("", result);

            }
            catch (Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(result);
            }
        }
        #endregion

        #region Put
        [HttpPut("persona/actualizar/{id}")]
        public ActionResult<Result<string>> PutPersona(int id,[FromBody] PersonaRequest request)
        {
            Result<string> result = new Result<string>();
            try
            {
                var model = _workSpace.PersonaRepo.GetById(id);
                if(model == null)
                {
                    result.MessageError = "La persona no fue encontrada";
                    return NotFound(result);
                }

                model.Apellido = request.Apellido;
                model.Nombre = request.Nombre;

                _workSpace.PersonaRepo.Update(model);
                _workSpace.PersonaRepo.Save();

                return NoContent();
            }catch(Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(result);
            }
        }

        [HttpPut("direccion/actualizar/{id}")]
        public ActionResult<Result<string>> PutDireccion(int id, [FromBody] DireccionRequest request)
        {
            Result<string> result = new Result<string>();
            try
            {
                var model = _workSpace.DireccionesRepo.GetById(id);
                if (model == null)
                {
                    result.MessageError = "La direccion no fue encontrada";
                    return NotFound(result);
                }

                model.Pais = request.Pais;
                model.Direccion = request.Direccion;
                model.Ciudad = request.Ciudad;

                _workSpace.DireccionesRepo.Update(model);
                _workSpace.DireccionesRepo.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(result);
            }
        }

        [HttpPut("contacto/actualizar/{id}")]
        public ActionResult<Result<string>> PutContacto(int id, [FromBody] ContactoRequest request)
        {
            Result<string> result = new Result<string>();
            try
            {
                var model = _workSpace.ContactosRepo.GetById(id);
                if (model == null)
                {
                    result.MessageError = "La direccion no fue encontrada";
                    return NotFound(result);
                }

                model.Contacto = request.Contacto;
                model.Tipo = request.Tipo;
                _workSpace.ContactosRepo.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(result);
            }
        }
        #endregion

        #region Delete 
        [HttpDelete("persona/eliminar/{id}")]
        public ActionResult<Result<string>> DeletePersona(int id)
        {
            Result<string> result = new Result<string>();
            try
            {
                var model = _workSpace.PersonaRepo.GetById(id);
                if(model == null)
                {
                    result.MessageError = "La persona no fue encontrada";
                    return NotFound(result);
                }

                _workSpace.PersonaRepo.Delete(id);
                _workSpace.PersonaRepo.Save();

                return NoContent();
            }catch(Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(result);
            }
        }

        [HttpDelete("direccion/eliminar/{id}")]
        public ActionResult<Result<string>> DeleteDireccion(int id)
        {
            Result<string> result = new Result<string>();
            try
            {
                var model = _workSpace.DireccionesRepo.GetById(id);
                if (model == null)
                {
                    result.MessageError = "La direccion no fue encontrada";
                    return NotFound(result);
                }

                _workSpace.DireccionesRepo.Delete(id);
                _workSpace.DireccionesRepo.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(result);
            }
        }


        [HttpDelete("contacto/eliminar/{id}")]
        public ActionResult<Result<string>> DeleteContacto(int id)
        {
            Result<string> result = new Result<string>();
            try
            {
                var model = _workSpace.DireccionesRepo.GetById(id);
                if (model == null)
                {
                    result.MessageError = "el contacto no fue encontrada";
                    return NotFound(result);
                }

                _workSpace.DireccionesRepo.Delete(id);
                _workSpace.DireccionesRepo.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(result);
            }
        }

        #endregion

        #region ChangeStatus
        [HttpPut("persona/cambiar-estado/{id}")]
        public ActionResult<Result<string>> ChangeStatusPersona(int id, bool status)
        {
            Result<string> result = new Result<string>();
            try
            {
                var model = _workSpace.PersonaRepo.GetById(id);
                if(model == null)
                {
                    result.MessageError = "La persona no fue encontrada";
                    return NotFound(result);
                }

                model.Status = status;
                _workSpace.PersonaRepo.Save();

                return NoContent();
            }catch(Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(ex);
            }
        }

        [HttpPut("direcccion/cambiar-estado/{id}")]
        public ActionResult<Result<string>> ChangeStatusDireccion(int id, bool status)
        {
            Result<string> result = new Result<string>();
            try
            {
                var model = _workSpace.DireccionesRepo.GetById(id);
                if (model == null)
                {
                    result.MessageError = "La direccion no fue encontrada";
                    return NotFound(result);
                }

                model.Status = status;
                _workSpace.DireccionesRepo.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(ex);
            }
        }
        [HttpPut("contacto/cambiar-estado/{id}")]
        public ActionResult<Result<string>> ChangeStatusContacto(int id, bool status)
        {
            Result<string> result = new Result<string>();
            try
            {
                var model = _workSpace.ContactosRepo.GetById(id);
                if (model == null)
                {
                    result.MessageError = "El contacto no fue encontrado";
                    return NotFound(result);
                }

                model.Status = status;
                _workSpace.ContactosRepo.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                result.MessageError = _workSpace.ErrorMessage(ex);
                return BadRequest(ex);
            }
        }
        #endregion

    }
}
