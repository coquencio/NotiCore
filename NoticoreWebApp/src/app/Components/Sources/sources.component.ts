import { Component, OnInit } from '@angular/core';
import { ISourceInterface } from 'src/app/Interfaces/Responses/ISourceInterface';
import { SourcesServiceService } from 'src/app/Services/SourcesService/sources-service.service';

@Component({
  selector: 'app-sources',
  templateUrl: './sources.component.html',
  styleUrls: ['./sources.component.css']
})
export class SourcesComponent implements OnInit {
  sources: ISourceInterface[];
  query: string = '';
  loading: boolean = false;
  constructor(private sourcesService: SourcesServiceService) { }

  ngOnInit(): void {
    this.getSources();
  }

  getSources():void{
    this.loading = true;
    this.sourcesService.GetSources(this.query).subscribe(
      r=> {
        this.sources = r.data;
        this.loading = false;
      }
      );
  }

}
