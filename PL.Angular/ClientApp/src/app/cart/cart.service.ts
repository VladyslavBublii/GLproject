import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CartService {

  private HttpOptions = {
    headers: {
      'Content-Type': 'application/json',
    },
  };

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  getBasket(userId: string): Observable<any[]> {
    return this.http.post<any[]>(this.baseUrl + "cart/getBasket", JSON.stringify(userId), this.HttpOptions);
  }

  addToBasket(userId: string, productId: string) {
    var body = {} as CartRequestModel;
    body.userId = userId;
    body.productId = productId;
    return this.http.post(this.baseUrl + "cart/add", 
    body,
    this.HttpOptions);
  }

  removeFromBasket(userId: string, productId: string) {
    var body = {} as CartRequestModel;
    body.userId = userId;
    body.productId = productId;
    return this.http.post(this.baseUrl + "cart/remove", 
    body,
    this.HttpOptions);
  }
}

export interface CartRequestModel {
  userId: string,
  productId: string
}