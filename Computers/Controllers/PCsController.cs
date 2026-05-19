using Computers.Data;
using Computers.DTOs;
using Computers.Exceptions;
using Computers.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Computers.Controllers;

[Route("[controller]")]
[ApiController]
public class PCsController : ControllerBase
{
    private readonly IDbService _dbService;
    public PCsController(IDbService dbService)
    {
        this._dbService = dbService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_dbService.GetPCsAsync());
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}/components")]
    public IActionResult GetComponents(int id)
    {
        try
        {
            return Ok(_dbService.GetComponentsAsync(id));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public IActionResult AddPc([FromBody] CreatePCDetailsDto dto)
    {
        try
        {
            return Created("api/pcs", _dbService.CreatePCAsync(dto));
        }catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePC(int id, UpdatePCDto dto)
    {
        await _dbService.UpdatePCAsync(id, dto);

        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePC(int id)
    {
        await _dbService.DeletePCAsync(id);

        return NoContent();
    }


}