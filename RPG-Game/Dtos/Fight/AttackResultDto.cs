﻿namespace RPG_Game.Dtos.Fight
{
    public class AttackResultDto
    {

        public string Attacker { get; set; }
        public string Opponent { get; set; }
        public int AttackerHp { get; set; }
        public int OpponentHP { get; set; }
        public int Damage { get; set; }

    }
}
