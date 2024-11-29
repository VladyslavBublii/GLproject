import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegisterService } from './register.service';
import { StorageService } from '../storage/storage.service';
import { LoginService } from '../login/login.service';
import { RegisterModel } from '../models/registerModel'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private registerService: RegisterService,
    private storageService: StorageService,
    private loginService: LoginService
  ) {
    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      name: [''],
      surName: [''],
      city: [''],
      postIndex: ['']
    });
  }

  registerinto() {
    if (this.registerForm.invalid) {
      return;
    }

    const registerData = new RegisterModel(
      this.registerForm.get('email')?.value ?? '',
      this.registerForm.get('password')?.value ?? '',
      this.registerForm.get('name')?.value ?? '',
      this.registerForm.get('surName')?.value ?? '',
      this.registerForm.get('city')?.value ?? '',
      this.registerForm.get('postIndex')?.value ?? ''
    );

    if (!registerData.isValid()) {
      console.error('Invalid registration data');
      return;
    }

    this.registerService.registerinto(registerData).subscribe(
      (res) => {
        const loginData = {
          id: "00000000-0000-0000-0000-000000000000",
          userRole: "user",
          email: registerData.email,
          passwordCache: registerData.password
        };

        this.loginService.signinto(loginData).subscribe(
          (resLog) => {
            this.storageService.saveUserData(resLog);
            this.loginService.returnhome();
          },
          (errorLog) => {
            console.error('Login error:', errorLog);
          }
        );
      },
      (error) => {
        console.error('Registration error:', error);
      }
    );
  }
}