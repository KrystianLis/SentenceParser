import { SentencePostResponse } from './../models/sentence-post-response';
import { SentenceGetResponse } from './../models/sentence-get-response';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SentenceRequest } from '../models/sentence-request';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SentenceService {

  constructor(private httpClient:HttpClient) { }

  baseUrl = 'http://localhost:4200/api/Sentence'

  postSentenceAsync(request:SentenceRequest):Observable<SentencePostResponse>{
    return this.httpClient.post<SentencePostResponse>(this.baseUrl, request);
  }

  getCsvAsync(request:SentencePostResponse):Observable<SentenceGetResponse>{
    return this.httpClient.get<SentenceGetResponse>(this.baseUrl + '/GetCsv/' + request?.id);
  }

  getXmlAsync(request:SentencePostResponse):Observable<SentenceGetResponse>{
    return this.httpClient.get<SentenceGetResponse>(this.baseUrl + '/GetXml/' + request?.id);
  }
}
