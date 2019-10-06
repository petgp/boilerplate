
import { Injectable, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { MessageService } from './message.service';
import { catchError, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { JwtHelper } from '../helper';

@Injectable({
  providedIn: 'root'
})

export class UserService {
  public isLoggedIn = false;
  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private messageService: MessageService,
    private jwt: JwtHelper,
    @Inject('BASE_URL') private baseUrl: string) {
    if (localStorage.getItem('token') !== null) {
      this.isLoggedIn = true;
    } else {
      this.isLoggedIn = false;
    }
  }
  // this is a form we built, it has the fields we need to send, with validators
  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', Validators.email],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })
  });

  // We made this method to compare passwords. We set errors if they dont match
  comparePasswords(fb: FormGroup) {
    const confirmPswrdCtrl = fb.get('ConfirmPassword');
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('Password').value !== confirmPswrdCtrl.value) {
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      } else {
        confirmPswrdCtrl.setErrors(null);
      }
    }
  }
  // sign up method
  register() {
    const body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      Password: this.formModel.value.Passwords.Password,
    };
    return this.http.post(this.createURL('api/ApplicationUser/Register'), body);

  }
  login(formData) {
    return this.http.post(this.createURL('api/ApplicationUser/Login'), formData);
  }
  logout() {
    this.messageService.log('User: ' + this.getUserId() + ' Logged Out');
    this.isLoggedIn = false;
    localStorage.removeItem('token');
  }
  getUserId(): string {
    if (localStorage.getItem('token') !== null) {
      const token = localStorage.getItem('token');
      return this.jwt.decodeToken(token);
    } else {
      return 'Fuck off';
    }
  }
  getUsers(): Observable<Users[]> {
    return this.http.get<Users[]>(this.createURL('api/ApplicationUser')).pipe(
      tap(_ => this.messageService.log('FetchedUsers')),
      catchError(this.messageService.handleError<Users[]>('getUsers', []))
    );
  }
  getSingleUser(id: string): Observable<Users> {
    return this.http.get<Users>(this.createURL('api/ApplicationUser/' + id)).pipe(
      tap(_ => this.messageService.log('FetchedUser ' + id)),
      catchError(this.messageService.handleError<Users>('getUser'))
    );
  }
  updateUser(user: Users): Observable<Users> {
    return this.http.post<Users>(this.createURL('api/ApplicationUser/update'), user).pipe(
      tap(_ => this.messageService.log('UpdatedUser ' + user.id)),
      catchError(this.messageService.handleError<Users>('updateUser'))
    );
  }
  createURL(url: string): string {
    return this.baseUrl + url;
  }
}
export interface Users {
  id: string;
  userName: string;
  email: string;
  emailConfirmed: boolean;
  passwordHash: string;
  securityStamp: string;
  concurrencyStamp: string;
  phoneNumber: string;
  phoneNumberConfirmed: boolean;
  twoFactorEnabled: boolean;
  lockoutEnd: string;
  lockoutEnabled: boolean;
  accessFailedCount: number;
}
