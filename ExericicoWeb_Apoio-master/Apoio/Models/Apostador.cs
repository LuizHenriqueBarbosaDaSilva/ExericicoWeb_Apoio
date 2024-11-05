namespace Apoio.Models
{
    public class Apostador
    {
        public int Id { get; set; }
        // [DisplayName("NickName")]
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Saldo { get; set; }

    }
}