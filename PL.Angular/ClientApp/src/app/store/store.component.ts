import { Component } from '@angular/core';
import { StoreService } from './store.service';
import { StorageService } from '../storage/storage.service';
import { CartService } from '../cart/cart.service';

@Component({
  selector: 'app-store',
  templateUrl: './store.component.html',
  styleUrls: ['./store.component.css']
})
export class StoreComponent {
    constructor(
        private storeServise: StoreService, 
        private storageService: StorageService,
        private cartServise: CartService) {}
    
    isLoggedIn = false;
    userId: string = "";
    public mainProductInformationList: Array<MainProductInformation> = [];
    
    ngOnInit(): void {
        if (this.storageService.isLoggedIn()) {
          this.isLoggedIn = true;
          this.userId = this.storageService.getUserId();
        }
        this.getData();
    }

    getData(): void {
      this.storeServise.get().subscribe(
        (data: any[]) => {
          this.mainProductInformationList = data.map((item) => ({
            id: item.id,
            name: item.name,
            price: item.price.toString(),
            imageName: item.imageName,
            googleUrl: item.googleUrl,
          }));
          console.log(this.mainProductInformationList);
        },
        (error) => {
          console.error(error);
        }
      );
    }

    addProductToBasket(productId: string){
      this.cartServise.addToBasket(this.userId, productId).subscribe(
        (data: any) => {
        },
        (error) => {
          console.error('Error:', error.error);
        }
      );
    }
}

export interface MainProductInformation {
    id: string,
    name: string,
    price: string,
    imageName: string,
    googleUrl: string,
  }