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
    public userId: string = "";
    public productInformationList: Array<ProductInformation> = [];
    
    ngOnInit(): void {
        this.userId = this.storageService.getUserId();
        this.getData();
    }

    getData(): void {
      this.cartServise.getBasket(this.userId).subscribe(
        (data: any[]) => {
          this.productInformationList = data.map((item) => ({
            id: item.id,
            name: item.name,
            description: item.description,
            category: item.category,
            price: item.price.toString(),
            count: item.count.toString(),
            imageName: item.imageName,
            urlImage: item.urlImage,
          }));
          this.isBasketEmpty = false;
        },
        (error) => {
          this.isBasketEmpty = true;
          console.error(error);
        }
      );
    }

    removeProductFromBasket(productId: string){
      this.cartServise.removeFromBasket(this.userId, productId).subscribe(
        (data: any) => {
          this.ngOnInit();
        },
        (error) => {
          console.error('Error:', error.error);
        }
      );
    }

    makeOrder(): void{
      this.cartServise.makeOrder(this.userId, this.productInformationList).subscribe(
        (data: any[]) => {
          console.log(data);
        },
        (error) => {
          console.error(error);
        }
      );
    }
}

export interface ProductInformation {
    id: string,
    name: string,
    description: string,
    category: string,
    price: string,
    count: string,
    imageName: string,
    urlImage: string
  }