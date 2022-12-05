import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CourseComponent } from './course/course.component';
import { ShowTeachersPipe } from './pipes/show-teachers.pipe';
import { ShowTimesForDatesPipe } from './pipes/show-times-for-dates.pipe';



@NgModule({
  declarations: [
    CourseComponent,
    ShowTeachersPipe,
    ShowTimesForDatesPipe,
  ],
  imports: [
    CommonModule,
  ],
  exports: [
    CourseComponent,
    ShowTeachersPipe,
    ShowTimesForDatesPipe,
  ],
})
export class SharedModule { }
