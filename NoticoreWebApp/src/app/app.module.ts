import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './Components/navbar/navbar.component';
import { FooterComponent } from './Components/Footer/footer.component';
import { RouterModule } from '@angular/router';
import { IndexComponent } from './Components/index/index.component';
import { InfoBoxComponent } from './Components/Nested/info-box/info-box.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    IndexComponent,
    InfoBoxComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot([
      {path:'Index', component: IndexComponent},
      {path: '**', redirectTo: '/Index'},
      {path: '', redirectTo: '/Index', pathMatch: 'full'}
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
