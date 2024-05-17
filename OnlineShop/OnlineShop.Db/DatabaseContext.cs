using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class DatabaseContext : IdentityDbContext<User, Role, string>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Image> Images { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartItem>()
                        .HasOne(p => p.Cart)
                        .WithMany(t => t.Items)
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Image>()
                        .HasOne(p => p.Product)
                        .WithMany(p => p.Images)
                        .HasForeignKey(p => p.ProductId)
                        .OnDelete(DeleteBehavior.Cascade);

            var product1Id = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var product2Id = Guid.Parse("00000000-0000-0000-0000-000000000002");
            var product3Id = Guid.Parse("00000000-0000-0000-0000-000000000003");
            var product4Id = Guid.Parse("00000000-0000-0000-0000-000000000004");

            var image1 = new Image
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                Url = "/images/Products/SD father.jpg",
                ProductId = product1Id
            };

            var image2 = new Image
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000020"),
                Url = "/images/Products/SD angel.jpg",
                ProductId = product2Id
            };

            var image3 = new Image
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000030"),
                Url = "/images/Products/SD king.jpg",
                ProductId = product3Id
            };

            var image4 = new Image
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000040"),
                Url = "/images/Products/SD evil.jpg",
                ProductId = product4Id
            };

            modelBuilder.Entity<Image>().HasData(image1, image2, image3, image4);

            modelBuilder.Entity<Product>().HasData(new List<Product>()
            {
                new Product(
                    product1Id,
                    "Убить отца", 
                    "Сандроне Дациери", 
                    400, 
                    "В парке в пригороде Рима исчез ребенок. " +
                    "Недалеко от места, где мальчика видели в последний раз, найдено тело его матери. " +
                    "Следователи подозревают главу семейства. " +
                    "Прибывшая на место преступления Коломба Каселли категорически не согласна с официальной версией. " +
                    "Однако ей приходится расследовать дело частным образом, ведь после трагических событий, связанных с гибелью людей в парижском ресторане, она готова подать в отставку..."
                    ),

                new Product(
                    product2Id,
                    "Убить ангела", 
                    "Сандроне Дациери", 
                    500, 
                    "На вокзал Термини прибывает скоростной поезд «Милан-Рим», пассажиры расходятся, платформа пустеет, но из вагона класса «люкс» не выходит никто. " +
                    "Агент полиции Коломба Каселли, знакомая читателю по роману «Убить Отца», обнаруживает в вагоне тела людей, явно скончавшихся от удушья. " +
                    "Напрашивается версия о террористическом акте, которую готово подхватить руководство полиции. " +
                    "Однако Коломба подозревает, что дело вовсе не связано с террористами..."
                    ),

                new Product(
                    product3Id,
                    "Убить короля", 
                    "Сандроне Дациери", 
                    600, 
                    "После ужасной снежной бури Коломба Каселли, знакомая читателям по романам «Убить Отца» и «Убить Ангела», обнаруживает в своем сарае подростка-аутиста по имени Томми, перемазанного кровью. " +
                    "Выясняется, что его родители убиты у него на глазах. " +
                    "И хотя она еще полтора года назад вышла в отставку, пытаясь оправиться после трагических событий в Венеции, когда она едва не погибла, а ее друг Данте Торре был похищен, молодая женщина вынуждена включиться в расследование..." 
                    ),

                new Product(
                    product4Id,
                    "Зло, которе творят люди", 
                    "Сандроне Дациери", 
                    700, 
                    "Давным-давно Итала Карузо, полицейская «королева», управляющая разветвленной коррупционной сетью, была вынуждена посадить человека, которого ложно обвинили в том, что он похитил и убил трех девушек-подростков. " +
                    "Спустя тридцать лет случается новое похищение — и жертва, шестнадцатилетняя Амала, понимает, что вряд ли выберется живой, если не найдет способа сбежать самостоятельно. " +
                    "Амалу, впрочем, ищут. Ее ищет Франческа Кавальканте, сестра ее матери, адвокат..." 
                    )
            });
        }
    }
}