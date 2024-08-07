import { UserRole } from './enums/user-role.enum';

export class User {

    constructor(
      public name: string,
      role: UserRole
    ) {  } 
}