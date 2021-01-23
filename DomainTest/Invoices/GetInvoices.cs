using Domain.Entities;
using Domain.Services;
using Domain.Specifications;
using Infrastructure;
using Moq;
using MyServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestBase;
using Xunit;

namespace DomainTest.Invoices
{
    public class GetInvoices : UnitTestBase<Invoice, Guid>
    {
        private InvoiceService _invoiceService;

        public GetInvoices() : base()
        {
            _invoiceService = new InvoiceService(_serviceProvider.Object);
        }

        [Theory]
        [MemberData(nameof(GetInvoicesByInvoiceCodeNo))]
        [MemberData(nameof(GetInvoicesByOrderId))]
        [MemberData(nameof(GetInvoicesByDrawer))]
        [MemberData(nameof(GetInvoicesByIsRed))]
        [MemberData(nameof(GetInvoicesByInvoiceDate))]
        [MemberData(nameof(GetInvoicesByAll))]
        public async Task GetInvoices_Should_GetInvoices(InvoiceSpecification invoiceSpecification, int index, int pageSize, int count)
        {
            _repository.Setup(s => s.GetListAsync(It.IsAny<Expression<Func<Invoice, bool>>>(), It.IsAny<Func<IQueryable<Invoice>, IOrderedQueryable<Invoice>>>(), It.IsAny<Expression<Func<Invoice, object>>>()))
                .ReturnsAsync(DataSeed.Invoices.AsQueryable().Where(invoiceSpecification));
            IPageResult<Invoice> invoices = await _invoiceService.GetInvoices(invoiceSpecification, index, pageSize);

            Assert.NotNull(invoices);
            Assert.Equal(count, invoices.Total);
            int pageCount = count - pageSize * index;
            Assert.Equal(pageCount >= pageSize ? pageSize : (pageCount > 0 ? count : 0), invoices.Data.Count());
        }

        //public static IEnumerable<object[]> GetInvoicesByOrderNo
        //    => new List<object[]> {
        //        new object[] { new InvoiceSpecification("12345678"), 0, 10, 1 }
        //    };
        public static IEnumerable<object[]> GetInvoicesByInvoiceDate
            => new List<object[]> {
                new object[] { new InvoiceSpecification(null, null, null, null, new DateTime(2020, 12, 1)), 0, 2, 3 }
            };
        public static IEnumerable<object[]> GetInvoicesByInvoiceCodeNo
            => new List<object[]> {
                new object[] { new InvoiceSpecification("14400190112001040925"), 0, 10, 1 }
            };
        public static IEnumerable<object[]> GetInvoicesByOrderId
            => new List<object[]> {
                new object[] { new InvoiceSpecification(null, null, null, Guid.Parse("00683f67-c852-484a-b168-14cf39a08b89")), 0, 10, 2 }
            };
        public static IEnumerable<object[]> GetInvoicesByDrawer
            => new List<object[]> {
                new object[] { new InvoiceSpecification(null, "a"), 2, 2, 4 }
            };
        public static IEnumerable<object[]> GetInvoicesByIsRed
            => new List<object[]> {
                new object[] { new InvoiceSpecification(null, null, true), 0, 10, 2 }
            };
        public static IEnumerable<object[]> GetInvoicesByAll
            => new List<object[]> {
                new object[] { new InvoiceSpecification(), 0, 10, DataSeed.Invoices.Count }
            };
    }
}
