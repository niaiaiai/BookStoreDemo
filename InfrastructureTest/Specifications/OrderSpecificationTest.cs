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
    public class OrderSpecificationTest
    {
        [Theory]
        [InlineData("2020-12-27", "2020-12-28")]
        [InlineData("2020-12-27", "2020-12-27 20:00:00")]
        [InlineData("2020-12-27 23:59:59", "2021-2-3 23:59:59")]
        [InlineData("2020-12-21", "2021-1-1")]
        public async Task OrderSpecification_Should_SatisfiedDate(DateTime startTime, DateTime endTime)
        {
            var satisfiedList = DataSeed.Orders.Where(o => o.OrderDate >= startTime && o.OrderDate <= endTime);
            foreach (Order order in satisfiedList)
            {
                bool isSatisfied = new OrderSpecification(null, null, null, null, null, startTime, endTime).IsSatisfiedBy(order);
                Assert.True(isSatisfied);
            }
        }

        [Theory]
        [InlineData("1234567")]
        [InlineData("12345666")]
        [InlineData("  ")]
        [InlineData(null)]
        public async Task OrderSpecification_Should_SatisfiedOrderNo(string orderNo)
        {
            Order order = DataSeed.Orders.FirstOrDefault(o => o.OrderNo == orderNo);
            bool isSatisfied = new OrderSpecification(orderNo).IsSatisfiedBy(order);
            Assert.True(isSatisfied);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(true)]
        [InlineData(false)]
        public async Task OrderSpecification_Should_SatisfiedIsClose(bool? isClose)
        {
            var satisfiedList = DataSeed.Orders.Where(o => o.IsClose == isClose);
            foreach (Order order in satisfiedList)
            {
                bool isSatisfied = new OrderSpecification(null, isClose).IsSatisfiedBy(order);
                Assert.True(isSatisfied);
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData(true)]
        [InlineData(false)]
        public async Task OrderSpecification_Should_SatisfiedIsDone(bool? isDone)
        {
            var satisfiedList = DataSeed.Orders.Where(o => o.IsDone == isDone);
            foreach (Order order in satisfiedList)
            {
                bool isSatisfied = new OrderSpecification(null, null, null, isDone).IsSatisfiedBy(order);
                Assert.True(isSatisfied);
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData(true)]
        [InlineData(false)]
        public async Task OrderSpecification_Should_SatisfiedIsDeliver(bool? isDeliver)
        {
            var satisfiedList = DataSeed.Orders.Where(o => o.IsDeliver == isDeliver);
            foreach (Order order in satisfiedList)
            {
                bool isSatisfied = new OrderSpecification(null, null, isDeliver).IsSatisfiedBy(order);
                Assert.True(isSatisfied);
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData(true)]
        [InlineData(false)]
        public async Task OrderSpecification_Should_SatisfiedNeedInvoice(bool? needInvoice)
        {
            var satisfiedList = DataSeed.Orders.Where(o => o.NeedInvoice == needInvoice);
            foreach (Order order in satisfiedList)
            {
                bool isSatisfied = new OrderSpecification(null, null, null, null, needInvoice).IsSatisfiedBy(order);
                Assert.True(isSatisfied);
            }
        }
    }
}
