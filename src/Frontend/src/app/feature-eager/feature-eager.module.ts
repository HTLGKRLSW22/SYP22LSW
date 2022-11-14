import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FeatureEagerRoutingModule } from './feature-eager-routing.module';
import { LoginComponent } from './login/login.component';


@NgModule({
  declarations: [
    LoginComponent,
  ],
  imports: [
    CommonModule,
    FeatureEagerRoutingModule,
  ]
})
export class FeatureEagerModule { }
