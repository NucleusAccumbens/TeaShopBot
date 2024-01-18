using Domain.Entities;
using Domain.Enums;

namespace Application.Herbs.Interfaces;

public interface IGetHerbQuery
{
    Task<List<Herb>> GetAltaiHerbsAsync();

    Task<List<Herb>> GetKareliaHerbsAsync();

    Task<List<Herb>> GetCaucasusHerbsAsync();

    Task<List<Herb>> GetSiberiaHerbsAsync();

    Task<Herb?> GetHerbAsync(long id);

    Task<int> GetHerbCountAsync(HerbRegion herbRegion);

    Task<int> GetHerbCountAsync();
}
