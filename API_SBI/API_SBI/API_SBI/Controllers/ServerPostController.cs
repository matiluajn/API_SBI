using API_SBI.Entities;
using API_SBI.Entities.Dtos;
using API_SBI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SBI.Controllers
{
    
    [ApiController]
    [Route("api/")]
    public class ServerPostController : Controller
    {        

        private readonly ServerPostService _ServerPostService;

        private readonly IMapper _mapper;


        public ServerPostController(ServerPostService ServerPostService, IMapper mapper)
        {           
            _ServerPostService = ServerPostService;
            _mapper = mapper;
        }

        /// <summary>
        /// Metodo para traer todos los Usuarios de la api
        /// </summary>
        /// <returns>200 ok y listado completo de Usuarios</returns>
        [HttpGet("GetAllUsers")]
        [AllowAnonymous]
        public IActionResult GetAllUsers()
        {
            try
            {                
                var allUsers = _ServerPostService.GetAllServerPost();

                if (allUsers == null)
                    return StatusCode(401, new { code = 2, message = "No se encontró ningun Usuario" });

                //AutoMapper
                var allUsersMapp = _mapper.Map<ICollection<ServerPost>, ICollection<Salida>>(allUsers);

                

                return StatusCode(200, allUsersMapp);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hubo un problema: " + ex);
               
            }

        }

        /// <summary>
        /// Metodo para traer Usuario de la api q coincida con el id
        /// </summary>
        /// <returns>200 ok y listado completo de Usuarios</returns>
        [HttpGet("GetUsersById/{id}")]
        [AllowAnonymous]
        public IActionResult GetUsersById(int id)
        {
            try
            {
                var UsersById = _ServerPostService.GetServerPostbyId(id);

                if (UsersById == null)
                    return StatusCode(401, new { code = 2, message = "No se encontró ningun Usuario" });

                //AutoMapper
                var UserByIdMapp = _mapper.Map<ServerPost, Salida>(UsersById);

                return StatusCode(200, UserByIdMapp);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hubo un problema: " + ex);

            }

        }
    }
}
