import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public constructor(private titleService: Title){}
  title = 'Noticore';
  ngOnInit(){
    this.titleService.setTitle(this.title);
  }
}
