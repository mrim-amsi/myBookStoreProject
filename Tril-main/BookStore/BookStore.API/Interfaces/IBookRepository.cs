using BookStore.API.DTOs;
using BookStore.API.Models;
using BookStore.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Interface
{
    public interface IBookRepository
    {
        Task<BookViewModel> GetById(int id);
        Task<List<BookViewModel>> GetAll();
        Task<CreateBookDto> Create([FromForm] CreateBookDto dto);
        Task<Book> Add(CreateBookDto book);

        Task<UpdateBookDto> Update(UpdateBookDto dto);
        Task<bool> Delete(int id);
        Task<List<BookViewModel>> GetAllByCategory(int categoryId);
    }
}
