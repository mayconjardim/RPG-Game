﻿using AutoMapper;
using RPG_Game.Dtos.Character;
using RPG_Game.Dtos.Fight;
using RPG_Game.Dtos.Skill;
using RPG_Game.Dtos.Weapon;
using RPG_Game.Models;

namespace RPG_Game.Profiles
{
    public class AutoMapperProfile : Profile
    {

      public AutoMapperProfile() { 
        
        CreateMap<Character, GetCharacterDto>();
        CreateMap<AddCharacterDto, Character>();
        CreateMap<UpdateCharacterDto, Character>();
        CreateMap<Weapon, GetWeaponDto>();
        CreateMap<Skill, GetSkillDto>();
        CreateMap<Character, HighScoreDto>();
        }
    }
}
