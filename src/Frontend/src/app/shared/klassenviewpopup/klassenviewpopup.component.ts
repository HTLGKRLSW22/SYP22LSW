import { Component, OnInit, ViewChild } from '@angular/core';
import { CheckboxRequiredValidator } from '@angular/forms';

@Component({
  selector: 'app-klassenviewpopup',
  templateUrl: './klassenviewpopup.component.html',
  styleUrls: ['./klassenviewpopup.component.scss']
})
export class KlassenviewpopupComponent implements OnInit {

  monday = false;
  tuesday = false;
  wednesday = false;

  // constructor() { }

  ngOnInit(): void {
    console.log('classview popup works');
  }

  clickselectall(): void 
  {
    
    if(this.monday === false && this.tuesday === false && this.wednesday === false)
    {
      this.monday = true;
      this.tuesday = true;
      this.wednesday= true;
    }
    else
    {
      this.monday = false;
      this.tuesday = false;
      this.wednesday= false;
    }
  }

}
