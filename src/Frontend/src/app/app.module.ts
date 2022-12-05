import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ApiModule, BASE_PATH } from './swagger';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FeatureEagerModule } from './feature-eager/feature-eager.module';
import { environment } from 'src/environments/environment';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ApiModule,
    SharedModule,
    FeatureEagerModule,
    AppRoutingModule,
  ],
  providers: [
    {provide: BASE_PATH, useValue: environment.apiRoot}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
