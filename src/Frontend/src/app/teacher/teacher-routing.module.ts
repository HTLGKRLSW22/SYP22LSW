import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TeacherViewComponent } from './teacher-view/teacher-view.component';

const routes: Routes = [
  { path: 'view', component: TeacherViewComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TeacherRoutingModule { }
