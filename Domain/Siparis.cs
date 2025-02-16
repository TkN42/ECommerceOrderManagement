using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Siparis
    {
        public int Id { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public int KullaniciId { get; set; }
    }
}
