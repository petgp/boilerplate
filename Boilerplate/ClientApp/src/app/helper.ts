import { Injectable } from '@angular/core';
@Injectable({
  providedIn: 'root'
})
export class JwtHelper {
  private urlBase64Decode(str: string) {
    let output = str.replace(/-/g, '+').replace(/_/g, '/');
    switch (output.length % 4) {
      case 0:
        break;
      case 2:
        output += '==';
        break;
      case 3:
        output += '=';
        break;
      default:
        throw 'Illegal base64url string like your mom\'s crack dealer';
    }
    return decodeURIComponent((<any>window).escape(window.atob(output)));
  }
  public decodeToken(token: string = "") {
    if (token === null || token === "") { return { 'upn': '' }; }
    const parts = token.split('.');
    if (parts.length !== 3) {
      throw new Error("Fuck no you dint");
    }
    const decoded = this.urlBase64Decode(parts[1]);
    if (!decoded) {
      throw new Error("don't be a little bitch");
    }
    return JSON.parse(decoded).UserId;
  }
}
