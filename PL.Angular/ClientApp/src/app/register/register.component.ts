import { Component } from '@angular/core';
import { RegisterService } from './register.service';
//import { RegisterModel} from '../models/registerModel';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
})

export class RegisterComponent {
  constructor(private registerService: RegisterService) {}
  public RegisterModel = {} as RegisterModel;
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  registerinto() {
    this.registerService.registerinto(this.RegisterModel).subscribe(
    (res) => {
      console.log('Answer:', res);
    },
    (error) => {
      console.error('Error:', error);
    }
  );
  }
}

export interface RegisterModel {
  name: string,
  surName: string,
  city: string,
  postIndex: string,
  email: string,
  password: string,
}