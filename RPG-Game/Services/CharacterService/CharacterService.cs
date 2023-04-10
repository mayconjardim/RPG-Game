﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RPG_Game.Data;
using RPG_Game.Dtos.Character;
using RPG_Game.Models;

namespace RPG_Game.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;

        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();

            var dbCharacters = await _context.Characters.ToListAsync();

            response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
          
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
          
            response.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
          
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
          
            Character character = _mapper.Map<Character>(newCharacter);
         
            _context.Add(character);
         
            await _context.SaveChangesAsync();
        
            response.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
         
            return response;
        }


        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

                _mapper.Map(updatedCharacter, character);

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);

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

                Character character = await _context.Characters.FirstAsync(c => c.Id == id);
                _context.Characters.Remove(character);

                await _context.SaveChangesAsync();

            } catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;

        }


    }
        
}
