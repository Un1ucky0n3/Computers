using Computers.Data;
using Computers.DTOs;
using Computers.Entities;
using Computers.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Computers.Services;

public class DbService : IDbService
{
    private readonly AppDbContext _context;

    public DbService(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<List<GetPCDetailsDto>> GetPCsAsync()
    {
        var pcs = await _context.PCs
            .Select(pc => new GetPCDetailsDto
            {
                id = pc.Id,
                name = pc.Name,
                weight = pc.Weight,
                warranty = pc.Warranty,
                createdAt = pc.CreatedAt,
                stock = pc.Stock
            })
            .ToListAsync();

        if (!pcs.Any())
            throw new NotFoundException("No PCs found");

        return pcs;
    }

    public async Task<GetPCComponentsDto> GetComponentsAsync(int id)
    {
        var pc = await _context.PCs
            .Include(p => p.PCComponents)
                .ThenInclude(pc => pc.Component)
                    .ThenInclude(c => c.ComponentManufacturer)
            .Include(p => p.PCComponents)
                .ThenInclude(pc => pc.Component)
                    .ThenInclude(c => c.ComponentType)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pc == null)
            throw new NotFoundException("PC not found with id: " + id);

        var res = new GetPCComponentsDto
        {
            id = pc.Id,
            name = pc.Name,
            weight = pc.Weight,
            warranty = pc.Warranty,
            createdAt = pc.CreatedAt,
            stock = pc.Stock,

            components = pc.PCComponents.Select(pcComponent => new ComponentAmountDto
            {
                amount = pcComponent.Amount,

                component = new ComponentDetailsDto
                {
                    code = pcComponent.Component.Code,
                    name = pcComponent.Component.Name,
                    description = pcComponent.Component.Description,

                    manufacturer = new ComponentManufacturerDetailsDto
                    {
                        id = pcComponent.Component.ComponentManufacturer.Id,
                        abbreviation = pcComponent.Component.ComponentManufacturer.Abbreviation,
                        fullName = pcComponent.Component.ComponentManufacturer.FullName,
                        foundationDate = pcComponent.Component.ComponentManufacturer.FoundationDate
                    },

                    type = new ComponentTypeDetailsDto
                    {
                        id = pcComponent.Component.ComponentType.Id,
                        abbreviation = pcComponent.Component.ComponentType.Abbreviation,
                        name = pcComponent.Component.ComponentType.Name
                    }
                }

            }).ToList()
        };

        return res;
    }

    public async Task<GetPCDetailsDto> CreatePCAsync(CreatePCDetailsDto dto)
    {
        var pc = new PC
        {
            Name = dto.name,
            Weight = dto.weight,
            Warranty = dto.warranty,
            CreatedAt = DateTime.UtcNow,
            Stock = dto.stock
        };
        _context.PCs.Add(pc);
        await _context.SaveChangesAsync();
        return new GetPCDetailsDto
        {
            id = pc.Id,
            name = pc.Name,
            weight = pc.Weight,
            warranty = pc.Warranty,
            createdAt = pc.CreatedAt,
            stock = pc.Stock
        };
    }
    
    public async Task UpdatePCAsync(int id, UpdatePCDto dto)
    {
        var pc = await _context.PCs.FindAsync(id);

        if (pc == null)
            throw new NotFoundException($"PC with id {id} not found");

        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;

        await _context.SaveChangesAsync();
    }
    public async Task DeletePCAsync(int id)
    {
        var pc = await _context.PCs.FindAsync(id);

        if (pc == null)
            throw new NotFoundException($"PC with id {id} not found");

        _context.PCs.Remove(pc);

        await _context.SaveChangesAsync();
    }
}