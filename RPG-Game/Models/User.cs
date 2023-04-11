namespace RPG_Game.Models
{
    public class User
    {

        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] passwordSalt { get; set; }

    }
}
