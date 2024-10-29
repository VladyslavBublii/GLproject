import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { ProductInformation } from './product.component';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  private HttpOptions = {
    headers: {
      'Content-Type': 'application/json',
    },
  };

  addProduct(product: ProductInformation) {
    return this.http.post(this.baseUrl + "product/add", JSON.stringify(product), this.HttpOptions);
  }

  getItems() {
    return [
      { category: 'Electronics', name: 'Laptop', description: 'Gaming laptop', price: 1500, ImageName: 'laptop.jpg' },
    ];
  }
}