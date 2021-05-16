import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';import { ISubscriberRequest } from 'src/app/Interfaces/Requests/ISubscriberRequestInterface';
import { Observable } from 'rxjs';
import { IBaseResponseInterface } from 'src/app/Interfaces/Responses/IResponseInterface';
import { AppSettings } from 'src/app/Constansts';


@Injectable({
  providedIn: 'root'
})
export class SubscriberServiceService {

  constructor(private httpClient: HttpClient) { }

  SubmitSubscriber(request : ISubscriberRequest): Observable<IBaseResponseInterface<string>>{
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');
    return this.httpClient.post<IBaseResponseInterface<string>>(AppSettings.BASE_ADDRESS + 'api/Workflow/Enroll', request, {headers, responseType:'json'});
  }
}
