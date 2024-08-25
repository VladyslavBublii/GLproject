import { Component } from '@angular/core';
import { RegisterService } from './register.service';
import { StorageService } from '../storage/storage.service';
import { LoginService } from '../login/login.service';
//import { RegisterModel} from '../models/registerModel';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent {
  constructor(
    private registerService: RegisterService,
    private storageService: StorageService,
    private loginServise: LoginService) {
  }
  public RegisterModel = {} as RegisterModel;

  registerinto() {
    this.registerService.registerinto(this.RegisterModel).subscribe(
    (res) => {
      this.loginServise.signinto({id: "00000000-0000-0000-0000-000000000000", email: this.RegisterModel.email, password: this.RegisterModel.password}).subscribe(
        (resLog) => {
          this.storageService.saveUserData(resLog);
          this.loginServise.returnhome();
        }
      );
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
