import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ProductService } from './product.service';
import { ProductModel } from '../models/productModel';

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

    public product: ProductModel = new ProductModel();

    onSubmit(): void {
        this.product = new ProductModel(
            this.checkoutForm.value.category ?? ' ',
            this.checkoutForm.value.name ?? ' ',
            this.checkoutForm.value.description ?? ' ',
            this.checkoutForm.value.price ?? 0,
            this.checkoutForm.value.ImageName ?? ' '
        );

        this.productService.addProduct(this.product).subscribe(
            (data) => {
                console.log('Product added:', data);
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