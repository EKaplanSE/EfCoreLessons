﻿// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Tracking;

Console.WriteLine("Hello, World!");

ETicaretDbContext context = new();
await context.Database.MigrateAsync();

#region AsNoTracking Metodu
//Context üzerinden gelen tüm datalar Change Tracker mekanizması tarafından
//takip edilmektedir.

//Change Tracker, takip ettiği nesnelerin sayısıyla doğru orantılı olacak
//şekilde bir maliyete sahiptir. O yüzden üzerinde işlem yapılmayacak
//verilerin takip edilmesi bizlere lüzumsuz yere bir maliyet ortaya çıkaracaktır.

//AsNoTracking metodu, context üzerinden sorgu neticesinde gelecek olan
//verilerin Change Tracker tarafından takip edilmesini engeller.

//AsNoTracking metodu ile Change Tracker'ın ihtiyaç olmayan verilerdeki
//maliyetini törpülemiş oluruz.

//AsNoTracking fonksiyonu ile yapılan sorgulamalarda, verileri elde edebilir,
//bu verileri istenilen noktalarda kullanabilir lakin veriler üzerinde
//herhangi bir değişiklik/update işlemi yapamayız.

//var kullanicilar = await context.Kullanicilar.AsNoTracking().ToListAsync();
//foreach (var kullanici in kullanicilar)
//{
//    Console.WriteLine(kullanici.Adi);
//    kullanici.Adi = $"yeni-{kullanici.Adi}";
//    context.Kullanicilar.Update(kullanici);
//}
//await context.SaveChangesAsync();

#endregion

#region AsNoTrackingWidthIdentityResolution
//CT(Change Tracker) mekanizması yinelenen verileri tekil instance olarak
//getirir. Buradan ekstradan bir performans kazancı söz konusudur.

//Bizler yaptığımız sorgularda takip mekanizmasının AsNoTracking metodu ile
//maliyetini kırmak isterken bazen maliyete sebebiyet verebiliriz.
//(Özellikle ilişkisel tabloları sorgularken bu duruma dikkat etmemiz gerekyior)

//AsNoTracking ile elde edilen veriler takip edilmeyeceğinden dolayı
//yinelenen verilerin ayrı instancelarda olmasına sebebiyet veriyoruz.
//Çünkü CT mekanizması takip ettiği nesneden bellekte varsa eğer aynı
//nesneden birdaha oluşturma gereği duymaksızın o nesneye ayrı noktalardaki
//ihtiyacı aynı instance üzerinden gidermektedir.

//Böyle bir durumda hem takip mekanizmasının maliyeitni ortadan kaldırmak
//hemide yinelenen dataları tek bir instance üzerinde karşılamak için
//AsNoTrackingWithIdentityResolution fonksiyonunu kullanabiliriz.

//var kitaplar = await context.Kitaplar.Include(k => k.Yazarlar)
//                     .AsNoTrackingWithIdentityResolution().ToListAsync();

//AsNoTrackingWithIdentityResolution fonksiyonu AsNoTracking fonksiyonuna
//nazaran görece yavaştır/maliyetlidir lakin CT'a nazaran daha performanslı
//ve az maliyetlidir.

#endregion

#region AsTracking
//Context üzerinden gelen dataların CT tarafından takip ewdilmesini iradeli
//bir şekilde ifade etmemizi sağlayan fonksiyondur.
//Peki hoca niye kullanalım ?
//Bir sonraki inceleyeceğimiz UseQueryTrackingBehavior metodunun davranışı
//gereği uygulama seviyesinde CT'ın default olarak devrede olup olmamasını
//ayarlıyor olacağız. Eğer ki default olarak pasif hale getirilirse böyle
//durumda takip mekanizmasının ihtiyaç olduğu sorgularda AsTracking
//fonksiyonunu kullanabilir ve böylece takip mekanizmasını iradeli bir
//şekilde devreye sokmuş oluruz.

//var kitaplar = await context.Kitaplar.AsTracking().ToListAsync();

#endregion

#region UseQueryTrackingBehavior
//EF Core seviyesinde/uygulama seviyesinde ilgili contextten gelen verilerin
//üzerinde CT mekanizmasının davranışı temel seviyede belirlememizi sağlayan
//fonksiyondur. Yani konfigürasyon fonksiyonudur.
#endregion