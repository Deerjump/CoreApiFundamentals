using AutoMapper;
using CoreCodeCamp.Data;
using CoreCodeCamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCodeCamp.Controllers
{
  [Route("api/[controller]")]
  public class CampsController : ControllerBase
  {
    private readonly ICampRepository _repository;
    private readonly IMapper _mapper;

    public CampsController(ICampRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<CampModel[]>> Get(bool includeTalks = true)
    {
      try
      {
        var results = await _repository.GetAllCampsAsync(includeTalks);


        return _mapper.Map<CampModel[]>(results); 
      }
      catch (Exception error)
      {
        Console.Error.Write(error);
        return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
      }
    }

    [HttpGet("{moniker}")]
    public async Task<ActionResult<CampModel>> GetCamp(string moniker)
    {
      var result = await _repository.GetCampAsync(moniker);

      if (result == null) return NotFound();

      var model = _mapper.Map<CampModel>(result);

      return model;
    }
  }
}
