import { Injectable } from '@angular/core';
import { City } from '../models/city';

@Injectable({
  providedIn: 'root' // Fournit ce service à toute l'application
})
export class CityDataService {
  private selectedCity: City | null = null;

  setCity(city: City): void {
    this.selectedCity = city;
  }

  getCity(): City | null {
    return this.selectedCity;
  }

  clearCity(): void {
    this.selectedCity = null;
  }
}
