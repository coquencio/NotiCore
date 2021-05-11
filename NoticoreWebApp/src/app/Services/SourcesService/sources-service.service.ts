import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppSettings } from 'src/app/Constansts';
import { IBaseResponseInterface } from 'src/app/Interfaces/Responses/IResponseInterface';
import { ISourceInterface } from 'src/app/Interfaces/Responses/ISourceInterface';

@Injectable({
  providedIn: 'root'
})
export class SourcesServiceService {

  constructor(private httpClient: HttpClient) { }

  GetSources(query: string = ''): Observable<IBaseResponseInterface<ISourceInterface[]>>{
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');
    return this.httpClient.get<IBaseResponseInterface<ISourceInterface[]>>(AppSettings.BASE_ADDRESS + 'api/Sources?query=' + query, {headers, responseType:'json'});
  
  }
}
