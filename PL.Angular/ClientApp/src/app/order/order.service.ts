import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OrderService {

  private HttpOptions = {
    headers: {
      'Content-Type': 'application/json',
    },
  };

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  getOrder(userId: string): Observable<any[]> {
    return this.http.post<any[]>(this.baseUrl + "order/getByUserId", JSON.stringify(userId), this.HttpOptions);
  }
}
