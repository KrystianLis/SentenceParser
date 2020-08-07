import { SentenceGetResponse } from './models/sentence-get-response';
import { SentenceService } from './services/sentence.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'client';

  result:SentenceGetResponse={ value:'' };
  inputValue:string;

  constructor(private sentenceService:SentenceService){}

  OnPostGetXmlAsync():void{
    this.sentenceService.postSentenceAsync({Value:this.inputValue}).subscribe((response) => {
        this.sentenceService.getXmlAsync(response).subscribe(response => {
        this.result.value = response.value
      }, error => console.log(error))
    });
  }  

  OnPostGetCsvAsync():void{
    this.sentenceService.postSentenceAsync({Value:this.inputValue}).subscribe((response) => {
        this.sentenceService.getCsvAsync(response).subscribe(response => {
        this.result.value = response.value
      }, error => console.log(error));
    });
  } 
}
