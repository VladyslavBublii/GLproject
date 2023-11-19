import { Component } from '@angular/core';
import { LoginService } from './login.service';
import { StorageService } from '../storage/storage.service';
import { MatDialog } from '@angular/material/dialog';
//import { DialogAlertComponent } from "../dialog/alert-dialog/alert-dialog.component";
import { ErrorStateMatcher } from '@angular/material/core';
import { NgForm, FormControl, FormGroupDirective, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(
    private loginServise: LoginService, 
    private storageService: StorageService,
    public dialog: MatDialog) {}

  public Login = {} as Login;
  isLoggedIn = false;
  isBadRequest = false;

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

  signinto() {
      if(this.emailFormControl.status=="INVALID"){
        return;
      }

      if(this.pwdFormControl.status=="INVALID"){
        return;
      }

      this.loginServise.signinto(this.Login).subscribe(
      (res) => {
        this.storageService.saveUser(res);
        this.isLoggedIn = true;
        console.log('Answer:', res);
        this.loginServise.returnhome();
      },
      (error) => {
        console.error('Error:', error.error);
        this.isBadRequest = true;
        /*
        this.dialog.open(DialogAlertComponent, {
          width: '250px',
          data: {message: error.error }
        });
        */
      }
    );
  }

  onEnterEmail(event: Event){
    this.isBadRequest = false;
    this.Login.email = (<HTMLInputElement>event.target).value;
  }

  onEnterPassword(event: Event){
    this.isBadRequest = false;
    this.Login.password = (<HTMLInputElement>event.target).value;
  }
}

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

export interface Login {
  email: string;
  password: string;
}