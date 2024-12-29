import { Component, OnInit } from '@angular/core';
import { LoginService } from './login.service';
import { StorageService } from '../storage/storage.service';
import { MatDialog } from '@angular/material/dialog';
import { ErrorStateMatcher } from '@angular/material/core';
import { FormControl, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { LoginModel } from '../models/loginModel'

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    standalone: false
})
export class LoginComponent implements OnInit {
  public Login: LoginModel = new LoginModel();
  isLoggedIn = false;
  isBadRequest = false;

  constructor(
    private loginService: LoginService, 
    private storageService: StorageService,
    public dialog: MatDialog) {}

  ngOnInit(): void {
    if (this.storageService.isLoggedIn()) {
      this.isLoggedIn = true;
    }
  }

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);

  pwdFormControl = new FormControl('', [
    Validators.required,
  ]);  

  matcher = new MyErrorStateMatcher();

  signinto(): void {
    if (this.emailFormControl.invalid || this.pwdFormControl.invalid) {
      return;
    }

    this.Login.email = this.emailFormControl.value ?? ' ';
    this.Login.passwordCache = this.pwdFormControl.value ?? ' ';

    if (!this.Login.isValid()) {
      console.error('Invalid login data');
      return;
    }

    this.loginService.signinto(this.Login).subscribe({
      next: (res) => {
        this.storageService.saveUserData(res);
        this.isLoggedIn = true;
        this.loginService.returnhome();
      },
      error: (error) => {
        console.error('Error:', error);
        this.isBadRequest = true;
      }
    });
  }

  onEnterEmail(event: Event): void {
    this.isBadRequest = false;
    this.emailFormControl.setValue((<HTMLInputElement>event.target).value);
  }

  onEnterPassword(event: Event): void {
    this.isBadRequest = false;
    this.pwdFormControl.setValue((<HTMLInputElement>event.target).value);
  }
}

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}