export class ProductModel {

    constructor(
        public category: string = '',
        public name: string = '',
        public description: string = '',
        public price: number = 0,
        public ImageName: string = '',
    ) {  }
}