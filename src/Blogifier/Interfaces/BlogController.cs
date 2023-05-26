using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[ApiController]
[Authorize]
[Route("api/blog")]
public class BlogController : ControllerBase
{
  private readonly IMapper _mapper;
  private readonly BlogManager _blogManager;

  public BlogController(IMapper mapper, BlogManager blogManager)
  {
    _mapper = mapper;
    _blogManager = blogManager;
  }

  [HttpGet]
  public async Task<BlogEitorDto> GetAsync()
  {
    var data = await _blogManager.GetAsync();
    var dataDto = _mapper.Map<BlogEitorDto>(data);
    return dataDto;
  }

  [HttpPut]
  public async Task PutAsync([FromBody] BlogEitorDto blog)
  {
    var data = await _blogManager.GetAsync();
    data.Title = blog.Title;
    data.Description = blog.Description;
    data.HeaderScript = blog.HeaderScript;
    data.FooterScript = blog.FooterScript;
    data.IncludeFeatured = blog.IncludeFeatured;
    data.ItemsPerPage = blog.ItemsPerPage;
    await _blogManager.SetAsync(data);
  }
}
