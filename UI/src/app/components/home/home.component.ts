import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ContactTableComponent } from '../contact-table/contact-table.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor(private router: Router) { }

  // Holds the search term coming from the SearchComponent.
  currentSearch: string = '';
  @ViewChild('contactTable') contactTable!: ContactTableComponent;

  // Update the current search term when the SearchComponent emits a new value.
  updateSearch(term: string) {
    this.currentSearch = term;
  }

  logOut() {
    sessionStorage.clear();
    this.router.navigate(['login']);
  }

  refreshContacts() {
    debugger;
    this.contactTable.getContacts();
  }
}
