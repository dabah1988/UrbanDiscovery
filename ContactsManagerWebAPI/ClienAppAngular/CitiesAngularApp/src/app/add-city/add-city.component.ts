import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { CitiesService } from '../services/cities.service';
import { City } from '../models/city';
import { generateGuid } from '../util';  // Importez la fonction
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-city',
  templateUrl: './add-city.component.html',
  styleUrls: ['./add-city.component.css']
})
export class AddCityComponent
{
  postCityForm: FormGroup;
  cityName: string = '';
  cityPopulation: number = 0;
  cityArea: number = 0;
  cityId: string | null = null;
  editCytyId: string | null = null;
  cities: City[] = [];
  isRegisterFormSubmitted: boolean;

  constructor(private citiesService: CitiesService, private router: Router)
  {
    this.isRegisterFormSubmitted = false;
    this.postCityForm = new FormGroup({
      cityId: new FormControl( ' ', [Validators.required]),
      cityName: new FormControl({ value: null, disabled: false }),
      cityPopulation: new FormControl(null, [Validators.required, Validators.min(0)]),
      cityArea: new FormControl(null, [Validators.required, Validators.min(0)])
    });
  }
  // Ajout de la méthode onSubmit() pour traiter le formulaire
  onSubmit()
  {
    this.isRegisterFormSubmitted = true;
    if (!this.postCityForm.valid) return;

    if (this.postCityForm.valid) {
      console.log('Formulaire soumis', this.postCityForm.value);
      // Récupération des données du formulaire et conversion en `City`
      const cityData: City = {
        cityId: generateGuid(),  // Ou utiliser `null` si ce n'est pas nécessaire
        cityName: this.postCityForm.get('cityName')?.value,  // Récupérer cityName depuis le formulaire
        cityPopulation: this.postCityForm.get('cityPopulation')?.value,
        cityArea: this.postCityForm.get('cityArea')?.value,
      };

      this.citiesService.postCity(cityData).subscribe(
        {        
          next: (data: City) => {
            console.log(data);
            console.log(cityData);
            this.router.navigate(['/cities']); 
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
