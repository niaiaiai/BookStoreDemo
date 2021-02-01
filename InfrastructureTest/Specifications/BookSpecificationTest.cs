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
    public class BookSpecificationTest
    {
        [Theory]
        [InlineData("c")]
        [InlineData("海贼王")]
        [InlineData("++")]
        public async Task BookSpecification_IsSatisfied_WithTitle(string title)
        {
            var satisfiedList = DataSeed.Books.Where(b => b.Title.Contains(title));

            foreach (Book book in satisfiedList)
            {
                bool isSatisfied = new BookSpecification(title).IsSatisfiedBy(book);
                Assert.True(isSatisfied);
            }
        }

        [Theory]
        [InlineData("c", "", " ", " ", 1)]
        [InlineData("海贼王", null, " ", "9787534032950", -1)]
        [InlineData(" ", null, " ", " ", 3)]
        [InlineData(" ", null, " ", " ", 1)]
        public async Task BookSpecification_IsSatisfied_WithAll(string title, string author, string publisher, string isbn, int typeId)
        {
            var satisfiedList = DataSeed.Books.AsQueryable().Where(new BookSpecification(title, author, publisher, isbn, typeId));

            foreach (Book book in satisfiedList)
            {
                if (!string.IsNullOrWhiteSpace(title))
                {
                    Assert.Contains(title, book.Title);
                }
                if (!string.IsNullOrWhiteSpace(author))
                {
                    Assert.Contains(author, book.Author);
                }
                if (!string.IsNullOrWhiteSpace(publisher))
                {
                    Assert.Contains(publisher, book.Publisher);
                }
                if (!string.IsNullOrWhiteSpace(isbn))
                {
                    Assert.Equal(isbn, book.ISBN);
                }
                if (typeId != -1)
                {
                    Assert.Equal(typeId, book.BookTypeId);
                }
            }
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task BookSpecification_IsSatisfied_WithAuthor(int type)
        {
            var satisfiedList = DataSeed.Books.Where(b => b.BookTypeId == (type));

            foreach (Book book in satisfiedList)
            {
                Assert.Equal(type, book.BookTypeId);
            }
        }

        [Theory]
        [InlineData("垣野内成美")]
        [InlineData("垣野")]
        public async Task BookSpecification_IsSatisfied_WithType(string author)
        {
            var satisfiedList = DataSeed.Books.Where(b => b.Author.Contains(author));

            foreach (Book book in satisfiedList)
            {
                Assert.Contains(author, book.Author);
            }
        }

        [Fact]
        public async Task BookSpecification_IsSatisfied_WithNothing()
        {
            foreach (Book book in DataSeed.Books)
            {
                bool isSatisfied = new BookSpecification().IsSatisfiedBy(book);
                Assert.True(isSatisfied);
            }
        }

        [Fact]
        public async Task BookSpecification_IsSatisfied_WithNull()
        {
            foreach (Book book in DataSeed.Books)
            {
                bool isSatisfied = new BookSpecification(null, "", "", null, -1).IsSatisfiedBy(book);
                Assert.True(isSatisfied);
            }
        }
    }
}
