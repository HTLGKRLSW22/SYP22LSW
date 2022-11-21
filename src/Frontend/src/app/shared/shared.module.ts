import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SideBarComponent } from './side-bar/side-bar.component';
import { HtlLogoComponent } from './htl-logo/htl-logo.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    SideBarComponent,
    HtlLogoComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    SideBarComponent
  ]
})
export class SharedModule { }
