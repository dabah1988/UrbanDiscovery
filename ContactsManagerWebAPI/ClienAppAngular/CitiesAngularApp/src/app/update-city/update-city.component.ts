import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { City } from '../models/city';
import { CitiesService } from '../services/cities.service';
import { CityDataService } from '../services/city-state.service';
import { Router } from '@angular/router';
import { generateGuid } from '../util';  // Importez la fonction

@Component({
  selector: 'app-update-city',
  templateUrl: './update-city.component.html',
  styleUrls: ['./update-city.component.css']
})
export class UpdateCityComponent 
{
  putCityForm: FormGroup;
  cityName: string = '';
  cityPopulation: number = 0;
  cityArea: number = 0;
  cityId: string | null = null;
  editCytyId: string | null = null;
  city: City | null = null;   ;

  constructor(private citiesService: CitiesService,
    private cityDataService: CityDataService,
    private router: Router)
  {
    this.putCityForm = new FormGroup({
      cityId: new FormControl(' ', [Validators.required]),
      cityName: new FormControl({ value: null, disabled: false }),
      cityPopulation: new FormControl(null, [Validators.required, Validators.min(0)]),
      cityArea: new FormControl(null, [Validators.required, Validators.min(0)])
    });
  }
  ngOnInit(): void  {
    this.city = this.cityDataService.getCity();
    if (this.city) {
      this.putCityForm.patchValue(this.city);
    } else {
      // Si aucune ville n'est sélectionnée, retour à la liste des villes
       alert("Aucune ville selectionnée")
    }
  }

  goToCities() {
    this.router.navigate(['/cities']);  // Redirige vers le composant `CitiesComponent`
  }
  onSubmit() {
    if (this.putCityForm.valid) {
      console.log('Formulaire soumis', this.putCityForm.value);
      // Récupération des données du formulaire et conversion en `City`
      const cityData: City = {
        cityId: this.putCityForm.get('cityId')?.value,   // Ou utiliser `null` si ce n'est pas nécessaire
        cityName: this.putCityForm.get('cityName')?.value,  // Récupérer cityName depuis le formulaire
        cityPopulation: this.putCityForm.get('cityPopulation')?.value,
        cityArea: this.putCityForm.get('cityArea')?.value,
      };

      this.citiesService.putCity(cityData).subscribe(
        {
          next: (data: string) => {
            console.log(data);
            console.log(cityData);
            alert("Modification effectuée");
            this.goToCities();
          },
          error: (error: any) => {
            if (error.status === 400) {
              console.error('Validation errors:', error.error.errors); // Affichez les erreurs de validation
            }
            alert('Erreur lors de l\'ajout de la ville');
            console.log(error);
          },
          complete: () => { }
        }
      );
    } else {
      console.log('Formulaire invalide');
    }
  }
}

