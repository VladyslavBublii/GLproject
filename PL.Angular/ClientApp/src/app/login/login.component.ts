import { Component } from '@angular/core';
import { LoginService } from './login.service';
import { StorageService } from '../storage/storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(private loginServise: LoginService, private storageService: StorageService) {}
  public Login = {} as Login;
  isLoggedIn = false;

  ngOnInit(): void {
    if (this.storageService.isLoggedIn()) {
      this.isLoggedIn = true;
    }
  }

  signinto() {
      this.loginServise.signinto(this.Login).subscribe(
      (res) => {
        this.storageService.saveUser(res);
        this.isLoggedIn = true;
        console.log('Answer:', res);
        this.loginServise.returnhome();
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