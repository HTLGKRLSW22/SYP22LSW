import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { CoursesListComponent } from './courses-list/courses-list.component';
import { NewCourseComponent } from './new-course/new-course.component';
import { UploadDragAndDropComponent } from './upload-drag-and-drop/upload-drag-and-drop.component';
import { StudentsListComponent } from './students-list/students-list.component';
import { TeachersListComponent } from './teachers-list/teachers-list.component';


@NgModule({
  declarations: [
    CoursesListComponent,
    NewCourseComponent,
    UploadDragAndDropComponent,
    StudentsListComponent,
    TeachersListComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule
  ]
})
export class AdminModule { }
