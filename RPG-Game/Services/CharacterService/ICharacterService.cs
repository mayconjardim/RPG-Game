﻿using RPG_Game.Dtos.Character;
using RPG_Game.Models;

namespace RPG_Game.Services.CharacterService
{
    public interface ICharacterService
    {

        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();

        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);

        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);

        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter
    } 
}
