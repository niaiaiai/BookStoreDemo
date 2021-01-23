using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Volo.Abp.Specifications;

namespace Domain.Specifications
{
    public class PriceSpecification : Specification<BookPrice>
    {
        private IEnumerable<Guid> _bookIds;

        public PriceSpecification(IEnumerable<Guid> bookIds = null)
        {
            _bookIds = bookIds;
        }

        public override Expression<Func<BookPrice, bool>> ToExpression()
        {
            return bp => _bookIds.Contains(bp.BookId);
        }
    }
}
