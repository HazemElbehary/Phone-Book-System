import { Component, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { Contact } from 'src/app/interfaces/contact';
import { ContactService } from 'src/app/services/contact.service';
import { AuthService } from 'src/app/services/auth.service';
import { HttpResponse } from '@angular/common/http';


@Component({
  selector: 'app-contact-table',
  templateUrl: './contact-table.component.html'
})
export class ContactTableComponent implements OnInit, OnChanges {
  contacts: any[] = [];
  filteredContacts: any[] = [];

  // Receive search term from parent component
  @Input() searchTerm: string = '';
  userRole: string = '';

  constructor(private contactService: ContactService, private authService: AuthService) {}
  
  ngOnInit() {
    this.authService.getCurrentUserRole().subscribe((response: HttpResponse<any>) => {
      this.userRole = response.body.role || '';
      this.getContacts();
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['searchTerm']) {
      this.filterContacts();
    }
  }

  getContacts() {
    this.contactService.getContacts().subscribe((data: any[]) => {
      this.contacts = data;
      this.filterContacts();
    });
  }
  
  filterContacts() {
    if (this.searchTerm) {
      const term = this.searchTerm.toLowerCase();
      this.filteredContacts = this.contacts.filter(contact =>
        (contact.name?.toLowerCase().includes(term) ?? false) ||
        (contact.phoneNumber?.includes(term) ?? false) ||
        (contact.email?.toLowerCase().includes(term) ?? false)
      );
    } else {
      this.filteredContacts = this.contacts;
    }
  }
  
  deleteContact(contact: any) {
    debugger;
    if (confirm(`Are you sure you want to delete ${contact.name}?`)) {
      this.contactService.deleteContact(contact.id).subscribe(() => {
        this.contacts = this.contacts.filter(c => c.id !== contact.id);
        this.filterContacts();
      });
    }
  }
  
}