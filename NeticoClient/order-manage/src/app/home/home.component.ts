import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../service/login.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  passShow!: boolean;
  username!: string;
  password!: string;
  constructor(private route: Router,private authen : LoginService,private toastr: ToastrService) {
    
  }
  async login(e:any) {
    if (this.username === '' || this.password === '') {
      alert('Không được bỏ trống tài khoản hoặc mật khẩu')
      return
    }
    else {
      const result = await this.authen.logIn(this.username, this.password);
      if(result) {
           this.route.navigate(['order']);
           this.toastr.success(`Hello, ${this.username}!`, 'Login success',{
            closeButton :true
          })
        }
        else {
          this.toastr.error('Đăng nhập thất bại', 'Login fail',{
            closeButton :true
          })
        }
    }
  }
  togglePasswordShow() {
    this.passShow = !this.passShow;
  }
}
