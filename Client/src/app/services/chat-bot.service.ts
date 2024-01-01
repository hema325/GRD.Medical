import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { PaginatedList } from '../models/paginated-list';
import { UserChatBotMessage } from '../models/chat-bot/user-chat-bot-message';
import { GenerateUserChatBotMessageResponse } from '../models/chat-bot/generate-user-chat-bot-message-response';

@Injectable({
  providedIn: 'root'
})
export class ChatBotService {

  baseUrl = environment.baseUrl + '/chatBot';

  constructor(private httpClient: HttpClient) { }

  generateResponse(message: string) {
    return this.httpClient.post<GenerateUserChatBotMessageResponse>(this.baseUrl, { message });
  }

  getMessages(pageNumber: number, pageSize: number) {
    let params = new HttpParams();
    params = params.append('pageNumber', pageNumber);
    params = params.append('pageSize', pageSize);

    return this.httpClient.get<PaginatedList<UserChatBotMessage>>(this.baseUrl, { params });
  }

}
