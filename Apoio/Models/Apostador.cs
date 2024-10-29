namespace Apoio.Models
{
    public class Apostador
    {
        public int Id { get; set; }
        // [DisplayName("NickName")]
        public string Name { get; set; }
        //[MaxLength(1000)]
        public string Email { get; set; }
        // public decimal? Saldo { get; set; } -> Exemplo de nulo
        public decimal Saldo { get; set; }

    }
}