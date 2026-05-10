using cwiczenia5.Data;
using cwiczenia5.DTOs;
using cwiczenia5.Entities;
using cwiczenia5.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace cwiczenia5.Services;

public class PcService : IPcService
{
    private readonly AppDbContext _dbContext;

    public PcService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GetPcDto>> GetAllPcs()
    {
        var pcs = await _dbContext.Pcs.Select(e => new GetPcDto
        {
            Id = e.Id,
            Name = e.Name,
            Stock = e.Stock,
            Warranty = e.Warranty,
            CreatedAt = e.CreatedAt,
            Weight = e.Weight,
        }).ToListAsync();

        return pcs;
    }

    public async Task<GetPcWithComponentsDto> GetPcComponents(int pcId)
    {
        var pc = await _dbContext.Pcs
            .Where(p => p.Id == pcId)
            .Select(p => new GetPcWithComponentsDto
            {
                Id = p.Id,
                Name = p.Name,
                Weight = p.Weight,
                Warranty = p.Warranty,
                CreatedAt = p.CreatedAt,
                Stock = p.Stock,
                Components = p.PcComponents.Select(pcc => new GetPcComponentDto
                {
                    Amount = pcc.Amount,
                    Component = new GetComponentDto
                    {
                        Code = pcc.Component.Code,
                        Name = pcc.Component.Name,
                        Description = pcc.Component.Description,
                        Manufacturer = new GetManufacturerDto
                        {
                            Id = pcc.Component.ComponentManufacturer.Id,
                            Abbreviation = pcc.Component.ComponentManufacturer.Abbreviation,
                            FullName = pcc.Component.ComponentManufacturer.FullName,
                            FoundationDate = pcc.Component.ComponentManufacturer.FoundationDate,
                        },
                        Type = new GetTypeDto
                        {
                            Id = pcc.Component.ComponentType.Id,
                            Abbreviation = pcc.Component.ComponentType.Abbreviation,
                            Name = pcc.Component.ComponentType.Name,
                        },
                    },
                }).ToList(),
            })
            .FirstOrDefaultAsync();

        if (pc is null) throw new NotFoundException();

        return pc;
    }

    public async Task<GetPcDto> AddPc(NewPcDto body)
    {
        var pc = new Pc
        {
            Name = body.Name,
            Stock = body.Stock,
            Warranty = body.Warranty,
            CreatedAt = body.CreatedAt,
            Weight = body.Weight,
        };
        
        _dbContext.Pcs.Add(pc);
        await _dbContext.SaveChangesAsync();

        return new GetPcDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock,
            Weight = pc.Weight,
        };
    }

    public async Task<GetPcDto> UpdatePc(int id, NewPcDto body)
    {
        var pc = await _dbContext.Pcs.FindAsync(id);
        if (pc is null) throw new NotFoundException();
        
        pc.Name = body.Name;
        pc.Warranty = body.Warranty;
        pc.CreatedAt = body.CreatedAt;
        pc.Stock = body.Stock;
        pc.Weight = body.Weight;
        
        await _dbContext.SaveChangesAsync();

        return new GetPcDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock,
            Weight = pc.Weight,
        };
    }
    
    public async Task DeletePc(int id)
    {
        var pc = await _dbContext.Pcs.FindAsync(id);
        if (pc is null) throw new NotFoundException();
        
        _dbContext.Pcs.Remove(pc);
        await _dbContext.SaveChangesAsync();
    }
}
