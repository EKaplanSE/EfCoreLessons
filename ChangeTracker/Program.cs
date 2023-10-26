// See https://aka.ms/new-console-template for more information

using ChangeTracker;
using Microsoft.EntityFrameworkCore;

ECommerceDbContext context = new();

#region Change Trcking Neydi?
//context nesnesi üzerinden gelen tüm veriler otomatik olarak bir takip
//mekanizması tarafından izlenir. Bu mekanizmaya Change Tracker denir.
//Change tracker ile nesneler üzerindeki değişiklikler/işlemler
//takip edilerek netice itibariyle bu işlemlerin fıtratına uygun sql
//sorgucukları generate edilir. İşte bu işleme de Change Tracking denir.
#endregion

#region Changetracker Property'si

//Takip edilen nesnelere erişebilmemizi sağlayan ve gerektiği takdirde
//işlemler gerçekleştirmemizi sağlayan bir propert'dir.
//Context sınıfının base classı olan Dbcontext sınıfının bir member'ıdır.

//var products = await context.Products.ToListAsync();

//products[6].Price = 123; //update
//context.Products.Remove(products[7]); //delete 
//products[8].Name = "asdsss"; //update

//var datas = context.ChangeTracker.Entries();

//await context.SaveChangesAsync();

#region DetectChanges Metodu
//EF Core context nesnesi tarafından izlenen tüm nesnelerdeki değişiklikleri 
//Change tracker sayesinde takip edebilmekte ve nesnelerde olan verisel değişiklikler 
//yakalanarak bunların anlık görüntülerini oluşturabilir. 
//Yapılan değişikliklerin veritbanına gönderilmeden önce algılandığından emin
//olmak gerekir. SaveChanges fonksiyonu çağrıldığı anda nesneler EF Core tarafından
//otomatik kontrol edilirler.
//Ancak yapılan operasyonlarda güncel tracking verilerinden emin olabilmek için
//değişikliklerin algılanmasını opsiyonel olarak gerçekleştirmek isteyebiliriz.
//İşte bunun için DetectChanges fonksiyonu kullanılabilir ve her ne kadar
//EF Core değişiklikleri otomatik algılıyor olsa da siz yine de iradenizle kontrole
//zorlayabilirsiniz.

var product = await context.Products.FirstOrDefaultAsync(p => p.Id==3);
product.Price = 123;

context.ChangeTracker.DetectChanges();
await context.SaveChangesAsync();

#endregion

#region AutoDetectChangesEnabled Property'si
//İlgili metodlar(SaveChanges, Entries) tarafından DetectChanges metodunun
//otomatik olarak tetiklenmesinin konfigürasyonunu sağlayan propertydir.
//SaveChanges fonksiyonu tetiklendiğinde DetectChanges metodunu içerisinde 
//default olarak çağırmaktadır. Bu durumda DetectChanges metodunun kullanımını
//irademizle yönetmek ve maliyet/performans optimizasyonu yapmak istediğimiz durumlarda
//AutoDetectChanges özelleliğini kapatabiliriz.
#endregion

#region Entries Metodu
//Conext'teki Entry metodunun koleksiyonel versiyonudur.
//ChangeTracker mekanizması tarafından izlenen her entity nesnesinin bilgisini
//EntityEntry türünden elde etmemizi sağlar ve belirl işlemler yapabilmemize olanak sağlar.
//Entries metodu DetectChanges metodunu tetikler. Bu durum da tıpkı SaveChanges'da 
//olduğu gibi bir maliyetttir.
//Buradaki maliyetten kaçınmak için autoDetectChanges özelliğine false değeri verilebilir.

//var products = await context.Products.ToListAsync();

//products.FirstOrDefault(p => p.Id==7).Price = 123; //update
//context.Products.Remove(products.FirstOrDefault(p => p.Id==8); //delete 
//products.FirstOrDefault(p => p.Id==9).Name = "asdsss"; //update

//context.ChangeTracker.Entries().ToList().ForEach(e =>
//{ 
//    if (e.State == EntityState.Unchanged)
//    {
//        //....
//    }
//    else if (e.State == EntityState.Deleted)
//    {
//        //....
//    }

//});

#endregion

#region AcceptAllChanges Metodu
//SaveChanges() veya SaveChanges(true) tetiklendiğinde EF Core herşeyin yolunda
//olduğunu varsayarak track ettiği verilerin takibini keser yeni değişikliklerin 
//takip edilmesini bekler. böyle bir durumda beklenmeyen bir durum/olası bir 
//hata söz konusu olursa eğer EF Core takip ettiği nesneleri bırakacağı için
//bir düzeltme mevzu bahis olmayacaktır.

