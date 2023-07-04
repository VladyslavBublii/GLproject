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
    http.get<Login[]>(baseUrl + 'account/login').subscribe(result => {
       this.Login = result;
    }, error => console.error(error));
  }

  login() {
    var t = this.http.get(this.baseUrl + "account/login");
    console.log(t);
    console.log(this.baseUrl);
    console.log(this.Login);
  }
}

interface Login {
  email: string;
  password: string;
}
