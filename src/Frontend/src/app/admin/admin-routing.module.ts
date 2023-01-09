import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {CoursesListComponent} from './courses-list/courses-list.component';
import { NewCourseComponent } from './new-course/new-course.component';
import { StudentsListComponent } from './students-list/students-list.component';
import { TeachersListComponent } from './teachers-list/teachers-list.component';
import { UploadDragAndDropComponent } from './upload-drag-and-drop/upload-drag-and-drop.component';

const routes: Routes = [
  { path: 'courses-list', component: CoursesListComponent},
  { path: 'new-course', component: NewCourseComponent},
  { path: 'upload-drag-and-drop', component: UploadDragAndDropComponent},
  { path: 'students-view', component: StudentsListComponent},
  { path: 'teachers-view', component: TeachersListComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
