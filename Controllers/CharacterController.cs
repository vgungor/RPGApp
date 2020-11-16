
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController: ControllerBase
    {
        private static Character knight = new Character();
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character(){Id=1, Name = "Sam"},
            new Character(){Id=2, Name = "Adam"}
        };
        
        [HttpGet("Get")]
        public IActionResult Get(){
            return Ok(characters);
        }
        
        [HttpGet("GetSingle/{id}")]
        public IActionResult GetSingle(int id){
            return Ok(characters.FirstOrDefault(p => p.Id == id));
        }





    }
}