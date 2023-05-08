namespace TrenRezervasyonApi.Models
{
    public class RezervasyonOutput
    {
        public bool RezervasyonYapilabilir { get; set; }
        public List<YerlesimAyrinti>? YerlesimAyrinti { get; set; }//Rezervasyon yapılamıyorsa YerlesimAyrinti bos array olacaktır
    }
}
