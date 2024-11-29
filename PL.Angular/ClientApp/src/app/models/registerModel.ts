export class RegisterModel {

    constructor(
        public email: string,
        public password: string,
        public name: string = '',
        public surName: string = '',
        public city: string = '',
        public postIndex: string = ''
    ) {  }

    isValid(): boolean {
        return !!this.email && !!this.password; 
      }
}