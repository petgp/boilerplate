import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from "../../shared/sample.service";
import { Router } from '@angular/router';
import { MessageService } from '../../shared/message.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {
  formModel = {
    Email: '',
    Password: ''
  };
  constructor(private service: UserService, private router: Router, private messageService: MessageService) { }
  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      this.router.navigateByUrl('/login');
    }
  }
  onSubmit(form: NgForm) {
    this.service.login(form.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
        this.messageService.log('UserLogin: ' + this.service.getUserId() + ' - Registration successful');
        this.service.isLoggedIn = true;
        this.router.navigateByUrl('/sample');
      },
      err => {
        this.service.isLoggedIn = false;
        if (err.status === 400) {
          console.log('Incorrect username or password', 'Authentication failed');
        } else {
          console.log(err);
          this.messageService.handleError('UserLogin', err);
        }
      }
    );
  }
}
