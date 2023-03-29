using CityInfoApi.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CityInfoApi.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/PointOfInterests")]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger<PointOfInterestController> _logger;

        public PointOfInterestController(ILogger<PointOfInterestController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointOfInterestForACity(int cityId)
        {
           try
            {
                
                var city = CitiesDataStore.Curent.Cities.Where(i => i.Id == cityId).FirstOrDefault();
                if (city == null)
                {
                    _logger.LogInformation($"City with id{cityId} wasn't found when you accesing poinofInterests");
                    return NotFound();

                }
                var pointofinterestforcity = city.PointOfInterests;

                return Ok(pointofinterestforcity);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting pointofinterests for {cityId}", ex);
                return StatusCode(500, "Something Went Wrong While handling your request!");
            }
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
        [HttpPut("{poinofinterest}")]
        public ActionResult UpdatePointOfInterest(int cityId, int poinofinterest, PointOfInterestForUpdateDto pointOfInterest)
        {
            var city = CitiesDataStore.Curent.Cities.First(p => p.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfinterest = city.PointOfInterests.FirstOrDefault(p => p.Id == poinofinterest);
            if (pointOfinterest == null)
            {
                return NotFound();
            }
            pointOfinterest.Name = pointOfInterest.Name;
            pointOfinterest.Description = pointOfInterest.Description;
            return NoContent();
        }
        [HttpPatch("{poinofinterest}")]
        public ActionResult UpdatePointOfInterestPactch(int cityId, int poinofinterest, JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            var city = CitiesDataStore.Curent.Cities.Where(p => p.Id == cityId).FirstOrDefault();
            if (city == null)
            {
                return NotFound();
            }
            var PointOfInterestFromStore = city.PointOfInterests.Where(p => p.Id == poinofinterest).FirstOrDefault();
            if (PointOfInterestFromStore == null)
            {
                return NotFound();
            }
            var pointOfInterestForPatch = new PointOfInterestForUpdateDto()
            {
                Name = PointOfInterestFromStore.Name,
                Description = PointOfInterestFromStore.Description

            };
            patchDocument.ApplyTo(pointOfInterestForPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            PointOfInterestFromStore.Name = pointOfInterestForPatch.Name;
            PointOfInterestFromStore.Description = pointOfInterestForPatch.Description;

            return NoContent();

        }
        [HttpDelete("{poinofinterest}")]
        public ActionResult PointOfInterestToDelete(int cityId,int poinofinterest)
        {
            var city = CitiesDataStore.Curent.Cities.FirstOrDefault(c=>c.Id==cityId);
            if(city==null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(p => p.Id == poinofinterest);
            if(pointOfInterestFromStore==null)
            {
                return NotFound();
            }
            city.PointOfInterests.Remove(pointOfInterestFromStore);
            return NoContent();
        }
    }
}
