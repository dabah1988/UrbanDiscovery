import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'
import { RegisterUser } from '../models/register-user';
import { LoginUser } from '../models/login-user';
const API_BASE_URL: string = "https://localhost:7082/api/v1/Account/";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  headers: any = new HttpHeaders();
  public currentUserName: string | null = null ;
  constructor(private httpClient: HttpClient) {
    this.headers = new HttpHeaders().set("Authorization", `Bearer ${localStorage['token']}`);
  }

  public postRegister(registerUser: RegisterUser):Observable<any>
  {
    return this.httpClient.post<any>(`${API_BASE_URL}`, registerUser, { headers: this.headers });
  }

  public postLogin(loginUser: LoginUser): Observable<any> {
    return this.httpClient.post<any>(`${API_BASE_URL}login`, loginUser, { headers: this.headers });
  }

  public getLogout(): Observable<string> {
    return this.httpClient.get<string>(`${API_BASE_URL}logout`, { headers: this.headers });
  }

  public refreshToken(): Observable<any> {
    var token = localStorage["token"]  ;
    var refreshToken = localStorage["refreshToken"];
    return this.httpClient.post<any>(`${API_BASE_URL}generateNewJwtToken`,
      { token: token, refreshToken: refreshToken }, { headers: this.headers });
  }

}
