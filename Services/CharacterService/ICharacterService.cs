using System.Collections.Generic;
using System.Threading.Tasks;
using Dtos.Character;
using Models;

namespace Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
        Task<ServiceResponse<GetCharacterDto>> GetChartacterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character); 
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto character);

        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id); 

    }
}