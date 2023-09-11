import { Component } from '@angular/core';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {
  public Login = {} as Login;

  constructor(private loginServise: LoginService) {}

  signin() {
    this.loginServise.signin().subscribe(
      (res) => {
        console.log('Answer:', res);
      },
      (error) => {
        console.error('Error:', error);
      }
    );
  }

  signinto() {
      this.loginServise.signinto(this.Login).subscribe(
      (res) => {
        console.log('Answer:', res);
      },
      (error) => {
        console.error('Error:', error);
      }
    );
  }

  onEnterEmail(event: Event){
      this.Login.email = (<HTMLInputElement>event.target).value;
  }

  onEnterPassword(event: Event){
    this.Login.password = (<HTMLInputElement>event.target).value;
  }
}

export interface Login {
  email: string;
  password: string;
}
