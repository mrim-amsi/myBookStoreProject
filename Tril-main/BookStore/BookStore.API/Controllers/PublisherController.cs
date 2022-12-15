using AutoMapper;
using BookStore.API.DTOs;
using BookStore.API.Interface;
using BookStore.API.Interfaces;
using BookStore.API.Models;
using BookStore.API.Repositories;
using BookStore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/publishers")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;

        public IImageUploaderService UploaderService;
        private readonly IMapper mapper;

        public PublisherController(IPublisherRepository publisherRepository,
            IImageUploaderService uploaderService,
            IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            UploaderService = uploaderService;
            this.mapper = mapper;
        }
        //[HttpPost]
        //public async Task<IActionResult> Create([FromForm] CreatePublisherDto input)
        //{
        //    var publisher = await _publisherRepository.Create(input);
        //    return Ok(publisher);

        //}
        //[HttpPost("add")]
        //public async Task<IActionResult> Post([FromForm] CreatePublisherDto Publisherd)
        //{

        //    var bookResult = await _publisherRepository.Add(Publisherd);
        //    //return Ok(bookResult);
        //    //return CreatedAtRoute("GetBookById", new { id = bookResult.Id }, bookResult);
        //    return CreatedAtAction(nameof(GetAll), new { id = bookResult.Id }, bookResult);
        //}
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreatePublisherDto request)
        {
            var publisher = mapper.Map<Publisher>(request);

            publisher.Logo= await UploaderService.UploadImageAsync(request.Image);
            var bookResult = await _publisherRepository.Add(publisher);

            return Ok(request);
        }
        [HttpPut]
        public async Task<IActionResult> Update (UpdatePublisherDto input)
        {
            var publisher = await _publisherRepository.Update(input);
            return Ok(publisher);

        }
        [HttpGet]
        public async Task<IActionResult> GetAll(string? serachKey)
        {
            var publishers = await _publisherRepository.GetAll(serachKey);
            return Ok(publishers);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
         var result = await _publisherRepository.Delete(id);
            return Ok(result);
        }

    }
}
