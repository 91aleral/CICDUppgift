namespace CICDUppgift.Model
{
    /// <summary>
    /// User ärver från Account - för att få tillgång till dess properties.
    /// Struktur till en användare.
    /// </summary>
    public class User : Account
    {
        public int ID { get; set; }
        public int salary { get; set; }
        public int balance { get; set; }
        public string role { get; set; }
    }
}
