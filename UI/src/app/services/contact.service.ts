import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contact } from '../interfaces/contact';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  private baseUrl = 'http://localhost:5000/Contacts';

  constructor(private http: HttpClient) {}

  getContacts(): Observable<Contact[]> {
    const token = sessionStorage.getItem('Token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<Contact[]>(this.baseUrl, { headers });
  }

  addContact(contact: Contact) {
    const token = sessionStorage.getItem('Token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.baseUrl}/CreateContact`, contact, { headers });
  }

  deleteContact(id: number) {
    const token = sessionStorage.getItem('Token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.baseUrl}/DeleteContact/${id}`, id, { headers });
  }

}