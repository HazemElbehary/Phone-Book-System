import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../interfaces/auth';
import { Observable } from 'rxjs';
import { ReturnedUser } from '../interfaces/returnedUser';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = 'http://localhost:5000';


  constructor(private http: HttpClient) { }

  registerUser(userDetails: User) {
    return this.http.post(`${this.baseUrl}/Account/register`, userDetails);
  }

  loginUser(Email: string, Password: string): Observable<HttpResponse<ReturnedUser>>{
    return this.http.post<any>(`${this.baseUrl}/Account/login`, { Email, Password }, { observe: 'response' });
  }

  getCurrentUserRole(): Observable<HttpResponse<any>> {
    const token = sessionStorage.getItem("Token");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<string>(`${this.baseUrl}/Account/getCurrentUserRole`, { headers, observe: 'response' });
  }
}
