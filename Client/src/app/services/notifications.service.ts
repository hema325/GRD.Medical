import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { NotificationsFilter } from '../models/notifications/notifications-filter';
import { PaginatedList } from '../models/paginated-list';
import { Notification } from 'src/app/models/notifications/notification';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { AccountService } from './account.service';
import { AuthResult } from '../models/account/auth-result';
import { BehaviorSubject, ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  baseUrl = environment.baseUrl + '/notifications';
  hubUrl = environment.hubUrl + '/notifications';
  private hubConnection: HubConnection | null = null;
  private notifications = new ReplaySubject<Notification>(0);
  notifiations$ = this.notifications.asObservable();
  constructor(private httpClient: HttpClient) {
  }

  get(filter: NotificationsFilter) {
    let params = new HttpParams();
    params = params.append('pageNumber', filter.pageNumber);
    params = params.append('pageSize', filter.pageSize);

    return this.httpClient.get<PaginatedList<Notification>>(this.baseUrl, { params });
  }

  markAsRead(id: number) {
    return this.httpClient.post(this.baseUrl + '/markAsRead', { id });
  }

  openHubConnection(auth: AuthResult) {

    if (this.hubConnection)
      this.closeHubConnection();

    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl, {
        accessTokenFactory: () => auth.jwtToken
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start();

    this.hubConnection.on('ServerNotification', notification => {
      this.notifications.next(notification);
    });
  }

  closeHubConnection() {
    if (this.hubConnection) {
      this.hubConnection.stop();
    }
  }

}
