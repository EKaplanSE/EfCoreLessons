using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking
{
    public class ETicaretDbContext : DbContext
    {
        public DbSet<Kullanici> Kullanicilar { get; set;}
        public DbSet<Rol> Roller { get; set;}
        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Yazar> Yazarlar { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=ECommerce; Trusted_Connection=true");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}

public class Kullanici
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public ICollection<Rol> Roller { get; set; }
}
public class Rol
{
    public int Id { get; set; }
    public string RolAdi { get; set; }
    public ICollection<Kullanici> Kullanicilar { get; set; }
}
public class Kitap
{
    public Kitap() => Console.WriteLine("Kitap nesnesi oluşturuldu.");
    public int Id { get; set; }
    public string KitapAdi { get; set; }
    public int SayfaSayisi { get; set; }

    public ICollection<Yazar> Yazarlar { get; set; }
}
public class Yazar
{
    public Yazar() => Console.WriteLine("Yazar nesnesi oluşturuldu.");
    public int Id { get; set; }
    public string YazarAdi { get; set; }

    public ICollection<Kitap> Kitaplar { get; set; }
}