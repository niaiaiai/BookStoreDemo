using MyEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class BookType : AggregateRoot<int>
    {
        [Required]
        public string TypeName { get; set; }
    }
}
