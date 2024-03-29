import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LoginService {

  private HttpOptions = {
    headers: {
      'Content-Type': 'application/json',
    },
  };

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  signinto(data: any) {
    return this.http.post(this.baseUrl + "login/signin", JSON.stringify(data), this.HttpOptions);
  }

  returnhome() {
    window.location.href = this.baseUrl + "/";
  }
}