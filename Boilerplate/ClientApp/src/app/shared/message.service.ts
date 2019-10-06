import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

class Message {
  public id: number;
  public message: string;
  public dateTime: string;
}
@Injectable({
  providedIn: 'root'
})
export class MessageService {
  constructor() { }
  private delegates: any[] = [];
  public messages: object[] = [];
  public addDelegate(delegate: any): void {
    this.delegates.push(delegate);
  }
  public log(message: string) {
    this.addMessage(`BoilerplateService: ${message}`);
  }
  private addMessage(message: string): void {
    this.messages.push(this.createMessage(message));
    if (this.delegates.length !== 0) {
      this.delegates.forEach(d => d.userAccessedData());
    }
  }
  public clear(): void {
    this.messages = [];
  }
  public handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      this.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }
  private createMessage(message: string): object {
    const tempMessage = new Message();
    tempMessage.id = this.messages.length;
    tempMessage.message = message;
    tempMessage.dateTime = new Date().toUTCString();
    return tempMessage;
  }
}
