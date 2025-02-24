import { Component } from '@angular/core';
import { AccountService } from '../services/account.service';
import { FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterUser } from '../models/register-user';
import { compareValidation } from '../validators/CustomValidation';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: FormGroup
  isRegisterFormSubmitted:boolean

  constructor(private accountService: AccountService, private router: Router)
  {
    this.isRegisterFormSubmitted = false;
    this.registerForm = new FormGroup(
      {
        personName: new FormControl(null, [Validators.required]),
        email: new FormControl(null, [Validators.required]),
        phoneNumber: new FormControl(null, [Validators.required]),
        password: new FormControl(null, [Validators.required]),
        confirmPassword: new FormControl(null, [Validators.required])
      },
      {
        validators: [compareValidation("password","confirmPassword")]
      })
  }

  get register_personName_Control(): any {
    return this.registerForm.controls["personName"];
  }

  get register_email_Control(): any {
    return this.registerForm.controls["email"];
  }

  get register_phoneNumber_Control(): any {
    return this.registerForm.controls["phoneNumber"];
  }

  get register_password_Control(): any {
    return this.registerForm.controls["password"];
  }

  get register_confirmPassword_Control(): any {
    return this.registerForm.controls["confirmPassword"];
  }

  CreateUserRegister(): RegisterUser {
   return    {
      email: this.registerForm.value.email,
      phoneNumber: this.registerForm.value.phoneNumber,
      personName: this.registerForm.value.personName,
      password: this.registerForm.value.password,
      confirmPassword: this.registerForm.value.confirmPassword
    };
  }
  registerSubmitted()
  {
    this.isRegisterFormSubmitted = true;

    if (this.registerForm.invalid) {
      return;
    }
    // Créer un objet RegisterDTO à partir des données du formulaire
    const registerUser = this.CreateUserRegister(); // Utilisation de la fonction

    this.accountService.postRegister(registerUser).subscribe(
      {
        next: (response: any) => {
          console.log(response);
          alert("User enregistré");
          localStorage["token"] = response.token;
          localStorage["refreshToken"] = response.refreshToken;
          this.isRegisterFormSubmitted = false;
          this.registerForm.reset();
          this.router.navigate(['/welcome']); 
        },
        error: (error: Error) => {
          alert(`Erreur survenue ${error.message}`);
          console.log(`${error.message}`);
        },
        complete: () => { }
      }
    );
  }
 
}
