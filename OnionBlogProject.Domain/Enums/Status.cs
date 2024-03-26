using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Domain.Enums
{
    public enum Status // Entity'lerimize Durum eklemek için kullanabiliriz. örn: Active User aktif hesaplar olarak Passive ler dondurulmuş veya kapatılmış hesaplar gibi.
    {
        Active = 1,
        Modified = 2,
        Passive = 3
    }
}
