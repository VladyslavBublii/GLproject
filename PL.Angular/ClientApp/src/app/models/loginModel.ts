export class LoginModel {

    constructor(
        public email: string = '',
        public passwordCache: string = '',
        public id: string = "00000000-0000-0000-0000-000000000000",
        public userRole: string = "user",
    ) {  }

    isValid(): boolean {
        return !!this.email && !!this.passwordCache; 
      }
}