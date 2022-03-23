import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-privacy-modal',
  templateUrl: './privacy-modal.component.html',
  styleUrls: ['./privacy-modal.component.css']
})
export class PrivacyModalComponent implements OnInit {

  constructor() { }
  @Input() isOpen: boolean = false; 
  @Output() closeModalEmitter = new EventEmitter<boolean>();
  
  ngOnInit(): void {
  }

  closeModal(): void{
    this.isOpen = false;
    this.closeModalEmitter.emit(false);
  }

}
