import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ApiModule, BASE_PATH } from './swagger';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ApiModule,
    AppRoutingModule,
  ],
  providers: [
    {provide: BASE_PATH, useValue: environment.apiRoot}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
