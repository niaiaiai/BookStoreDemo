using Domain.Entities;
using Domain.Specifications;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InfrastructureTest.Specifications
{
    public class PriceSpecificationsTest
    {
        [Theory]
        [InlineData("c", null)]
        [InlineData("  ", "")]
        [InlineData(null,"9784253177719")]
        [InlineData("人性的弱点", "9784253177719")]
        [InlineData("人性的弱点", "9787505738966")]
        [InlineData(null, null)]
        public async Task PriceSpecification_IsSatisfied_WithAll(string bookTitle, string isbn)
        {
            List<Guid> bookIds = DataSeed.Books.Where(b => 
                (string.IsNullOrWhiteSpace(bookTitle) || b.Title.Contains(bookTitle))
                  && (string.IsNullOrWhiteSpace(isbn) || b.ISBN == isbn))
                .Select(b => b.Id).ToList();

            var satisfiedList = from bookPrice in DataSeed.BookPrices
                                where bookIds.Contains(bookPrice.BookId)
                                select bookPrice;

            foreach (BookPrice price in satisfiedList)
            {
                bool isSatisfied = new PriceSpecification(bookIds).IsSatisfiedBy(price);
                Assert.True(isSatisfied);
            }
        }
    }
}
