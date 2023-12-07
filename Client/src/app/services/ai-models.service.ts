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


  checkHeart(data: any) {
    return this.httpClient.post<AiModelsResponse>(this.baseUrl + '/heartChecking', data);
  }

}
