﻿using RPG_Game.Models;

namespace RPG_Game.Services.CharacterService
{
    public interface ICharacterService
    {

        Task<ServiceResponse<List<Character>>> GetAllCharacters();

        Task<ServiceResponse<Character>> GetCharacterById(int id);

        Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter);

    } 
}
