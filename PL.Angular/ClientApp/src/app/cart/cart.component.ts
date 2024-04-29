import { Component } from '@angular/core';
import { CartService } from './cart.service';
import { StorageService } from '../storage/storage.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {
    constructor(
        private cartServise: CartService, 
        private storageService: StorageService) {}
    
    isBasketEmpty = false;
    userId: string = "";
    public productInformationList: Array<ProductInformation> = [];
    
    ngOnInit(): void {
        this.userId = this.storageService.getUserId();
        this.getData();
    }

    getData(): void {
      this.cartServise.getBasket(this.userId).subscribe(
        (data: any[]) => {
          this.productInformationList = data.map((item) => ({
            name: item.name,
            description: item.description,
            category: item.category,
            price: item.price.toString(),
            count: item.count.toString(),
            imageName: item.imageName,
            googleUrl: item.googleUrl,
          }));
          this.isBasketEmpty = false;
          console.log(data);
          console.log(this.productInformationList);
        },
        (error) => {
          this.isBasketEmpty = true;
          console.error(error);
        }
      );
    }
}

export interface ProductInformation {
    name: string,
    description: string,
    category: string,
    price: string,
    count: string,
    imageName: string,
    googleUrl: string,
  }