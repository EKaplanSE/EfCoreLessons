// See https://aka.ms/new-console-template for more information


using CodeFirstExam;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

ECommerceDbContext context = new ECommerceDbContext();
//await context.Database.MigrateAsync();

#region Veri Ekleme
/*

Product product1 = new()
{
    Name = "Bardak",
    Price = 100,
    Quantity = 10
};
Product product2 = new()
{
    Name = "Tabak",
    Price = 150,
    Quantity = 15
};
Product product3 = new()
{
    Name = "Çatal",
    Price = 30,
    Quantity = 100
};
Console.WriteLine(context.Entry(product1).State);
//await context.AddAsync(product);
//await context.Products.AddAsync(product);

await context.Products.AddRangeAsync(product1, product2, product3);
Console.WriteLine(context.Entry(product1).State);

await context.SaveChangesAsync();
Console.WriteLine(context.Entry(product1).State);
Console.WriteLine(product1.Id + " " + product2.Id + " " + product3.Id);
*/
#endregion


#region Veri Güncelleme
/*
Product product = await context.Products.FirstOrDefaultAsync(p => p.Id == 2);
product.Name = "Kaşık";
product.Price = 40;
product.Quantity = 200;

await context.SaveChangesAsync();
*/
#endregion

#region Update Fonksiyonu
/*
Product product1 = new()
{
    Id = 1,
    Name = "Bardak",
    Price = 120,
    Quantity = 20
};
context.Products.Update(product1);
await context.SaveChangesAsync();
*/
#endregion

#region Toplu Update Yapılırken Nelere Dkkat Edilmeli
/*
var products =await context.Products.ToListAsync();

foreach (var product in products)
{
    product.Name += "*";
}
await context.SaveChangesAsync();     
*/
#endregion

#region Veri silme
/*
Product product = await context.Products.FirstOrDefaultAsync(p => p.Id == 3);

context.Products.Remove(product);

await context.SaveChangesAsync();
*/
#endregion

#region Takip edilmeyen nesneler nasıl silinir?
/*
Product p = new()
{
    Id = 2
};
context.Products.Remove(p);
await context.SaveChangesAsync();
*/
#endregion

#region EntityState ile silme işlemi
/*
Product p = new()
{
    Id = 1
};
context.Entry(p).State = EntityState.Deleted;
await context.SaveChangesAsync();
*/
#endregion

#region RemoveRange
/*
List<Product> products = await context.Products.Where(p => p.Id >= 5 && p.Id <= 10).ToListAsync();
context.Products.RemoveRange(products);
await context.SaveChangesAsync();
*/
#endregion
