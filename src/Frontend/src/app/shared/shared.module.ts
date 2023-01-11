import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SideBarComponent } from './side-bar/side-bar.component';
import { HtlLogoComponent } from './htl-logo/htl-logo.component';
import { RouterModule } from '@angular/router';
import { KlassenviewpopupComponent } from './klassenviewpopup/klassenviewpopup.component';
import { CourseComponent } from './course/course.component';
import { ShowTeachersPipe } from './pipes/show-teachers.pipe';
import { ShowTimesForDatesPipe } from './pipes/show-times-for-dates.pipe';



@NgModule({
  declarations: [
    SideBarComponent,
    HtlLogoComponent,
    KlassenviewpopupComponent,
    CourseComponent,
    ShowTeachersPipe,
    ShowTimesForDatesPipe,
  ],
  imports: [
    CommonModule,
    RouterModule,
  ],
  exports: [
    KlassenviewpopupComponent,
    SideBarComponent,
    CourseComponent,
    ShowTeachersPipe,
    ShowTimesForDatesPipe,
  ],
})
export class SharedModule { }
