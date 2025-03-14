
public interface IHouseRepository {
    Task<List<HouseDto>> GetAll();
    Task<HouseDetailDto?> Get(int id);

    Task<HouseDetailDto> Update(HouseDetailDto dto);

    Task<HouseDetailDto> Add(HouseDetailDto dto);

    Task Delete(int id);
}
