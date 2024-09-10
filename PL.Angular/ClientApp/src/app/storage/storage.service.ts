// src/app/services/storage.service.ts
import { Injectable } from '@angular/core';
import { OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import * as userActions from '../user/user.actions';
import * as userSelectors from '../user/user.selectors';
import { UserRole } from '../models/enums/user-role.enum';

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
    this.store.dispatch(userActions.setUser({ userId: '', userEmail: '', userRole: '' }));
    this.store.dispatch(userActions.getUser());
  }

  public saveUserData(login: any): void {
    login.role = parseInt(login.userRole);
    this.store.dispatch(userActions.setUser({ userId: login.id, userEmail: login.email, userRole:  login.role }));
    this.store.dispatch(userActions.getUser());
  }

  public getUserId(): string {
    let userId: string = '';
    this.user$.subscribe(user => {
      userId = user.id;
    }).unsubscribe(); 

    return userId;
  }

  public getUserRole(): UserRole | null {
    let userRole: UserRole | null = null;
    this.user$.subscribe(user => {
      userRole = user.role as unknown as UserRole;
    }).unsubscribe(); 

    return userRole;
  }

  public isLoggedIn(): boolean {
    this.user$.subscribe(
      (user) => { 
      });
    let isLogged = false;
    this.user$.subscribe(user => {
      isLogged = user.email !== '';
    }).unsubscribe(); 
    return isLogged;
  }
}