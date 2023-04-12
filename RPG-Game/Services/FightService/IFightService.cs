﻿using RPG_Game.Dtos.Fight;
using RPG_Game.Models;

namespace RPG_Game.Services.FightService
{
    public interface IFightService
    {

        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);

        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request);

        Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request);

        Task<ServiceResponse<List<HighScoreDto>>> GetHighScore();


    }
}
