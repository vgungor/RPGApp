
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Services.CharacterService;
using System.Threading.Tasks;
using Dtos.Character;
using Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;

        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _characterService.GetAllCharacters(id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _characterService.GetChartacterById(id));
        }

        [HttpGet("Getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _characterService.GetChartacterById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddCharacterDto character)
        {
            return Ok(await _characterService.AddCharacter(character));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto character){
            ServiceResponse<GetCharacterDto> responce = await _characterService.UpdateCharacter(character);
            if (responce.Success)
            {
                return Ok(responce);
            }

            return NotFound(responce);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCharacter(int id){
            ServiceResponse<List<GetCharacterDto>> responce = await _characterService.DeleteCharacter(id);
            if (responce.Success)
            {
                return Ok(responce);
            }
            return BadRequest(responce);

        }





    }
}