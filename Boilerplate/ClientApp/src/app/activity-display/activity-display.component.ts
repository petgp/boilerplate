import { Component, OnInit } from '@angular/core';
import { MessageService } from '../shared/message.service';
import * as $ from 'jquery';

@Component({
  selector: 'app-activity-display',
  templateUrl: './activity-display.component.html',
  styleUrls: ['./activity-display.component.css']
})
export class ActivityDisplayComponent implements OnInit {
  public messagesHidden = true;
  constructor(public messageService: MessageService) { }
  ngOnInit() {
    this.messageService.addDelegate(this);
  }
  userAccessedData() {
    this.updateView();
  }
  updateView() {
    if ($('#users').length) {
      $('.scrollable').animate({ scrollTop: $('.scrollable')[0].scrollHeight }, 300);
    }
  }
  displayMessages() {
    this.messagesHidden = !this.messagesHidden;
  }
  clearMessages() {
    this.messageService.clear();
  }
}
