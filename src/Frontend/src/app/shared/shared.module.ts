import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SideBarComponent } from './side-bar/side-bar.component';
import { HtlLogoComponent } from './htl-logo/htl-logo.component';
import { RouterModule } from '@angular/router';
import { KlassenviewpopupComponent } from './klassenviewpopup/klassenviewpopup.component';



@NgModule({
  declarations: [
    SideBarComponent,
    HtlLogoComponent,
    KlassenviewpopupComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    SideBarComponent, KlassenviewpopupComponent
  ]
})
export class SharedModule { }
