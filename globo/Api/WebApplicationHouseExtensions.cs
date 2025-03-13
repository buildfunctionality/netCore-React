using Microsoft.AspNetCore.Mvc;
using MiniValidation;
using Microsoft.Extensions.Options;
using System.Net.Cache;

public static class WebApplicationHouseExtensions{
    public static void MapHouseEndPoints(this WebApplication app)
    {
        
        app.MapGet("/houses", (IHouseRepository repo) => repo.GetAll()
        ).ProducesProblem(404).Produces<HouseDto[]>(StatusCodes.Status200OK);

        app.MapGet("/house/{houseId:int}",async (int houseId, IHouseRepository repo) =>
        {
            var house = await repo.Get(houseId);
            if (house == null)
                return Results.Problem($"House with ID {houseId} not found",statusCode: 404);
            return Results.Ok(house);
        }).ProducesProblem(404).Produces<HouseDetailDto>(StatusCodes.Status200OK);


        app.MapPost("/houses", async ([FromBody] HouseDetailDto dto, IHouseRepository repo) => {
            if(!MiniValidator.TryValidate(dto,out var errors))
                return Results.ValidationProblem(errors);
            var newhouse = await repo.Add(dto);
            return Results.Created($"/house/{newhouse.Id}",newhouse);
        }).Produces<HouseDetailDto>(StatusCodes.Status201Created).ProducesValidationProblem();

        app.MapPut("/house", async ([FromBody] HouseDetailDto dto, IHouseRepository repo) => {
        if(!MiniValidator.TryValidate(dto,out var errors))
                return Results.ValidationProblem(errors);
            if(await repo.Get(dto.Id) == null)
            return Results.Problem($"House {dto.Id} not found" , statusCode:404);
            var addHouse = await repo.Update(dto);
            return Results.Ok(addHouse);
            }).ProducesProblem(404).Produces<HouseDetailDto>(StatusCodes.Status201Created).ProducesValidationProblem(); 

        app.MapDelete("/houses/{Id:int}", async (int Id, IHouseRepository repo) => {
            if(await repo.Get(Id) == null)
                return Results.Problem($"House {Id} not found" , statusCode:404);
            await repo.Delete(Id);
            return Results.Ok();
        }).ProducesProblem(404).Produces<HouseDetailDto>(StatusCodes.Status200OK);


    }
}