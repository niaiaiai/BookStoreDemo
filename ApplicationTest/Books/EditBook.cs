//using Application.Dtos.Books;
//using Application.Services;
//using AutoMapper;
//using Domain.Entities;
//using Domain.Interfaces;
//using Domain.Services;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TestBase;
//using Xunit;

//namespace ApplicationTest.Books
//{
//    public class EditBook : UnitTestBase<Book, Guid>
//    {
//        private readonly Mock<IBookService> _bookServiceMock;
//        private BookViewService _bookViewService;

//        public EditBook()
//        {
//            _bookServiceMock = new();
//            _serviceProvider.Setup(s => s.GetService(typeof(IBookService))).Returns(_bookServiceMock.Object);
//            _bookViewService = new BookViewService(_serviceProvider.Object);
//        }

//        [Theory]
//        [MemberData(nameof(UpdateBook1))]
//        [MemberData(nameof(UpdateBook2))]
//        [MemberData(nameof(UpdateBook3))]
//        public async Task EditImage_Should_Success(UpdateBookDto dto)
//        {
//            _bookServiceMock.Setup(s => s.EditImage(dto.Id, dto.ImageBase64)).ReturnsAsync(Mock.Of<Book>());
//            await _bookViewService.EditImage(dto);
//        }

//        [Theory]
//        [MemberData(nameof(UpdateBook1))]
//        [MemberData(nameof(UpdateBook2))]
//        [MemberData(nameof(UpdateBook3))]
//        public async Task EditRemark_Should_Success(UpdateBookDto dto)
//        {
//            _bookServiceMock.Setup(s => s.EditRemark(dto.Id, dto.Remark)).ReturnsAsync(Mock.Of<Book>());
//            await _bookViewService.EditRemark(dto);
//        }

//        public static IEnumerable<object[]> UpdateBook1
//            => new List<object[]> {
//                    new object[] { new UpdateBookDto() { Id = Guid.Parse("0badcd1b-1dfe-4c5d-85ba-08d8a7273f08"),  ImageBase64 = "", Remark = "" } }
//            };
//        public static IEnumerable<object[]> UpdateBook2
//            => new List<object[]> {
//                    new object[] { new UpdateBookDto() { Id = Guid.Parse("0badcd1b-1dfe-4c5d-85ba-08d8a7273f08"),  ImageBase64 = null, Remark = "" } }
//            };
//        public static IEnumerable<object[]> UpdateBook3
//            => new List<object[]> {
//                    new object[] { new UpdateBookDto() { Id = Guid.Parse("0badcd1b-1dfe-4c5d-85ba-08d8a7273f08"),  ImageBase64 = "123", Remark = "123" } }
//            };
//    }
//}
