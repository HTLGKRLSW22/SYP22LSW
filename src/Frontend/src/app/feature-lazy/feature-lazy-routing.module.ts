import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminCoursesListComponent } from './admin-courses-list/admin-courses-list.component';

const routes: Routes = [
  {
    path: '',
    children: [
      { path: 'courses-list', component: AdminCoursesListComponent},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FeatureLazyRoutingModule { }
