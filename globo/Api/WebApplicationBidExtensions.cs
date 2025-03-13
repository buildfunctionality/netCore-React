using Microsoft.AspNetCore.Mvc;
using MiniValidation;
using Microsoft.Extensions.Options;
using System.Net.Cache;

public static class WebApplicationBidExtensions{
    public static void MapBidExtensions(this WebApplication app){
        
app.MapGet("house/{houseId:int}/bids",async (int houseId, IBidRepository bidRepo,IHouseRepository houseRepo) => {
 if (await houseRepo.Get(houseId) == null)
 return Results.Problem($"House {houseId} not found", statusCode:404);
 var bids = await bidRepo.Get(houseId);
 return Results.Ok(bids);
});


app.MapPost("house/{houseId:int}/bids",async (int houseId, [FromBody]BidDto dto,IBidRepository bidRepo) => {
 if (dto.HouseId != houseId)
    return Results.Problem($"Not match",statusCode:StatusCodes.Status400BadRequest);
if(!MiniValidator.TryValidate(dto,out var errors))
        return Results.ValidationProblem(errors);
var newBid = await bidRepo.Add(dto);
return Results.Created($"/houses/{newBid.HouseId}/bids",newBid);  
}).ProducesValidationProblem().ProducesProblem(400).Produces<BidDto>(statusCode:StatusCodes.Status201Created);

    }
}