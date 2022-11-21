import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-htl-logo',
  templateUrl: './htl-logo.component.html',
  styleUrls: ['./htl-logo.component.scss']
})
export class HtlLogoComponent implements OnInit {

    @Input() size = '20px';

  ngOnInit(): void {}

}
