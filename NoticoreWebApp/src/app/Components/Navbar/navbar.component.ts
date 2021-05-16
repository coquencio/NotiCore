import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  toggled: boolean = false;
  constructor() { }
  imagePath : string = 'assets/img/blackIcon.png';
  ngOnInit(): void {
  }
  displayNavbar(){
    this.toggled = !this.toggled;
  }
}
