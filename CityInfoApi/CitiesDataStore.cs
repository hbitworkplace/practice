using CityInfoApi.Model;
using System.Net;

namespace CityInfoApi
{
    public class CitiesDataStore
    {
        public ICollection<CityDto> Cities { get; set; }
        public ICollection<PointOfInterestDto> PointOfInterests { get; set; }
        public static CitiesDataStore Curent { get;  } = new CitiesDataStore();
        public CitiesDataStore()
        {
            //initialize dummy data
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Pakistan",
                    Description="The City of muslims ",
                    PointOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id=1,
                            Name="Muslim",
                            Description="Offer Prayerss more often"
                        },
                        new PointOfInterestDto()
                        {
                            Id=2,
                            Name="People",
                            Description="people are loving"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name="India",
                    Description="Bollywood is well known",
                    PointOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id=3,
                            Name="Bollywood",
                            Description="known for there high crosing movies"
                        },
                        new PointOfInterestDto()
                        {
                            Id=4,
                            Name="Taj Mahal",
                            Description=" Made in love with wife the badashaa"
                        }
                    }
                },
                new CityDto()
                {
                    Id=3,
                    Name="Siri Lanka",
                    Description="Economic crisess",
                    PointOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id=5,
                            Name="Bank Crupt",
                            Description="Recently got bankcrupt due his politons"

                        },
                        new PointOfInterestDto()
                        {
                            Id=6,
                            Name="roads",
                            Description="have clear roads in srilanka"
                        }
                    }

                }
                
            };
        }
    }
}
