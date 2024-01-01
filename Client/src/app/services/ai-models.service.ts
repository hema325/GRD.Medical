import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { AiModelsResponse } from '../models/ai-models-response';

@Injectable({
  providedIn: 'root'
})
export class AiModelsService {

  baseUrl = environment.AIModelsBaseUrl;

  constructor(private httpClient: HttpClient) { }


  checkHeart(voice: File) {
    const fd = new FormData();
    fd.append('voice', voice);

    return this.httpClient.post<AiModelsResponse>(this.baseUrl + '/heartChecking', fd);
  }

  checkSkin(image: File) {
    const fd = new FormData();
    fd.append('image', image);

    return this.httpClient.post<AiModelsResponse>(this.baseUrl + '/skinChecking', fd);
  }

}
