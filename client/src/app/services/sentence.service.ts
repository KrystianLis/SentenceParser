import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SentenceRequest } from '../models/sentence-request';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SentenceService {

  constructor(private httpClient:HttpClient) { }

  PostSentenceAsync(request:SentenceRequest):Observable<any>{
    return this.httpClient.post<any>('http://localhost:4200/api/Sentence', request);
  }
}
