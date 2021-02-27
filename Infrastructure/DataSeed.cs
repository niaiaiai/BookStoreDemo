
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DataSeed
    {
        public static List<Book> Books => new List<Book>
            {
                new Book("设计模式", "9787111618331", "", "机械工业出版社", "gof", "本", null) { Id = Guid.Parse("0badcd1b-1dfe-4c5d-85ba-08d8a7273f08"), BookTypeId = 1 },
                new Book("c++", "1234", "", "xxx出版社", "aaa", "本", null) { Id = Guid.Parse("68fb9bf4-58b6-49b6-85bb-08d8a7273f08"), BookTypeId = 1 },
                new Book("c#", "3456", "", "xxx出版社", "abc", "本", null) { Id = Guid.Parse("14024ae3-624f-4f3c-85bc-08d8a7273f08"), BookTypeId = 1 },
                new Book("海贼王", "9787534032950", "", "浙江人民美术出版社", "尾田荣一郎", "本", null) { Id = Guid.Parse("e3fac700-ed2a-4258-85bd-08d8a7273f08"), BookTypeId = 2 },
                new Book("吸血姬美夕", "9784253177719", "", "秋田书店", "垣野内成美", "本", null) { Id = Guid.Parse("4f3a9241-4147-4adc-85be-08d8a7273f08"), BookTypeId = 2 },
                new Book("除魔美少女", "9784253127219", "", "讲谈社", "垣野内成美", "本", null) { Id = Guid.Parse("7e85a8ed-4682-40a2-85bf-08d8a7273f08"), BookTypeId = 2 },
                new Book("人性的弱点", "9787505738966", "", "中国友谊出版公司", "戴尔*卡耐基", "本", null) { Id = Guid.Parse("98214cf7-fbf2-4621-85c0-08d8a7273f08"), BookTypeId = 3 },
            };
        public static List<BookPrice> BookPrices => new List<BookPrice>
        {
            new BookPrice(Guid.Parse("0badcd1b-1dfe-4c5d-85ba-08d8a7273f08"), 39.50000m, null) { Id = Guid.Parse("4bbae575-6bb5-4ef1-999c-6a1a8fb73134") },
            new BookPrice(Guid.Parse("0badcd1b-1dfe-4c5d-85ba-08d8a7273f08"), 0.00001m, "测试用") { Id = Guid.Parse("d83babcc-47cb-43c1-9755-294e3fb9d6ce") },
            new BookPrice(Guid.Parse("68fb9bf4-58b6-49b6-85bb-08d8a7273f08"), 45.00000m, null) { Id = Guid.Parse("2838d89d-c7d3-4441-8501-642438e3eeae") },
            new BookPrice(Guid.Parse("14024ae3-624f-4f3c-85bc-08d8a7273f08"), 40.00000m, null) { Id = Guid.Parse("6dfdaa60-218b-4a40-94b7-5648fc382a07") },
            new BookPrice(Guid.Parse("e3fac700-ed2a-4258-85bd-08d8a7273f08"), 110.43000m, null) { Id = Guid.Parse("a8fe1ff9-43f1-4fb1-98b5-323922e1b69e") },
            new BookPrice(Guid.Parse("e3fac700-ed2a-4258-85bd-08d8a7273f08"), 10.00000m, "测试用") { Id = Guid.Parse("995776e6-359f-45eb-b973-16a968826942") },
            new BookPrice(Guid.Parse("4f3a9241-4147-4adc-85be-08d8a7273f08"), 200.00000m, null) { Id = Guid.Parse("e372a721-734d-439e-9036-ea54ac94f2ec") },
            new BookPrice(Guid.Parse("7e85a8ed-4682-40a2-85bf-08d8a7273f08"), 80.50000m, null) { Id = Guid.Parse("6e0539a7-7359-460b-8217-fdc20fe57e4d") },
            new BookPrice(Guid.Parse("98214cf7-fbf2-4621-85c0-08d8a7273f08"), 59.90000m, null) { Id = Guid.Parse("19f3fd69-caf0-462b-9555-5e5978591894") },
        };

        public static List<Order> Orders => new List<Order>
        {
            new Order("1234567", customerId: Guid.Parse("9d13f46b-67a2-41f7-8bda-09a529fb3e91"), true, false, false, false, new DateTime(2020, 12, 27),
                new List<OrderItem> {
                    new OrderItem(orderId: Guid.Parse("e8284ed8-3e36-40fd-b57e-dfbbcf7e1bb8"), bookId: Guid.Parse("0badcd1b-1dfe-4c5d-85ba-08d8a7273f08"), 0.00001m, 1),
                    new OrderItem(orderId: Guid.Parse("e8284ed8-3e36-40fd-b57e-dfbbcf7e1bb8"), bookId: Guid.Parse("14024ae3-624f-4f3c-85bc-08d8a7273f08"), 40.00000m, 2),
                    new OrderItem(orderId: Guid.Parse("e8284ed8-3e36-40fd-b57e-dfbbcf7e1bb8"), bookId: Guid.Parse("e3fac700-ed2a-4258-85bd-08d8a7273f08"), 10.00000m, 1)
                }) { Id = Guid.Parse("e8284ed8-3e36-40fd-b57e-dfbbcf7e1bb8") },

            new Order("12345678", customerId: Guid.Parse("2a6d6c17-e0ed-4a45-9ed3-3ef840671f54"), false, true, true, false, new DateTime(2020, 12, 27, 23, 59, 59),
                new List<OrderItem> {
                    new OrderItem(orderId: Guid.Parse("6a7a6f4b-3df2-4018-b37a-4ae7b6272222"), bookId: Guid.Parse("0badcd1b-1dfe-4c5d-85ba-08d8a7273f08"), 0.00001m, 10)
                }) { Id = Guid.Parse("6a7a6f4b-3df2-4018-b37a-4ae7b6272222") },

            new Order("12345666", customerId: Guid.Parse("5cd42586-d1af-44ef-ab4f-b87c051ce3e8"), false, false, false, true, new DateTime(2021, 2, 3, 23, 59, 59),
                new List<OrderItem> {
                    new OrderItem(orderId: Guid.Parse("00683f67-c852-484a-b168-14cf39a08b89"), bookId: Guid.Parse("4f3a9241-4147-4adc-85be-08d8a7273f08"), 200.00000m, 1)
                }) { Id = Guid.Parse("00683f67-c852-484a-b168-14cf39a08b89") },

            new Order("12345665", customerId: Guid.Parse("00683f67-c852-484a-b168-14cf39a08b89"), true, true, false, true,  new DateTime(2020, 12, 28, 0, 4, 56),
                new List<OrderItem> {
                    new OrderItem(orderId: Guid.Parse("12443554-70ae-4560-815f-7ad77ae0a713"), bookId: Guid.Parse("98214cf7-fbf2-4621-85c0-08d8a7273f08"), 59.90000m, 1)
                }) { Id = Guid.Parse("12443554-70ae-4560-815f-7ad77ae0a713") }
        };

        public static List<Invoice> Invoices => new List<Invoice>
        {
            new Invoice(new DateTime(2020, 12, 30), "144001901110", "01040924", "abc", orderId: Guid.Parse("e8284ed8-3e36-40fd-b57e-dfbbcf7e1bb8"), customerId: Guid.Parse("9d13f46b-67a2-41f7-8bda-09a529fb3e91"), false, null, new List<InvoiceItem> {
                new InvoiceItem(Guid.Parse("436b2726-84e6-487c-92fa-eca3c147f784"), Guid.Parse("0badcd1b-1dfe-4c5d-85ba-08d8a7273f08"), 1, 0.00001m),
                new InvoiceItem(Guid.Parse("436b2726-84e6-487c-92fa-eca3c147f784"), Guid.Parse("14024ae3-624f-4f3c-85bc-08d8a7273f08"), 2, 40.00000m),
                new InvoiceItem(Guid.Parse("436b2726-84e6-487c-92fa-eca3c147f784"), Guid.Parse("e3fac700-ed2a-4258-85bd-08d8a7273f08"), 1, 10.00000m)
            }) { Id = Guid.Parse("436b2726-84e6-487c-92fa-eca3c147f784") },

            new Invoice(new DateTime(2020, 12, 28), "144001901120", "01040925", "abd", orderId: Guid.Parse("12443554-70ae-4560-815f-7ad77ae0a713"), customerId: Guid.Parse("00683f67-c852-484a-b168-14cf39a08b89"), false, null, new List<InvoiceItem> {
                new InvoiceItem(Guid.Parse("bc160868-5b57-44ea-a0fc-fdd6a85f18e1"), Guid.Parse("98214cf7-fbf2-4621-85c0-08d8a7273f08"), 1, 59.90000m)
            }) { Id = Guid.Parse("bc160868-5b57-44ea-a0fc-fdd6a85f18e1") },

            new Invoice(new DateTime(2020, 11, 28), "144001901120", "01040926", "abd", orderId: Guid.Parse("00683f67-c852-484a-b168-14cf39a08b89"), customerId: Guid.Parse("5cd42586-d1af-44ef-ab4f-b87c051ce3e8"), true, null, new List<InvoiceItem> {
                new InvoiceItem(Guid.Parse("d5a53f09-eed8-4e19-998a-61e49bf359cb"), Guid.Parse("98214cf7-fbf2-4621-85c0-08d8a7273f08"), 1, -10.90000m)
            }) { Id = Guid.Parse("d5a53f09-eed8-4e19-998a-61e49bf359cb") },

            new Invoice(new DateTime(2020, 12, 31), "144001901121", "01040926", "abdc", orderId: Guid.Parse("00683f67-c852-484a-b168-14cf39a08b89"), customerId: Guid.Parse("5cd42586-d1af-44ef-ab4f-b87c051ce3e8"), true, null, new List<InvoiceItem> {
                new InvoiceItem(Guid.Parse("e3bde9da-28cb-4777-b779-33ccc178422e"), Guid.Parse("98214cf7-fbf2-4621-85c0-08d8a7273f08"), 2, -10.90000m),
                new InvoiceItem(Guid.Parse("e3bde9da-28cb-4777-b779-33ccc178422e"), Guid.Parse("4f3a9241-4147-4adc-85be-08d8a7273f08"), 1, -10.90000m)
            }) { Id = Guid.Parse("e3bde9da-28cb-4777-b779-33ccc178422e") }
        };
    }
}
