using Domain.Entities;
using Domain.Enums;

namespace Application.Teas.Interfaces;

public interface IGetTeaQuery
{
    Task<List<Tea>> GetAllRedTeaAsync();

    Task<List<Tea>> GetAllGreenTeaAsync();

    Task<List<Tea>> GetAllWhiteTeaAsync();

    Task<List<Tea>> GetAllOloongTeaAsync();

    Task<List<Tea>> GetAllShuPuerTeaAsync();

    Task<List<Tea>> GetAllShenPuerTeaAsync();

    Task<List<Tea>> GetAllCraftTeasAsync();

    Task<Tea?> GetTeaAsync(long id);

    Task<int> GetTeaCountAsync(TeaType teaType);

    Task<int> GetTeaCountAsync();
}
