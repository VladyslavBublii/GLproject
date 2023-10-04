import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class RegisterService {

  private HttpOptions = {
    headers: {
      'Content-Type': 'application/json',
    },
  };

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  registerinto(data: any) {
    return this.http.post(this.baseUrl + "registration/registration", JSON.stringify(data), this.HttpOptions);
  }
}