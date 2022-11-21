import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FeatureLazyRoutingModule } from './feature-lazy-routing.module';
import { AdminCoursesListComponent } from './admin-courses-list/admin-courses-list.component';


@NgModule({
  declarations: [
    AdminCoursesListComponent
  ],
  imports: [
    CommonModule,
    FeatureLazyRoutingModule
  ]
})
export class FeatureLazyModule { }
