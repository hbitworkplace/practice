using CityInfoApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoApi.Controllers
{
    [ApiController]
    [Route("api/Cities")]
    public class CitiesController:ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(CitiesDataStore.Curent.Cities);
        }
        [HttpGet("{cityId}")]
        public ActionResult<CityDto> GetCity(int cityId)
        {
           var city =  CitiesDataStore.Curent.Cities.Where(a=> a.Id  == cityId).FirstOrDefault();
            if(city==null)
            {
                return NotFound();
            }
            return Ok(city);
        }
    }
}
