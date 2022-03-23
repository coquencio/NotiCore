import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-info-box',
  templateUrl: './info-box.component.html',
  styleUrls: ['./info-box.component.css']
})
export class InfoBoxComponent implements OnInit {

  constructor() { }
  @Input () Title : string;
  @Input () Text : string;  
  @Input () IconClass : string = '';


  ngOnInit(): void {
  }

}
