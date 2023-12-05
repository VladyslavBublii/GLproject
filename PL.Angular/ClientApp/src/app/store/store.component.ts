import { Component } from '@angular/core';
import { StoreService } from './store.service';
import { StorageService } from '../storage/storage.service';

@Component({
  selector: 'app-store',
  templateUrl: './store.component.html',
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
            image: `data:image/png;base64,${item.image}`,
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
    image: string,
  }