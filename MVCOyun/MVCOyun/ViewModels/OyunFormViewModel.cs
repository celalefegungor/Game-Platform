using MVCOyun.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCOyun.ViewModels
{
    public class OyunFormViewModel
    {
        public IEnumerable<Kategori> Kategoris { get; set; }
        public Oyun Oyun { get; set; }
    }
}