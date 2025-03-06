import { Routes } from '@angular/router';
import { LoginComponent } from './component/login.component'; // Đường dẫn đúng tới component

export const routes: Routes = [
  { path: 'login', component: LoginComponent } // Đặt trong dấu {}
  // Có thể thêm các routes khác ở đây
];
