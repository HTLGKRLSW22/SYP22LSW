import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-upload-drag-and-drop',
  templateUrl: './upload-drag-and-drop.component.html',
  styleUrls: ['./upload-drag-and-drop.component.scss']
})
export class UploadDragAndDropComponent implements OnInit {

  // constructor() { }

  ngOnInit(): void {
    console.log('Drag and drop works');
  }

}
