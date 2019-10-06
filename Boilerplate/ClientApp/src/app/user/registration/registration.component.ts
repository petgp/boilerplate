import { Component } from '@angular/core';
import { UserService } from '../../shared/sample.service';
import { Router } from '@angular/router';
import { MessageService } from '../../shared/message.service';
@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {

  constructor(public service: UserService, private router: Router, private messageService: MessageService) { }
  onSubmit(form) {
    const user = {
      Email: form.value.Email,
      Password: form.value.Passwords.Password
    };
    this.service.register().subscribe(
      (res: any) => {
        if (res.succeeded) {
          this.service.formModel.reset();
          this.messageService.log('UserCreated - Registration successful');
          this.service.isLoggedIn = true;
          this.login(user);
        } else {
          res.errors.forEach(element => {
            this.service.isLoggedIn = false;
            switch (element.code) {
              case 'DuplicateUserName':
                console.log('Username is already taken', 'Registration failed');
                // Username already taken
                break;
              default:
                console.log(element.description, 'Registration failed');
                // registration failed
                break;
            }
          });
        }
      },
      err => {
        console.log(err);
        this.messageService.handleError('UserCreate', err);
      }
    );
  }
  login(data) {
    this.service.login(data).subscribe(
      (res: any) => {
        this.messageService.log('UserLogin - Registration successful');
        localStorage.setItem('token', res.token);
        this.service.isLoggedIn = true;
        this.router.navigateByUrl('/home');
      },
      err => {
        this.service.isLoggedIn = false;
        if (err.status === 400) {
          console.log('Incorrect email or password', 'Authentication failed');
        } else {
          console.log(err);
          this.messageService.handleError('UserLogin', err);
        }
      }
    );
  }
}
