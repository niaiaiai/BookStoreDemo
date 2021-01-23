
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Volo.Abp.Specifications;

namespace Domain.Specifications
{
    public class BookSpecification : Specification<Book>
    {
        private string _title;
        private string _author;
        private string _publisher;
        private string _isbn;
        private int _typeId;

        public BookSpecification(string title = null, string author = null, string publisher = null, string isbn = null, int typeId = -1)
        {
            _title = title;
            _author = author;
            _publisher = publisher;
            _isbn = isbn;
            _typeId = typeId;
        }


        public override Expression<Func<Book, bool>> ToExpression()
        {
            return book =>
                (string.IsNullOrWhiteSpace(_title) || book.Title.Contains(_title)) &&
                (string.IsNullOrWhiteSpace(_author) || book.Auther.Contains(_author)) &&
                (string.IsNullOrWhiteSpace(_publisher) || book.Publisher.Contains(_publisher)) &&
                (string.IsNullOrWhiteSpace(_isbn) || book.ISBN == _isbn) &&
                (_typeId == -1 || book.BookTypeId == _typeId);
        }
    }
}
