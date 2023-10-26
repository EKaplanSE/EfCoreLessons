// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Querying;

Console.WriteLine("Hello, World!");

ECommerceDbContext context = new();

#region En Temel Basit bir Sordulama Nasıl Yapılır?

#region Method Syntax
var products = await context.Products.ToListAsync();
#endregion

#region Query syntax
var products2 = await (from product in context.Products
                       select product).ToListAsync();
#endregion
#endregion

#region Sorguyu Execute Etmek için ne yapmalıyım?
#region ToListAsync
var products5 = await (from product in context.Products
                select product).ToListAsync();
#endregion
#region Foreach
var products6 = from product in context.Products
                select product;

foreach(Product product in products6)
{
    Console.WriteLine(product.Name);
}
#endregion

#endregion

#region IQeryable ve IEnumerable Nedir?

#region IQueryable
var products3 = from product in context.Products
                select product;
#endregion

#region IEnumerable
var products4 = await (from product in context.Products
                select product).ToListAsync();
#endregion

#endregion

#region Deferred Execute (Ertelenmiş Çalışma)

int urunId = 2;
var urunler = from urun in context.Products
              where urun.Id == urunId
              select urun;
urunId = 100;

foreach(Product urun in urunler)
{
    Console.WriteLine(urun.Name);
}



#endregion

#region Çoğul Veri Getiren Sorgulama Fonksiyonları

#region ToListAsync

#endregion

#region Where
//Method syntax
var products7 = await context.Products.Where(p => p.Id > 500).ToListAsync();

//Query syntax
var products8 = from product in context.Products
            where product.Id > 500 && product.Name.EndsWith("7")
            select product;
var data = await products8.ToListAsync();

#endregion

#region OrderBy
//Metod Syntax
var products9 = context.Products.Where(p => p.Id > 500 && p.Name.EndsWith("2")).OrderBy(p => p.Name);
await products9.ToListAsync();

//Query syntax
var products10 = from product in context.Products
                 where product.Id > 500 || product.Name.StartsWith("2")
                 orderby product.Name
                 select product;
await products10.ToListAsync();
#endregion

#region ThenBy
var products11 = context.Products.Where(p => p.Id > 500 && p.Name.EndsWith("2")).OrderBy(p => p.Name)
                .ThenBy(p => p.Price).ThenBy(p => p.Id);
await products11.ToListAsync();

#endregion

#region OrderByDescending
//Method Syntax
var products12 = await context.Products.OrderByDescending(p => p.Price).ToListAsync();

//Query syntax
var products13 = await (from product in context.Products
                        orderby product.Name descending
                        select product).ToListAsync();


#endregion

#region ThenByDescending

var products14 = await context.Products.OrderByDescending(p => p.Id).ThenByDescending(p => p.Name).ToListAsync();


#endregion

#endregion

#region Tekil Veri Getiren Sorgulama Fonksiyonları

#region SingleAsync
var product1 = await context.Products.SingleAsync(p => p.Id == 55);
#endregion

#region SingleAsync
var product2 = await context.Products.SingleOrDefaultAsync(p => p.Id == 55);
#endregion

#region FirstAsync
var product3 = await context.Products.FirstAsync(p => p.Id > 55);
#endregion

#region FirstAsync
var product4 = await context.Products.FirstOrDefaultAsync(p => p.Id > 55);
#endregion

#region FindAsync
var product5 = await context.Products.FindAsync(55);
#endregion

#region LastAsync
var product6 = await context.Products.OrderBy(p => p.Name).LastAsync(p => p.Id > 55);
#endregion

#region LastOrDefaultAsync
var product7 = await context.Products.OrderBy(p => p.Price).LastOrDefaultAsync(p => p.Id > 55);
#endregion

#endregion

#region Diğer Sorgulama Fonksiyonları
#region CountAsync
var productCount = await context.Products.CountAsync();
productCount = await context.Products.CountAsync(p => p.Price > 50);
#endregion

#region LongCountAsync
var productCount2 = await context.Products.LongCountAsync();
#endregion

