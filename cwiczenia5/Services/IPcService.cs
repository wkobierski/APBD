using cwiczenia5.DTOs;

namespace cwiczenia5.Services;

public interface IPcService
{
    public Task<IEnumerable<GetPcDto>> GetAllPcs();
    public Task<GetPcWithComponentsDto> GetPcComponents(int pcId);
    public Task<GetPcDto> AddPc(NewPcDto newPcDto);
    public Task<GetPcDto> UpdatePc(int id, NewPcDto newPcDto);
    public Task DeletePc(int id);
}
