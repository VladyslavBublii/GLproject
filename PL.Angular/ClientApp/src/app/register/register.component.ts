import { Component } from '@angular/core';
import { Router } from '@angular/router';
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
    private router: Router,
    private registerService: RegisterService,
    private storageService: StorageService,
    private loginServise: LoginService) {
  }
  public RegisterModel = {} as RegisterModel;

  registerinto() {
    this.registerService.registerinto(this.RegisterModel).subscribe(
    (res) => {
      console.log('Answer: ', res);
      this.loginServise.signinto({email: this.RegisterModel.email, password: this.RegisterModel.password}).subscribe(
        (resLog) => {
          this.storageService.saveUser(resLog);
          console.log('Answer:', resLog);
          this.loginServise.returnhome();
          //this.router.navigate(['/store']);
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
