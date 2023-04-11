using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RPG_Game.Data;
using RPG_Game.Dtos.Character;
using RPG_Game.Models;
using System.Security.Claims;

namespace RPG_Game.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;

        private readonly DataContext _context;

        private readonly IHttpContextAccessor _httpContextAcessor;


        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAcessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAcessor = httpContextAcessor;
        }

        private int GetUserId() => int.Parse(_httpContextAcessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();

            var dbCharacters = await _context.Characters.Where(c => c.User.Id == GetUserId()).ToListAsync();

            response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
          
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
          
            response.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
          
            if (response.Data == null)
            {
                response.Message = "Your are not owner of this character!";
                response.Success = false;
            }

            return response; 
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
          
            Character character = _mapper.Map<Character>(newCharacter); 

            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
         
            _context.Add(character);
         
            await _context.SaveChangesAsync();
        
            response.Data = await _context.Characters
                .Where(c => c.User.Id == GetUserId())
                .Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
         
            return response;
        }


        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            
            try
            {
                var character = await _context.Characters.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

                if(character.User.Id == GetUserId())
                {

                _mapper.Map(updatedCharacter, character);

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Character not found!";
                }
            } 
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            
            try
            {

                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());

                if (character != null)
                {
                    _context.Characters.Remove(character);
                    
                    await _context.SaveChangesAsync();

                    response.Data = _context.Characters.Where(c => c.User.Id == GetUserId())
                        .Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

                }
                else
                {
                    response.Success = false;
                    response.Message = "Characer not found";
                }
            } 
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;

        }


    }
        
}
