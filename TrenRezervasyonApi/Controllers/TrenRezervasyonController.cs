using System.Collections.Generic;
using System.Linq;
using TrenRezervasyonApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace TrenRezervasyon.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RezervasyonController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(RezervasyonInput input)
        {
            var yerlesimAyrinti = new List<YerlesimAyrinti>();
            var rezervasyonYapilabilir = false;

            if (input.KisilerFarkliVagonlaraYerlestirilebilir)
            {
                foreach (var vagon in input.Tren.Vagonlar)
                {
                    var bosKoltukSayisi = vagon.Kapasite - vagon.DoluKoltukAdet;
                    var rezervasyonYapilabilecekKoltukSayisi = (int)(vagon.Kapasite * 0.7) - vagon.DoluKoltukAdet;

                    if (rezervasyonYapilabilecekKoltukSayisi >= input.RezervasyonYapilacakKisiSayisi)
                    {
                        yerlesimAyrinti.Add(new YerlesimAyrinti { VagonAdi = vagon.Ad, KisiSayisi = input.RezervasyonYapilacakKisiSayisi });
                        rezervasyonYapilabilir = true;
                        break;
                    }
                    else if (rezervasyonYapilabilecekKoltukSayisi > 0)
                    {
                        yerlesimAyrinti.Add(new YerlesimAyrinti { VagonAdi = vagon.Ad, KisiSayisi = rezervasyonYapilabilecekKoltukSayisi });
                        input.RezervasyonYapilacakKisiSayisi -= rezervasyonYapilabilecekKoltukSayisi;
                    }
                }
            }
            else
            {
                var uygunVagon = input.Tren.Vagonlar.FirstOrDefault(v => (int)(v.Kapasite * 0.7) - v.DoluKoltukAdet >= input.RezervasyonYapilacakKisiSayisi);

                if (uygunVagon != null)
                {
                    yerlesimAyrinti.Add(new YerlesimAyrinti { VagonAdi = uygunVagon.Ad, KisiSayisi = input.RezervasyonYapilacakKisiSayisi });
                    rezervasyonYapilabilir = true;
                }
            }

            var sonuc = new RezervasyonOutput
            {
                RezervasyonYapilabilir = rezervasyonYapilabilir,
                YerlesimAyrinti = yerlesimAyrinti
            };

            return Ok(sonuc);
        }
    }
}