import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { NotificationsFilter } from '../models/notifications/notifications-filter';
import { PaginatedList } from '../models/paginated-list';
import { Notification } from 'src/app/models/notifications/notification';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { AuthResult } from '../models/account/auth-result';
import { BehaviorSubject, ReplaySubject, map, take } from 'rxjs';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  baseUrl = environment.baseUrl + '/notifications';
  hubUrl = environment.hubUrl + '/notifications';
  private hubConnection: HubConnection | null = null;
  private notifications = new ReplaySubject<Notification>(0);
  notifiations$ = this.notifications.asObservable();

  private notificationsCount = new BehaviorSubject<number>(0);
  notificationsCount$ = this.notificationsCount.asObservable();

  constructor(private httpClient: HttpClient, private accountService: AccountService) {
  }

  get(filter: NotificationsFilter) {
    let params = new HttpParams();
    params = params.append('pageNumber', filter.pageNumber);
    params = params.append('pageSize', filter.pageSize);
    params = params.append('before', filter.before);

    return this.httpClient.get<PaginatedList<Notification>>(this.baseUrl, { params });
  }

  getUnReadNotificationsCount() {
    return this.httpClient.get<number>(this.baseUrl + '/unReadNotificationsCount').pipe(map(res => {
      this.notificationsCount.next(res);
    }));
  }

  markAsRead(id: number) {
    return this.httpClient.post(this.baseUrl + '/markAsRead', { id })
      .pipe(map(() => this.notificationsCount.next(this.notificationsCount.value - 1)));
  }

  startHubConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl, {
        accessTokenFactory: () => {
          let auth: string | undefined = undefined;
          this.accountService.currentAuth$.pipe(take(1)).subscribe(res => auth = res?.jwtToken);
          return auth!;
        }
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start();

    this.hubConnection.on('ServerNotification', notification => {
      this.notifications.next(notification);
      this.notificationsCount.next(this.notificationsCount.value + 1);
    });
  }

  closeHubConnection() {
    this.hubConnection?.stop();
  }

}
