//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCOyun.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Oyun
    {
        public int id { get; set; }
        public Nullable<int> KategoriId { get; set; }
        public string Ad { get; set; }
        public string Foto { get; set; }
        public string Icerik { get; set; }
        public Nullable<int> Puan { get; set; }
        public string Sirket { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public Nullable<int> Fiyat { get; set; }
    
        public virtual Kategori Kategori { get; set; }
    }
}
