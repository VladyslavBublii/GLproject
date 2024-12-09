export class OrderRequestModel {
    constructor(
        public userId: string = '',
        public productId: string = '',
        public count: number = 0,
    ) { }
}