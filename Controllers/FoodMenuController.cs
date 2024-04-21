using Microsoft.AspNetCore.Mvc;
using smartkantin.Models;
using smartkantin.Repository;

namespace smartkantin.Controllers;

[ApiController]
[Route("/api/public/food")]
public class FoodMenuController : ControllerBase
{
    private readonly IFoodRepository foodRepository;

    public FoodMenuController(IFoodRepository foodRepository)
    {
        this.foodRepository = foodRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Food>> GetTopMenu()
    {
        var result = await foodRepository.GetAll();
        return result;
    }
}