import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-klassenviewpopup',
  templateUrl: './klassenviewpopup.component.html',
  styleUrls: ['./klassenviewpopup.component.scss']
})
export class KlassenviewpopupComponent implements OnInit {

  montag: boolean = false;
  constructor() { }

  ngOnInit(): void {
   

  }

  clickallesauswaehlen(): void 
  {
    var element = <HTMLInputElement> document.getElementById("montag");
    var element2 = <HTMLInputElement> document.getElementById("dienstag");
    var element3 = <HTMLInputElement> document.getElementById("mittwoch");


    if( element.checked == true && element2.checked == true && element3.checked == true )
    {
      element.checked= false;
      element2.checked= false;
      element3.checked= false;
    }
     else
     {
      element.checked= true;
      element2.checked= true;
      element3.checked= true;
     }
  }

}
