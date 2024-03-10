import { Injectable } from '@angular/core';
import { OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import * as userActions from '../user/user.actions';
import * as userSelectors from '../user/user.selectors';

const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root'
})
export class StorageService implements OnInit {
  user$ = this.store.select(userSelectors.selectUser);

  constructor(private store: Store) {}

  ngOnInit() {
    this.store.dispatch(userActions.getUser());
  }

  clean(): void {
    this.store.dispatch(userActions.setUser({ userId: "", userEmail: "" }));
    this.store.dispatch(userActions.getUser());
  }

  public saveUserData(login: any): void {
    // Вызываем экшен setUser при инициализации компонента
    this.store.dispatch(userActions.setUser({ userId: login.id, userEmail: login.email }));
     // Вызываем экшен getUser, который не изменяет состояние, но инициирует сохранение в localStorage
    this.store.dispatch(userActions.getUser());
  }

  public getUserId(): string {
    let userId: string = "";
    this.user$.subscribe(
      (user) => { 
        userId = user.id;
      });

    return userId;
  }

  public isLoggedIn(): boolean {
    this.user$.subscribe(
      (user) => { 
        console.log('User Email:', user.email);
        console.log('User Id:', user.id);
      });

    var isLogged = false;
    this.user$.subscribe(
      (user) => {
        if (user.email != '') {
          isLogged = true;
        }
      });

    return isLogged;
  }
}