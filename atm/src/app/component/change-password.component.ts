// change-password.component.ts
import { Component } from '@angular/core';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent {
  customerId: number = 0;
  oldPassword: string = '';
  newPassword: string = '';
  message: string = '';

  constructor(private authService: AuthService) {}

  onChangePassword(): void {
    this.authService.changePassword({ customerId: this.customerId, oldPassword: this.oldPassword, newPassword: this.newPassword })
      .subscribe(
        response => this.message = response.Message,
        error => this.message = error.error
      );
  }
}
