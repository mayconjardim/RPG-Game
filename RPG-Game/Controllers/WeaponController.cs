using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPG_Game.Dtos.Character;
using RPG_Game.Dtos.Weapon;
using RPG_Game.Models;
using RPG_Game.Services.WeaponService;

namespace RPG_Game.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddWeapon(AddWeaponDto newWeapon)
        {
            return Ok(await _weaponService.AddWeapon(newWeapon));
        }

    }
}
