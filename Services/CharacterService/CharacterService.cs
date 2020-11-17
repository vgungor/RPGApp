using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data;
using Dtos.Character;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.CharacterService;

namespace Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static Character knight = new Character();
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character(){Id=1, Name = "Sam"},
            new Character(){Id=2, Name = "Adam"}
        };
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character)
        {
            Character addedCharater = _mapper.Map<Character>(character);
            // addedCharater.Id = characters.Max(p => p.Id) + 1;
            // characters.Add(addedCharater);
            await _context.Characters.AddAsync(addedCharater);
            await _context.SaveChangesAsync();
            return new ServiceResponse<List<GetCharacterDto>>(_mapper.Map<List<GetCharacterDto>>(await _context.Characters.ToListAsync()));
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character deletedChar = await _context.Characters.FirstOrDefaultAsync(p => p.Id == id);
            if (deletedChar != null)
            {
                try
                {
                    _context.Characters.Remove(deletedChar);
                    await _context.SaveChangesAsync();
                    
                    serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(await _context.Characters.ToListAsync());
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Player deleted from history :)";
                    return serviceResponse;
                }
                catch (System.Exception)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Player CAN NOT be deleted from history :(";
                    return serviceResponse;
                }
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Player does not exists to be deleted ?:o";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            //serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(characters);
            serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(await _context.Characters.ToListAsync());
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetChartacterById(int id)
        {
            return new ServiceResponse<GetCharacterDto>(_mapper.Map<GetCharacterDto>(await _context.Characters.FirstOrDefaultAsync(p => p.Id == id)));
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.Characters.FirstOrDefaultAsync(p => p.Id == updatedCharacter.Id);
                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

                _context.Characters.Update(character);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                return serviceResponse;
            }
            catch (System.NullReferenceException)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "This player does not exists. Check your ID";
                return serviceResponse;
            }





        }
    }
}