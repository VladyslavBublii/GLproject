import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {
  public login: Login[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Login[]>(baseUrl + 'login').subscribe(result => {
      this.login = result;
    }, error => console.error(error));
  }
}

interface Login {
  email: string;
  password: string;
}
