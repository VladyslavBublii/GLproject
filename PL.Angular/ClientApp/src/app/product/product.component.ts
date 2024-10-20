import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ProductService } from './product.service';
//import { ProductInformation } from '../models/productInformation';
//import { ProductInformation } from '../models/productInformation';

@Component({
    selector: 'app-product',
    templateUrl: './product.component.html',
    styleUrls: ['./product.component.css']
})
export class ProductComponent {
    items = this.productService.getItems();

    checkoutForm = this.formBuilder.group({
        category: '',
        name: '',
        description: '',
        price: 0,
        ImageName: ''
    });

    constructor(
        private formBuilder: FormBuilder,
        private productService: ProductService) 
    {}

    public product = {} as ProductInformation;

    onSubmit(): void {
        this.product.category = this.checkoutForm.value.category ?? ' ';
        this.product.name = this.checkoutForm.value.name ?? ' ';
        this.product.description = this.checkoutForm.value.description ?? ' ';
        this.product.price = this.checkoutForm.value.price ?? 0;
        this.product.ImageName = this.checkoutForm.value.ImageName ?? ' ';
        this.productService.addProduct(this.product).subscribe(
            (data) => {
            },
            (error) => {
              console.error(error);
            }
        );
        console.log(
            'Your product has been added',
            this.checkoutForm.value
        );
        this.checkoutForm.reset();
    }
}

export interface ProductInformation {
    category:string,
    name: string,
    description: string,
    price: number,
    ImageName: string
}