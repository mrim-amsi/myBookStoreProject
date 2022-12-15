﻿using AutoMapper;
using BookStore.API.Data;
using BookStore.API.DTOs;
using BookStore.API.Interface;
using BookStore.API.Models;
using BookStore.API.Services;
using BookStore.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _hostEnvironment;



        public BookRepository(BookStoreDbContext context, IMapper mapper, IFileService fileService,
            IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<BookViewModel> GetById(int id)
        {
            var book = await _context.Books.FindAsync(id);

            var bookVM = _mapper.Map<BookViewModel>(book);
            return bookVM;
        }

        public async Task<List<BookViewModel>> GetAll()
        {

            var book = await _context.Books.ToListAsync();
            var bookVM = _mapper.Map<List<BookViewModel>>(book);
            return bookVM;
        }
        public async Task<List<BookViewModel>> GetAllByCategory(int categoryId)
        {
            var book = await _context.Books
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync();
            var bookVM = _mapper.Map<List<BookViewModel>>(book);
            return bookVM;
        }
        public async Task<CreateBookDto> Create([FromForm]CreateBookDto dto)
        {
            var book = _mapper.Map<Book>(dto);
            if (dto.Image != null)
            {
                book.Image = await _fileService.SaveFile(dto.Image, "Images");
            }
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return dto;
        }



        public async Task<Book> Add(CreateBookDto book)
        {
            var todoDemo = _mapper.Map<Book>(book);
            var uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images");
            var uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(book.Image.FileName);
            var filePath = Path.Combine(uploadFolder, uniqueName);
            book.Image.CopyTo(new FileStream(filePath, FileMode.Create));
            todoDemo.Image = uniqueName;
            await _context.Books.AddAsync(todoDemo);
            await _context.SaveChangesAsync();
            return todoDemo;
        }

        public async Task<UpdateBookDto> Update(UpdateBookDto dto)
        {
            var book = await _context.Books.SingleOrDefaultAsync(x => x.Id == dto.Id);

            if (book == null)
                throw new ArgumentException("Item Not Found");
            else
            {

                var updatedBook = _mapper.Map(dto, book);
                if (dto.Image != null)
                {
                    book.Image = await _fileService.SaveFile(dto.Image, "Images");
                }
                var updatedBookVM = _mapper.Map<Book, UpdateBookDto>(book, dto);
                _context.Books.Update(updatedBook);
                await _context.SaveChangesAsync();
                return updatedBookVM;

            }

        }

        public async Task<bool> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return false;
            else
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return true;
            }

        }
    }
}
