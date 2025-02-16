using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SiparisDetay
    {
        public int Id { get; set; }
        public int SiparisId { get; set; }
        public string UrunAdi { get; set; }
        public int UrunId { get; set; }
        public int Miktar { get; set; }
        public decimal Fiyat { get; set; }
    }
}
