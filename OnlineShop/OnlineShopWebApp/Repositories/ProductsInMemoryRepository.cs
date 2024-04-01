﻿using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public class ProductsInMemoryRepository : IProductsRepository
    {
        private readonly List<Product> products = [

            new Product("Убить отца", "Сандроне Дациери", 400, "В парке в пригороде Рима исчез ребенок. Недалеко от места, где мальчика видели в последний раз, найдено тело его матери. Следователи подозревают главу семейства. Прибывшая на место преступления Коломба Каселли категорически не согласна с официальной версией. Однако ей приходится расследовать дело частным образом, ведь после трагических событий, связанных с гибелью людей в парижском ресторане, она готова подать в отставку...", "/images/SD father.jpg"),

            new Product("Убить ангела", "Сандроне Дациери", 500, "На вокзал Термини прибывает скоростной поезд «Милан-Рим», пассажиры расходятся, платформа пустеет, но из вагона класса «люкс» не выходит никто. Агент полиции Коломба Каселли, знакомая читателю по роману «Убить Отца», обнаруживает в вагоне тела людей, явно скончавшихся от удушья. Напрашивается версия о террористическом акте, которую готово подхватить руководство полиции. Однако Коломба подозревает, что дело вовсе не связано с террористами...", "/images/SD angel.jpg"),

            new Product("Убить короля", "Сандроне Дациери", 600, "После ужасной снежной бури Коломба Каселли, знакомая читателям по романам «Убить Отца» и «Убить Ангела», обнаруживает в своем сарае подростка-аутиста по имени Томми, перемазанного кровью. Выясняется, что его родители убиты у него на глазах. И хотя она еще полтора года назад вышла в отставку, пытаясь оправиться после трагических событий в Венеции, когда она едва не погибла, а ее друг Данте Торре был похищен, молодая женщина вынуждена включиться в расследование...", "/images/SD king.jpg"),

            new Product("Зло, которе творят люди", "Сандроне Дациери", 700, "Давным-давно Итала Карузо, полицейская «королева», управляющая разветвленной коррупционной сетью, была вынуждена посадить человека, которого ложно обвинили в том, что он похитил и убил трех девушек-подростков. Спустя тридцать лет случается новое похищение — и жертва, шестнадцатилетняя Амала, понимает, что вряд ли выберется живой, если не найдет способа сбежать самостоятельно. Амалу, впрочем, ищут. Ее ищет Франческа Кавальканте, сестра ее матери, адвокат...", "/images/SD evil.jpg"),
            ];        

        public IEnumerable<Product> GetAll() => products;

        public Product TryGetById(Guid id) => products.SingleOrDefault(p => p.Id == id);

        public void Add(Product product)
        {
            products.Add(product);
        }

        public void Edit(Product product)
        {
            products.Add(product);
        }

        public void Delete(Product product)
        {
            products.Remove(product);
        }
    }
}
