import { Component } from '@angular/core';
import { UserService, Users } from '../shared/sample.service';

@Component({
  selector: 'app-sample-display',
  templateUrl: './sample-display.component.html',
  styleUrls: ['./sample-display.component.css']
})
export class SampleDisplayComponent {
  public users: Users[];
  constructor(private userService: UserService) {
    this.userService.getUsers().subscribe(users => this.users = users);
  }
}

