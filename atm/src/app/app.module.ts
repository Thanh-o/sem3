import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';  // Đảm bảo đã nhập FormsModule
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { LoginComponent } from './component/login.component';
import { RegisterComponent } from './component/register.component';
import { ChangePasswordComponent } from './component/change-password.component';
import { AuthService } from './component/auth.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    ChangePasswordComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,          // Đảm bảo nhập ở đây
    HttpClientModule,
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
