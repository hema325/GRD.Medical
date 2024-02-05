import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { AppointmentMessageFilter } from '../models/appointments/appointment-message-filter';
import { PaginatedList } from '../models/paginated-list';
import { AppointmentMessage } from '../models/appointments/appointment-message';
import { CreateAppointmentMessage } from '../models/appointments/create-appointment-message';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { AccountService } from './account.service';
import { ReplaySubject, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppointmentMessagesService {

  baseUrl = environment.baseUrl + '/appointmentMessages';
  hubUrl = environment.hubUrl + '/appointmentChat';
  private hubConnection: HubConnection | null = null;
  private newMessagesSource = new ReplaySubject<AppointmentMessage>(0);
  private writingStatusSource = new ReplaySubject<boolean>(0);
  newMessages$ = this.newMessagesSource.asObservable();
  writingStatus$ = this.writingStatusSource.asObservable();

  constructor(private httpClient: HttpClient,
    private accountService: AccountService) { }

  get(filter: AppointmentMessageFilter) {
    let params = new HttpParams();
    params = filter.appointmentId ? params.append('appointmentId', filter.appointmentId) : params;
    params = params.append('pageNumber', filter.pageNumber);
    params = params.append('pageSize', filter.pageSize);

    return this.httpClient.get<PaginatedList<AppointmentMessage>>(this.baseUrl, { params });
  }

  createMessage(message: CreateAppointmentMessage) {
    const fd = new FormData();
    fd.append('appointmentId', message.appointmentId.toString());
    fd.append('content', message.content);
    message.images?.forEach(f => fd.append('images', f));

    return this.httpClient.post<AppointmentMessage>(this.baseUrl, fd);
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

    this.hubConnection.on('ReceiveMessage', message => this.newMessagesSource.next(message));
    this.hubConnection.on('IsWriting', status => this.writingStatusSource.next(status));
  }

  notifyWritingStatus(to: number, status: boolean) {
    this.hubConnection?.invoke('NotifyWritingStatus', to, status);
  }

  closeHubConnection() {
    this.hubConnection?.stop();
  }
}
