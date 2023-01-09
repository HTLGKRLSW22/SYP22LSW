import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TeacherRoutingModule } from './teacher-routing.module';
import { TeacherViewComponent } from './teacher-view/teacher-view.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    TeacherViewComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    TeacherRoutingModule
  ]
})
export class TeacherModule { }
