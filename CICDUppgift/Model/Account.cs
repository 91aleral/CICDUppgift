namespace CICDUppgift.Model
{
    /// <summary>
    /// Konto modell för Användare/Admin.
    /// </summary>
    public class Account
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string accountType { get; set; }
    }
}
