import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SideBarComponent } from './side-bar/side-bar.component';
import { HtlLogoComponent } from './htl-logo/htl-logo.component';



@NgModule({
  declarations: [
    SideBarComponent,
    HtlLogoComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    SideBarComponent
  ]
})
export class SharedModule { }
