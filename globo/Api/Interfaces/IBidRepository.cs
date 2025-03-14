
public interface IBidRepository
{
    Task<List<BidDto>> Get(int houseId);

    Task<BidDto> Add(BidDto bid);
}
