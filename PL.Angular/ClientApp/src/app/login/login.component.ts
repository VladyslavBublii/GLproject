import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {
  public Login: Login[] = [];
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  login() {
    console.log(this.baseUrl);
    this.http.get(this.baseUrl + "account/login").subscribe(result => {
      console.log(result);
    })
  }
}

interface Login {
  email: string;
  password: string;
}
