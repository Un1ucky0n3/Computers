using Computers.DTOs;

namespace Computers.Services;

public interface IDbService
{
    public Task<List<GetPCDetailsDto>> GetPCsAsync();
    public Task<GetPCComponentsDto> GetComponentsAsync(int id);
    public Task<GetPCDetailsDto> CreatePCAsync(CreatePCDetailsDto dto);
    public Task UpdatePCAsync(int id, UpdatePCDto dto);
    public Task DeletePCAsync(int id);
}