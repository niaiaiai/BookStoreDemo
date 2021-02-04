using MyEntity;
using MyEntity.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class BookPrice : CreateAndModifyAuditedAggregateRoot<Guid, string, string>
    {
        protected BookPrice() { }

        [Required]
        public Guid BookId { get; set; }

        public decimal Price { get; set; }

        public string Remark { get; set; }

        public BookPrice(Guid bookId, decimal price, string remark)
        {
            BookId = bookId;
            Price = price;
            Remark = remark;
        }
    }
}
