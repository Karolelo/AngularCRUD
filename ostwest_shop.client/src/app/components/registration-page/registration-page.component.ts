import { Component,inject } from '@angular/core';
import { FormGroup, AbstractControl, ValidationErrors, ValidatorFn, FormControl, Validators } from '@angular/forms';
import {UserService} from '../../services/UserService/user.service';
import { Router } from '@angular/router';
@Component({
  standalone: false,
  selector: 'app-registration-page',
  templateUrl: './registration-page.component.html',
  styleUrls: ['./registration-page.component.css']
})
export class RegistrationPageComponent {
  registrationForm: FormGroup;
  router= inject(Router);
  constructor(private userService: UserService) {
    this.registrationForm = new FormGroup(
      {
        login: new FormControl('', [Validators.required]),
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [
          Validators.required,
          Validators.minLength(6),
        ]),
        confirmPassword: new FormControl('', [Validators.required]),
        name: new FormControl('', [Validators.required]),
        surname: new FormControl('', [Validators.required]),
        birthDate: new FormControl('', [Validators.required]),
      },
      { validators: this.passwordsMatchValidator() }
    );
  }
  passwordsMatchValidator(): ValidatorFn {
    return (formGroup: AbstractControl): ValidationErrors | null => {
      const password = formGroup.get('password')?.value;
      const confirmPassword = formGroup.get('confirmPassword')?.value;

      if (password !== confirmPassword) {
        return {passwordsMismatch: true};
      }
      return null;
    };
  }
  onSubmit(): void {
    const user = {
      userName: this.registrationForm.value.login,
      password: this.registrationForm.value.password,
      email: this.registrationForm.value.email,
      name: this.registrationForm.value.name,
      surname: this.registrationForm.value.surname,
      birthdDate: this.registrationForm.value.birthdDate
    };

    this.userService.register(user).subscribe({
      next:(response)=> {console.log('Pomyślnie stworzono nowe konto!',response);
        this.router.navigate(['login']);
        },
      error:(err) =>{console.log('Wystapił błąd podczas rejestracji:',err.error.message.replace('Exception: ', ''))}
    })
  }
}
