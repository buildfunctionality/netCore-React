using Microsoft.EntityFrameworkCore;
using SQLitePCL;

public interface IHouseRepository {
    Task<List<HouseDto>> GetAll();
    Task<HouseDetailDto?> Get(int id);

    Task<HouseDetailDto> Update(HouseDetailDto dto);

    Task<HouseDetailDto> Add(HouseDetailDto dto);

    Task Delete(int id);
}

public class HouseRepository : IHouseRepository
{
    public readonly HouseDbContext context;
    public HouseRepository(HouseDbContext _context)
    {
        context = _context;
    }

    private static void DtoToEntity(HouseDetailDto dto, HouseEntity e){
        e.Address = dto.Address;
        e.Country = dto.Country;
        e.Description = dto.Description;
        e.Price = dto.Price;
        e.Photo = dto.Photo;
    }

    private static HouseDetailDto EntityToDetailDto(HouseEntity e){
        return new HouseDetailDto(e.Id,e.Address,e.Country,e.Price,e.Description,e.Photo);
    }

    public async Task<List<HouseDto>> GetAll(){
        return await context.Houses.Select(h => 
                new HouseDto(h.Id,h.Address, h.Country, h.Price)).ToListAsync();
    }

    public async Task<HouseDetailDto?> Get(int id){
        var e = await context.Houses.SingleOrDefaultAsync(h => h.Id == id);
        if (e == null)
           return null;
        return EntityToDetailDto(e);

    }

    public async Task<HouseDetailDto> Add(HouseDetailDto dto){
        var entity = new HouseEntity();
        DtoToEntity(dto,entity);
        context.Houses.Add(entity);
        await context.SaveChangesAsync();
        return EntityToDetailDto(entity);
    }

    public async Task<HouseDetailDto> Update(HouseDetailDto dto){
        var entity = await context.Houses.FindAsync(dto.Id);
        if (entity == null) 
           throw new ArgumentException($"Error on updating house{dto.Id}");
        DtoToEntity(dto,entity);
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return EntityToDetailDto(entity);
    }

    public async Task Delete(int id){
        var entity = await context.Houses.FindAsync(id);
        if (entity == null)
            throw new ArgumentException($"Error deleting house {id}");
        context.Houses.Remove(entity);
        await context.SaveChangesAsync();

    }
    
}