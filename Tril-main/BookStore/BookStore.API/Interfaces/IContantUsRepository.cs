using BookStore.API.DTOs;
using BookStore.API.Models;
using BookStore.API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Interfaces
{
    public interface IContantUsRepository
    {
        Task<List<ContantUsViewModel>> GetAll();
        Task<ContantUs> Create(CreateContantUsDto dto);


         Task<UpdateContantUsDto> Update(UpdateContantUsDto dto);


         Task<bool> Delete(int id);
        
    }
}
