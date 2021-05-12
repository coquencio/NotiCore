import { Component, Input, OnInit } from '@angular/core';
import { ClipboardService } from 'ngx-clipboard';

@Component({
  selector: 'app-wallet',
  templateUrl: './wallet.component.html',
  styleUrls: ['./wallet.component.css']
})
export class WalletComponent implements OnInit {

  constructor(private clipboardService: ClipboardService) { }

  @Input() coin : string;
  @Input() address : string;  

  ngOnInit(): void {
  }

  copy(){
    this.clipboardService.copyFromContent(this.address);
  }

}
