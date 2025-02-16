using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Urun
    {
        public int Id { get; set; }
        public string UrunKodu { get; set; }
        public string UrunAdi { get; set; }
        public decimal Fiyat { get; set; }
    }
}
