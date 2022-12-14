import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ApiModule, BASE_PATH } from './swagger';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { environment } from 'src/environments/environment';
import { SharedModule } from './shared/shared.module';
import { LoginModule } from './login/login.module';
import { StudentModule } from './student/student.module';
import { TeacherModule } from './teacher/teacher.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ApiModule,
    SharedModule,
    LoginModule,
    
    StudentModule,
    TeacherModule,
    AppRoutingModule,
  ],
  providers: [
    { provide: BASE_PATH, useValue: environment.apiRoot }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
