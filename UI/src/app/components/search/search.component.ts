import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-search',
  template: `
    <div class="input-group mb-3">
  <span class="input-group-text" id="search-icon">
    <i class="bi bi-search"></i>
  </span>
  <input
    type="text"
    class="form-control"
    placeholder="Search contacts..."
    aria-label="Search contacts"
    aria-describedby="search-icon"
    (input)="onSearch($event)"
  />
  `
})
export class SearchComponent {
  @Output() searchTerm = new EventEmitter<string>();

  onSearch(event: Event) {
    const input = event.target as HTMLInputElement;
    this.searchTerm.emit(input.value);
  }
}