#region AnyAsync
var productExist = await context.Products.AnyAsync();
productExist = await context.Products.Where(p => p.Name.Contains("A")).AnyAsync();
productExist = await context.Products.AnyAsync(p => p.Name.Contains("A"));
#endregion

#region MaxAsync
var maxPrice = await context.Products.MaxAsync(p => p.Price);
#endregion

#region MinAsync
var minPrice = await context.Products.MinAsync(p => p.Price);
#endregion

#region Distinct
var products15 = await context.Products.Distinct().ToListAsync();
#endregion

#region AllAsync
//Bir sorgu neticesinde gelen verilerin, verilen şarta uyup uymadığını kontrol eder.
//Eğer tüm veriler şarta uyuyorsa true, uymuyorsa false döndürür.
var u = await context.Products.AllAsync(p => p.Price > 5000);
#endregion

#region SumAsync
//Vermiş olduğumuz sayısal propertynin toplamını getirir.
var sumOfPrice = await context.Products.SumAsync(p => p.Price);
#endregion 

#region AvarageAsync
//Vermiş olduğumuz sayısal propertynin aritmetik ortalamasını getirir.
var avaragePrice = await context.Products.AverageAsync(p => p.Price);
#endregion

#region ContainsAsync
//Like sorgusu oluşturmamızı sağlar. ... İçinde geçenleri getirir.
var ptoducts16 = await context.Products.Where(p => p.Name.Contains("7")).ToListAsync();
#endregion

#region StartsWith
//Like sorgusu oluşturmamızı sağlar. ... ile başlayanları getirir.
var ptoducts17 = await context.Products.Where(p => p.Name.StartsWith("7")).ToListAsync();
#endregion

#region StartsWith
//Like sorgusu oluşturmamızı sağlar. ... ile bitenleri getirir.
var ptoducts18 = await context.Products.Where(p => p.Name.EndsWith("7")).ToListAsync();
#endregion

#endregion

#region Sorgu Sonucu Dönüşüm Fonksiyonları
//Bu fonksiyonlar ile sorgu neticesinde elde edilen verileri farklı türlerde
//projeksiyon edebiliriz.
#region ToDictionaryAsync
//Sorgu neticesinde gelecek veriyi dictionary olarak elde etmek isteniyorsa kullanılır.
var products16 = await context.Products.ToDictionaryAsync(p => p.Name, p => p.Price);
#endregion

#region ToArrayAsync
var products17 = await context.Products.ToArrayAsync();
#endregion

#region Select
//1.Generate edilecek sorgunun çekilecek kolonlarını ayarlamamızı sağlar.
var products18 = await context.Products.Select(p => new Product
{
    Id = p.Id,
    Price = p.Price
}).ToListAsync();

//2.Gelen verileri farklı türlerde karşılamamızı sağlar. T, anonim

//Anonim karşılama
var products19 = await context.Products.Select(p => new
{
    Id = p.Id,
    Price = p.Price
}).ToListAsync();

//Farklı türde karşılama
var products20 = await context.Products.Select(p => new ProductDetail
{
    ProductId = p.Id,
    ProductPrice = p.Price
}).ToListAsync();

#endregion

#region SelectMany
//Select ile ynı amaca hizmet eder. Ancak ilişkisel tablolar neticesinde gelen
//koleksiyonel verileri de tekilleştirip projeksiyon etmemizi sağlar.
var products21 = await context.Products.Include(p => p.Parts)
         .SelectMany(p => p.Parts, (p, pp) => new
{
    p.Id,
    p.Price,
    pp.Name
}).ToListAsync();
#endregion

#endregion

#region GroupBy Fonksiyonu

#region Method syntax
var datas = await context.Products.GroupBy(p => p.Price).Select(group => new 
{
    Count = group.Count(),
    Price = group.Key
}).ToListAsync();
#endregion
#region Query syntax
var datas2 = await (from product in context.Products
             group product by product.Price
             into @group
             select new
             {
                 Price = @group.Key,
                 Count = @group.Count()
             }).ToListAsync();
#endregion
#endregion

#region Foreach Fonksiyonu
datas2.ForEach(d =>
{
    Console.WriteLine(d.Price + " " + d.Count);
});
#endregion

