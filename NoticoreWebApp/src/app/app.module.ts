import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './Components/navbar/navbar.component';
import { FooterComponent } from './Components/Footer/footer.component';
import { RouterModule } from '@angular/router';
import { IndexComponent } from './Components/index/index.component';
import { InfoBoxComponent } from './Components/Nested/info-box/info-box.component';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LoadingComponent } from './Components/Nested/Loading/loading.component' 
import { SourcesComponent } from './Components/Sources/sources.component';
import { DonateComponent } from './Components/Donate/donate.component';
import { QRCodeModule } from 'angularx-qrcode';
import { WalletComponent } from './Components/Nested/Wallet/wallet.component';
import { ClipboardModule } from 'ngx-clipboard';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    IndexComponent,
    InfoBoxComponent,
    LoadingComponent,
    SourcesComponent,
    DonateComponent,
    WalletComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot([
      {path:'Index', component: IndexComponent},
      {path:'Sources', component: SourcesComponent},
      {path:'Donate', component: DonateComponent},
      {path: '**', redirectTo: '/Index'},
      {path: '', redirectTo: '/Index', pathMatch: 'full'}
    ]),
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    QRCodeModule,
    ClipboardModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
