export class City
{
  cityId: string | null;
  cityName: string | null;
  cityPopulation: number | null  ;
  cityArea: number | null  ;

 constructor(cityId: string | null = null, cityName: string | null = null, cityPopulation: number = 0,
   cityArea: number | null)
  {
   this.cityId = cityId;
   this.cityArea = cityArea;
   this.cityName = cityName;
   this.cityPopulation = cityPopulation;
  }
}
