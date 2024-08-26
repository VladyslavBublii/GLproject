import { Component } from '@angular/core';
import { OrderService } from './order.service';
import { StorageService } from '../storage/storage.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent {
    constructor(
        private orderServise: OrderService, 
        private storageService: StorageService) {}
    
    isOrderEmpty = false;
    public userId: string = "";
    public orderInformation: Array<OrderInformation> = [];
    public orderProductList: Array<OrderProduct> = [];
    public productInformationList: Array<ProductInformation> = [];
    
    ngOnInit(): void {
        this.userId = this.storageService.getUserId();
        this.getData();
    }

    getData(): void {
      this.orderServise.getOrder(this.userId).subscribe(
        (data: any[]) => {
          this.orderInformation = data.map((item) => ({
            id: item.id,
            userId: item.userId,
            phoneNumber: item.phoneNumber,
            city: item.city,
            sum: item.sum.toString(),
            postIndex: item.postIndex,
          }));
          this.isOrderEmpty = false;
        },
        (error) => {
          this.isOrderEmpty = true;
          console.error(error);
        }
      );
    }

    payOrder(): void{
    }
}

export interface OrderProduct {
    orderId: string,
    productId: string
  }

export interface OrderInformation {
    id: string,
    userId: string,
    phoneNumber: string,
    city: string,
    postIndex: string,
    sum: string,
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