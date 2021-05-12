import { Component, OnInit } from '@angular/core';
import { IWallets } from 'src/app/Interfaces/Responses/IWalletResponse';
import { WalletServiceService } from 'src/app/Services/WalletService/wallet-service.service';

@Component({
  selector: 'app-donate',
  templateUrl: './donate.component.html',
  styleUrls: ['./donate.component.css']
})
export class DonateComponent implements OnInit {

  constructor(private walletService: WalletServiceService) { }
  wallets: IWallets;
  loading : boolean = false;
  errorMessage: string = '';
  
  ngOnInit(): void {
    this.getWallets();
  }

  getWallets(){
    this.loading = true;
    this.walletService.GetWallets().subscribe(
      r=> this.wallets = r.data,
      e=> this.errorMessage = e.error.errors.join("\n"),
      () => this.loading = false
    )
  }

}
