
using MyEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Book : AggregateRoot<Guid>
    {
        protected Book() { }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        public string Image { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string Auther { get; set; }

        public int BookTypeId { get; set; }

        [Required]
        public string Unit { get; set; }

        public string Remark { get; set; }

        public BookType BookType { get; set; }

        public Book(string title, string isbn, string image, string publisher, string auther, string unit, string remark)
        {
            Title = title;
            ISBN = isbn;
            Image = image;
            Publisher = publisher;
            Auther = auther;
            Unit = unit;
            Remark = remark;
        }
    }
}
