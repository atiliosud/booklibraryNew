using System;
using BookLibrary.Commands;
using BookLibrary.Entities;
using BookLibrary.Repositories;
using MediatR;

namespace BookLibrary.EventHandlers
{
    public class CreateBookHandler : IRequestHandler<CreateBook, Book>
    {
        private readonly IBookRepository<Book> repositoryBook;

        public CreateBookHandler(IBookRepository<Book> repositoryBook)
        {
            this.repositoryBook = repositoryBook;
        }

        public async Task<Book> Handle(CreateBook request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Author = request.Author,
                TotalCopies = request.TotalCopies,
                CopiesInUse = request.CopiesInUse,
                Type = request.Type,
                Category = request.Category,
            };
            await repositoryBook.AddAsync(book);
            return book;
        }
    }
}
