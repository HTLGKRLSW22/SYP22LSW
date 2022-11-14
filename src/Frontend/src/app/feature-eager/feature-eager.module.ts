import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FeatureEagerRoutingModule } from './feature-eager-routing.module';
import { LoginComponent } from './login/login.component';


@NgModule({
  declarations: [
    LoginComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    FeatureEagerRoutingModule,
  ]
})
export class FeatureEagerModule { }
