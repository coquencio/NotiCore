import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {

  isOpen: boolean = false;
  constructor() { }

  ngOnInit(): void {
  }

  showPrivacy(): void{
    this.isOpen = true;
  }
  closeModal(close: boolean){
    this.isOpen = close;
  }

}
