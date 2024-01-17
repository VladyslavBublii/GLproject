import { Component } from '@angular/core';
import { StoreService } from './store.service';
import { StorageService } from '../storage/storage.service';

@Component({
  selector: 'app-store',
  templateUrl: './store.component.html',
  styleUrls: ['./store.component.css']
})
export class StoreComponent {
    constructor(
        private storeServise: StoreService, 
        private storageService: StorageService) {}
    
    isLoggedIn = false;
    public mainProductInformationList: Array<MainProductInformation> = [];
    
    ngOnInit(): void {
        if (this.storageService.isLoggedIn()) {
          this.isLoggedIn = true;
        }
        this.getData();
    }

    getData(): void {
      this.storeServise.get().subscribe(
        (data: any[]) => {
          this.mainProductInformationList = data.map((item) => ({
            name: item.name,
            price: item.price.toString(),
            imageUrl: item.imageUrl,
            googleUrl: item.googleUrl,
          }));
          console.log(data);
          console.log(this.mainProductInformationList);
        },
        (error) => {
          console.error(error);
        }
      );
    }
}

export interface MainProductInformation {
    name: string,
    price: string,
    imageUrl: string,
    googleUrl: string,
  }