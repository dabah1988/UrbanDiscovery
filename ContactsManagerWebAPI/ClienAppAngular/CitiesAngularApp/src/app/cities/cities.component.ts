import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { City } from '../models/city';
import { CitiesService } from '../services/cities.service';
import { CityDataService } from '../services/city-state.service';

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styleUrls: ['./cities.component.css']
})
export class CitiesComponent {
  cities: City[] = [];

  constructor(
    private citiesService: CitiesService,
    private cityDataService: CityDataService,
    private router: Router)
  {

  }

  editCity(city: City): void {
    this.cityDataService.setCity(city); // Stocke la ville sélectionnée
    this.router.navigate(['/updateCity']); // Navigue vers le composant de mise à jour
  }
  deleteCity(city: City): void {
    this.cityDataService.setCity(city); // Stocke la ville sélectionnée
    if (window.confirm("Voulez-vous vraiment supprimer cette ville ?")) {
      if (city?.cityId) {
        this.citiesService.deleteCity(city.cityId).subscribe({
          next: () => {
            alert("Ville supprimée avec succès !");
          },
          error: (error) => {
            console.log("Erreur lors de la suppression :", error);
            alert("Une erreur est survenue lors de la suppression de la ville.");
          }
        });
      }
    }
  }

  loadCities()
  {
    this.citiesService.getCities().subscribe(
      {
        next: (response: City[]) => {
          this.cities = response;
          console.log(this.cities);
          this.cities.forEach(city => console.log(`cityArea: ${city.cityArea}, Type: ${typeof city.cityArea}`));
        },
        error: (error: any) => { console.log(error) },
        complete: () => { }
      }
    );
  }


  ngOnInit()
  {
    this.loadCities();
  }


 
}
