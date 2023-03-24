using CityInfoApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoApi.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/PointOfInterests")]
    public class PointOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointOfInterestForACity(int cityId)
        {
            var city = CitiesDataStore.Curent.Cities.Where(i=> i.Id==cityId).FirstOrDefault();
            if(city==null)
            {
                return NotFound();

            }
            var pointofinterestforcity = city.PointOfInterests;
            
            return Ok(pointofinterestforcity);
        }
        [HttpGet("{poinofinterest}")]
        public ActionResult<PointOfInterestDto> GetPointofinterest(int cityId,int poinofinterest)
        {
            var city = CitiesDataStore.Curent.Cities.Where(c => c.Id == cityId).FirstOrDefault();
            if(city==null)
            {
                return NotFound();
            }
            var poinofinterests= city.PointOfInterests.Where(i=> i.Id==poinofinterest).FirstOrDefault();
            if(poinofinterests==null)
            {
                return NotFound();
            }
            return Ok(poinofinterests);
        }
    }
}
