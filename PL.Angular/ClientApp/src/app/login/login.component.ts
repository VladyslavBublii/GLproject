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
    // http.get<Login[]>(baseUrl + 'account/login').subscribe(result => {
    //    this.Login = result;
    // }, error => console.error(error));
  }

  login() {
    //console.log(this.http.get(this.baseUrl.toString() + "account/login".toString()));
    this.http.get(this.baseUrl + "login/login").subscribe(result => {
      console.log(result);
    })
    //console.log(t);
    //console.log(this.baseUrl);
    //console.log(this.Login);
  }
}

interface Login {
  email: string;
  password: string;
}
