import { OrderedProduct } from './orderedProduct';

export class OrderModel {

    constructor(
        public id: string = '',
        public userId: string = '',
        public phoneNumber: string = '',
        public city: string = '',
        public postIndex: string = '',
        public sum: string = '',
        public products: OrderedProduct[] = []
    ) {  }
}