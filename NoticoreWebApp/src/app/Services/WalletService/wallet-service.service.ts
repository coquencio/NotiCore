import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppSettings } from 'src/app/Constansts';
import { IBaseResponseInterface } from 'src/app/Interfaces/Responses/IResponseInterface';
import { IWallets } from 'src/app/Interfaces/Responses/IWalletResponse';

@Injectable({
  providedIn: 'root'
})
export class WalletServiceService {

  constructor(private client: HttpClient) { }

  GetWallets() : Observable<IBaseResponseInterface<IWallets>>{
    return this.client.get<IBaseResponseInterface<IWallets>>(AppSettings.BASE_ADDRESS + 'api/Wallets', {});
  }
}
