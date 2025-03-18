import { HttpResponse } from '@angular/common/http';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { ContactService } from 'src/app/services/contact.service';

@Component({
  selector: 'app-contact-form',
  templateUrl: './contact-form-component.html',
})
export class ContactFormComponent {
  contactForm: FormGroup;
  @Output() contactAdded = new EventEmitter<void>();
  userRole: string = '';

  constructor(private fb: FormBuilder, private contactService: ContactService, private authService: AuthService) {
    this.contactForm = this.fb.group({
      name: ['', Validators.required],
      phoneNumber: [
        '',
        [
          Validators.required,
          Validators.pattern(/^\+?(?:[0-9\-\(\)\/\.]\s?){6,15}[0-9]{1}$/),
        ],
      ],
      email: ['', Validators.email]
    });
  }

  ngOnInit(){
    this.authService.getCurrentUserRole().subscribe((response: HttpResponse<any>) => {
          this.userRole = response.body.role || '';
        });
  }
  onSubmit() {
    if (this.contactForm.valid) {
      this.contactService.addContact(this.contactForm.value).subscribe(() => {
        this.contactAdded.emit();
        this.contactForm.reset();
      });
    } else {
      this.contactForm.markAllAsTouched();
    }
  }
}
