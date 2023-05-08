namespace TrenRezervasyonApi.Models
{
    public class RezervasyonInput
    {
        public Tren Tren { get; set; }
        public int RezervasyonYapilacakKisiSayisi { get; set; }
        public bool KisilerFarkliVagonlaraYerlestirilebilir { get; set; }
    }
}
