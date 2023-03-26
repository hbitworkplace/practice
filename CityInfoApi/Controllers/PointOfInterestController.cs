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
            var city = CitiesDataStore.Curent.Cities.Where(i => i.Id == cityId).FirstOrDefault();
            if (city == null)
            {
                return NotFound();

            }
            var pointofinterestforcity = city.PointOfInterests;

            return Ok(pointofinterestforcity);
        }
        [HttpGet("{poinofinterest}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointofinterest(int cityId, int poinofinterest)
        {
            var city = CitiesDataStore.Curent.Cities.Where(c => c.Id == cityId).FirstOrDefault();
            if (city == null)
            {
                return NotFound();
            }
            var poinofinterests = city.PointOfInterests.Where(i => i.Id == poinofinterest).FirstOrDefault();
            if (poinofinterests == null)
            {
                return NotFound();
            }
            return Ok(poinofinterests);
        }
        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int CityId, PointOfInterestForCreationDto PointOfInterests)
        {
            var city = CitiesDataStore.Curent.Cities.Where(c => c.Id == CityId).FirstOrDefault();
            if (city == null)
            {
                return NotFound();
            }
            //finding max id for posting poinOfInterest
            var maxPointOfInterest = CitiesDataStore.Curent.Cities.SelectMany(c => c.PointOfInterests).Max(c => c.Id);
            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterest,
                Name = PointOfInterests.Name,
                Description = PointOfInterests.Description,
            };


            city.PointOfInterests.Add(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = CityId,
                    poinofinterest = finalPointOfInterest.Id,

                }, finalPointOfInterest);

        }
        [HttpPut("{pointofinterestid}")]
        public ActionResult UpdatePointOfInterest(int cityId,int pointofinterestid, PointOfInterestForUpdateDto pointOfInterest)
        {
            var city = CitiesDataStore.Curent.Cities.First(p => p.Id == cityId);
            if(city==null)
            {
                return NotFound();
            }
            var pointOfinterest = city.PointOfInterests.FirstOrDefault(p => p.Id == pointofinterestid);
            if(pointOfinterest == null)
            {
                return NotFound();
            }
            pointOfinterest.Name = pointOfInterest.Name;
            pointOfinterest.Description= pointOfInterest.Description;
            return NoContent();
        }
    }
}
