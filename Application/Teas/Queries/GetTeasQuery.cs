using Application.Common.Interfaces;
using Application.Teas.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Teas.Queries;

public class GetTeasQuery : IGetTeaQuery
{
    private readonly ITeaShopBotDbContext _context;

    public GetTeasQuery(ITeaShopBotDbContext context)
    {
        _context = context;
    }

    public async Task<List<Tea>> GetAllCraftTeasAsync()
    {
        return await _context.Teas
            .Where(t => t.TeaType == TeaType.CraftTeas)
            .ToListAsync();
    }

    public async Task<List<Tea>> GetAllGreenTeaAsync()
    {
        return await _context.Teas
            .Where(t => t.TeaType == TeaType.Green)
            .ToListAsync();
    }

    public async Task<List<Tea>> GetAllOloongTeaAsync()
    {
        return await _context.Teas
            .Where(t => t.TeaType == TeaType.Oolong)
            .ToListAsync();
    }

    public async Task<List<Tea>> GetAllRedTeaAsync()
    {
        return await _context.Teas
            .Where(t => t.TeaType == TeaType.Red)
            .ToListAsync();
    }

    public async Task<List<Tea>> GetAllShenPuerTeaAsync()
    {
        return await _context.Teas
            .Where(t => t.TeaType == TeaType.ShenPuer)
            .ToListAsync();
    }

    public async Task<List<Tea>> GetAllShuPuerTeaAsync()
    {
        return await _context.Teas
            .Where(t => t.TeaType == TeaType.ShuPuer)
            .ToListAsync();
    }

    public async Task<List<Tea>> GetAllWhiteTeaAsync()
    {
        return await _context.Teas
            .Where(t => t.TeaType == TeaType.White)
            .ToListAsync();
    }

    public async Task<Tea?> GetTeaAsync(long id)
    {
        return await _context.Teas
            .SingleOrDefaultAsync(t => t.Id == id);
    }

    public async Task<int> GetTeaCountAsync(TeaType teaType)
    {
        var teas = await _context.Teas
            .Where(t => t.TeaType == teaType)
            .ToListAsync();

        return teas.Count;
    }

    public async Task<int> GetTeaCountAsync()
    {
        var teas = await _context.Teas
            .ToListAsync();

        return teas.Count;
    }
}
