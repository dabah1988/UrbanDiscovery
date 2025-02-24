import { Component } from '@angular/core';
import { AccountService } from '../services/account.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginUser } from '../models/login-user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup
  isLoginFormFormSubmitted: boolean
  constructor(private accountService: AccountService, private router: Router) {
    this.isLoginFormFormSubmitted = false;
    this.loginForm = new FormGroup(
      {
        email: new FormControl(null, [Validators.required]),
        password : new FormControl(null, [Validators.required])
      });
  }

  get register_login_Control(): any {
    return this.loginForm.controls["email"];
  }

  get register_password_Control(): any {
    return this.loginForm.controls["password"];
  }

  CreateloginUser(): LoginUser {
    return {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password
    };
  }

  loginUserSubmitted()
  {
    this.isLoginFormFormSubmitted = true;
    if (this.loginForm.invalid) return;
    const loginUser = this.CreateloginUser();
    this.accountService.postLogin(loginUser).subscribe(
      {
        next: (response: any) => {
          console.info(response);
          this.accountService.currentUserName = response.email;
          localStorage["token"] = response.token;
          localStorage["refreshToken"] = response.refreshToken;
          localStorage["currentUserName"] = response.email;
          this.isLoginFormFormSubmitted = false;
          this.loginForm.reset();
          this.router.navigate(['/welcome']); 
        },
        error: (error: Error) => { console.log(error.message)},
        complete: () => { }
      });
  }

  logOut() {
    this.isLoginFormFormSubmitted = true;
    if (this.loginForm.invalid) return;
    const loginUser = this.CreateloginUser();
    this.accountService.postLogin(loginUser).subscribe(
      {
        next: (response: LoginUser) => {
          console.info(response);
          this.isLoginFormFormSubmitted = false;
          this.loginForm.reset();
          this.router.navigate(['/cities']);
        },
        error: (error: Error) => { console.log(error.message) },
        complete: () => { }
      });
  }

}
