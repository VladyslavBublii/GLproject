import { Component } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
})
export class RegisterComponent {
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}

interface Register {
  email: string;
  password: string;
  confirmPassword: string;
  name: string;
  surName: string;
  city: string;
  postIndex: string;
}
