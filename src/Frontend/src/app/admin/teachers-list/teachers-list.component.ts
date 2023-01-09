import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-teachers-list',
  templateUrl: './teachers-list.component.html',
  styleUrls: ['./teachers-list.component.scss']
})
export class TeachersListComponent implements OnInit {

  // constructor() { }

  ngOnInit(): void {
    console.log('teachers list works');
  }

}
