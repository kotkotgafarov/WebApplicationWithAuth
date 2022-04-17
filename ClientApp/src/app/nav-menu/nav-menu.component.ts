import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { LKAUser } from '../models/lkauser';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
  providers: [DataService]
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isModerator: boolean = false;

  constructor(private dataService: DataService) { }

  ngOnInit() {
    this.dataService.getCurrentUser()
      .subscribe((data: LKAUser) => this.processDataReceived(data));
  }

  processDataReceived(data: LKAUser) {
    this.isModerator = data.isModerator
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
