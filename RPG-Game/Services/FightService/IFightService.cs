using RPG_Game.Dtos.Fight;
using RPG_Game.Models;

namespace RPG_Game.Services.FightService
{
    public interface IFightService
    {

        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);
    }
}
