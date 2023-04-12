﻿namespace RPG_Game.Dtos.Fight
{
    public class HighScoreDto
    {

        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;
        public int Fight { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }

    }
}
