using RPG_Game.Dtos.Character;
using RPG_Game.Dtos.Weapon;
using RPG_Game.Models;

namespace RPG_Game.Services.WeaponService
{
    public interface IWeaponService
    {

        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto addWeapon);

    }
}
