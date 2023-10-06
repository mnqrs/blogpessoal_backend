namespace Blog_Pessoal.Security
{
    public class Settings
    {
        private static string secret = "74e215cf568d812786492c2d2d6dc134f766049d610580a29258386fdbb21f6f";

        public static string Key { get => secret; set => secret = value; }
    }
}