//Haliyle bu durumda devreye SaveChanges(false) ve AcceptAllChanges() metotları
//girecektir.

//SaveChanges(false), EF Core'a gerekli veritabanı komutlarını yürütmesini söyler
//ancak gerektiğinde yeniden oynatılabilmesi için değişiklikleri beklemeye/nesneleri
//takip etmeye devam eder. Ta ki AcceptAllChanges metodunu irademizle çağırana kadar.

//SaveChanges(flase) ile işlemin başarılı olduğundan emin olursanız AcceptAllChanges
//metodu ile nesnelerden takibi kesebilirisiniz.

//var products = await context.Products.ToListAsync();

//products.FirstOrDefault(p => p.Id == 7).Price = 123; //update
//context.Products.Remove(products.FirstOrDefault(p => p.Id == 8); //delete 
//products.FirstOrDefault(p => p.Id == 9).Name = "asdsss"; //update

//await context.SaveChangesAsync(false);
//context.ChangeTracker.AcceptAllChanges();

#endregion

#region HasChanges Metodu
//Takip edilen nesneler arasınd değişiklik yapılanların olup olmadığı 
//bilgisini verir.
//Arka planda DetectChanges metodunu tetikler.

//var result = context.ChangeTracker.HasChanges();
#endregion

#region Entity States
//Entity nesnelerinin durumlarını ifade eder.

#region Detached
//Nesnenin ChangeTracker tarafından takip edilmediğini ifade eder.
//var urun = new();
//Console.WriteLine(context.Entry(urun).State);
//urun.Name = "asasada";
//await context.SaveChangesAsync();
#endregion

#region Added
//Veritabanına eklenecek ama henüz eklenmeyen nesneyi ifade eder.
//SaveChanges fonksiyonu çağrıldığında insert sorgusu oluşturulağı anlamına gelir.

//var urun = new() { Price = 123, Name = "Ürün 001" };
//Console.WriteLine(context.Entry(urun).State); //Detached
//await context.Products.AddAsync(urun);
//Console.WriteLine(context.Entry(urun).State); //Added
//await context.SaveChangesAsync();
//urun.Price = 321;
//Console.WriteLine(context.Entry(urun).State); //Modified
//await context.SaveChangesAsync();

#endregion

#region Unchanged
//Veritabanında sorgulandığındaan beri nesne üzerinde herhangi bir değişiklik
//yapılmadığını ifade eder. Sorgu neticesinde elde edilen tüm nesneler başlangıçta
//bu state değerindedir.

//var urunler = await context.Products.ToListAsync();

//var data = context.ChangeTracker.Entries();

#endregion

#region Modified
//Nesne üzerinde değişiklik yapıldığını ifade eder. SaveChanges fonksiyonu
//çağırıldığında Update sorgusu oluşturulacağı anlamına gelir.

//var urun = await context.Products.FirstOrDefaultAsync(u => u.Id == 3);
//Console.WriteLine(context.Entry(urun).State); //Unchanged
//urun.Name = "asdada";
//Console.WriteLine(context.Entry(urun).State); //Modified
//await context.SaveChangesAsync();
//Console.WriteLine(context.Entry(urun).State); //Unchanged

#endregion

#region Deleted
//Nesnenin silindiğini ifade eder.SaveChanges fonksiyonuçağırıldığında
//Delete sorgusu oluşturulacağı anlamına gelir.

//var urun = await context.Products.FirstOrDefaultAsync(u => u.Id == 3);
//context.Products.Remove(urun);
//Console.WriteLine(context.Entry(urun).State); //Deleted
//await context.SaveChangesAsync();

#endregion

#endregion

#endregion

#region Context nesnesi üzerinden Change Tracker
var urun = await context.Products.FirstOrDefaultAsync(u => u.Id==55);
urun.Price = 123;
urun.Name = "Silgi"; //Modified | Update

#region Entry Metodu
#region OriginalValues Property'si
//var fiyat = context.Entry(urun).OriginalValues.GetValue<decimal>(nameof(urun.Price));
//var urunAdi = context.Entry(urun).OriginalValues.GetValue<string>(nameof(urun.Name));
#endregion
#region CurrentValues Property'si
//var urunAdi = context.Entry(urun).CurrentValues.GetValue<string>(nameof(urun.Name));
#endregion
#region DatabseValues Property'si
var _urun = await context.Entry(urun).GetDatabaseValuesAsync();
#endregion

#endregion

#endregion

