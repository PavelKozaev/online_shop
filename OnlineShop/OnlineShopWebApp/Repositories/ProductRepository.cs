﻿using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> products = [

            new Product("Эпическое путешествие к центру галактики Млечный Путь", 1000, "Путешествие к центру галактики Млечный путь может - это возможность расширить границы нашего осознания и обрести новые глубокие знания о природе сущего. " +
                                                                                          "Ведь исследование галактики, в которой мы существуем, представляет собой уникальное отражение нашей внутренней духовной странности и устремленности. " +
                                                                                          "Ведь именно в глубинах галактик мы можем найти ответы на вечные вопросы о прошлом, настоящем и будущем. " +
                                                                                          "Это путешествие стимулирует наш ум и воображение, вызывает в нас настоящую философскую тревогу."),

            new Product("Путешествие в созвездие Центавра.", 500, "Путешествие к созвездию Центавра - это погружение в неизведанные глубины космоса и духовного самосознания. " +
                                                                          "Это путешествие вызывает в нас вопросы о нашем месте во Вселенной, о смысле жизни и о нашем взаимодействии с космосом и другими формами бытия. " +
                                                                          "Смотря в сторону Центавра, мы обращаемся к истокам философии и основополагающим вопросам о происхождении и сущности. " +
                                                                          "Это место, где временно и пространство переплетаются, где мы можем раскрыться в полной мере и осознать нашу связь с миром."),

            new Product("Путешествие в Андромеду", 3000, "Путешествие к Андромеде - это погружение в мистические глубины космоса и возможность расширить границы нашего понимания и воображения. " +
                                                         "Андромеда — это созвездие, находящееся на расстоянии двух миллионов световых лет от нас, и является одним из ближайших к нам крупных галактических соседей. " +
                                                         "Это путешествие предлагает нам уникальную возможность размышлять о вечных вопросах о сущности мироздания и нашем месте в нём. Проникая в глубины космического пространства, мы можем обрести новые знания о природе Вселенной и углубить понимание собственной роли в этом величественном театре.")

            ];

        public IEnumerable<Product> GetAll() => products;

        public Product TryGetById(Guid id) => products.SingleOrDefault(p => p.Id == id); 
    }
}
