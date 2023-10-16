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

  onEnterEmail(event: Event){
      this.RegisterModel.email = (<HTMLInputElement>event.target).value;
  }

  onEnterPassword(event: Event){
    this.RegisterModel.password = (<HTMLInputElement>event.target).value;
  }

  onEnterName(event: Event){
      this.RegisterModel.name = (<HTMLInputElement>event.target).value;
  }

  onEnterSurname(event: Event){
    this.RegisterModel.surName = (<HTMLInputElement>event.target).value;
  }

  onEnterCity(event: Event){
    this.RegisterModel.city = (<HTMLInputElement>event.target).value;
  }

  onEnterPostIndex(event: Event){
    this.RegisterModel.postIndex = (<HTMLInputElement>event.target).value;
  }
}

export interface RegisterModel {
  email: string,
  password: string,
  name: string,
  surName: string,
  city: string,
  postIndex: string,
}
