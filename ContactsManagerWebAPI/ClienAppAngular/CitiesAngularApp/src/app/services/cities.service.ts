import { Injectable } from '@angular/core';
import { City } from '../models/city';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'
const API_BASE_URL: string = "https://localhost:7082/api/v1/city";

@Injectable({
  providedIn: 'root'
})
export class CitiesService {
  cities: City[] = [];
  headers: any   = new HttpHeaders();
  constructor(private httpClient : HttpClient)
  {
    const token = localStorage.getItem('token');
    console.log("Token récupéré :", token);  // Debugging

    if (!token) {
      console.error("⚠️ Aucun token trouvé, l'utilisateur doit se reconnecter !");
      console.error("Token introuvable");

    }

    this.cities = [];
    this.headers = new HttpHeaders().set("Authorization", `Bearer ${localStorage['token']}`);
  }

  public getCities(): Observable<City[]>
  {
    return this.httpClient.get<City[]>(API_BASE_URL, { headers: this.headers });
  }


  public postCity(theCity:City): Observable<City> {
    return this.httpClient.post<City>(API_BASE_URL ,theCity , { headers: this.headers }  );
  }

  public putCity(theCity: City): Observable<string> {
    return this.httpClient.put<string>(API_BASE_URL+"/"+theCity.cityId, theCity, { headers: this.headers });
  }

  public deleteCity(theCityId: string): Observable<void> {
    return this.httpClient.delete<void>(`${API_BASE_URL}/${theCityId}`, { headers: this.headers });
  }
}
