// register.component.ts
import { Component } from '@angular/core';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  name: string = '';
  email: string = '';
  password: string = '';
  message: string = '';

  constructor(private authService: AuthService) {}

  onRegister(): void {
    this.authService.register({ name: this.name, email: this.email, password: this.password })
      .subscribe(
        response => this.message = response.Message,
        error => this.message = error.error
      );
  }
}
