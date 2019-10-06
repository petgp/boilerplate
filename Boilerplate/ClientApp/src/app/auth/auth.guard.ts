import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

// Injectable means we can use it to inject other components
@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {

  // create variables using dependency injection
  constructor(private router: Router) {}
  // This is so the JWT add-on can work to sign someone in
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    if (localStorage.getItem('token') != null) {

      return true;
    } else {
      // in [] form because it is 'navigate'. I think navigateByUrl you only need a string
      this.router.navigate(['/login']);
      return false;
    }
  }
}
