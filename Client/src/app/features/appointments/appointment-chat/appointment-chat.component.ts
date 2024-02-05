import { Component, ElementRef, Renderer2, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, finalize, throwError } from 'rxjs';
import { AuthResult } from 'src/app/models/account/auth-result';
import { Roles } from 'src/app/models/account/roles.enum';
import { Appointment } from 'src/app/models/appointments/appointment';
import { AppointmentMessage } from 'src/app/models/appointments/appointment-message';
import { AppointmentMessageFilter } from 'src/app/models/appointments/appointment-message-filter';
import { CreateAppointmentMessage } from 'src/app/models/appointments/create-appointment-message';
import { MediaTypes } from 'src/app/models/media-types.enum';
import { PaginatedList } from 'src/app/models/paginated-list';
import { AccountService } from 'src/app/services/account.service';
import { AppointmentMessagesService } from 'src/app/services/appointment-messages.service';
import { AppointmentsService } from 'src/app/services/appointments.service';
import { LoaderService } from 'src/app/services/loader.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-appointment-chat',
  templateUrl: './appointment-chat.component.html',
  styleUrls: ['./appointment-chat.component.css']
})
export class AppointmentChatComponent {

  defaultUserImageUrl = environment.defaultUserImageUrl;
  messages: PaginatedList<AppointmentMessage> | null = null;
  currentAuth: AuthResult | null = null;
  appointment?: Appointment;
  isChatAvailable: boolean = true;

  constructor(private appointmentMessagesService: AppointmentMessagesService,
    private activatedRoute: ActivatedRoute,
    private accountService: AccountService,
    private renderer: Renderer2,
    private loaderService: LoaderService,
    private router: Router,
    private appointmentsService: AppointmentsService) {
    const state = router.getCurrentNavigation()?.extras.state as Appointment;

    if (state)
      this.appointment = state;
  }

  filter: AppointmentMessageFilter = {
    appointmentId: 0,
    pageNumber: 1,
    pageSize: 20
  }

  isChatStarted() {
    if (this.appointment)
      return new Date() >= new Date(this.appointment.startDateTime);

    return false;
  }

  isWriting = false;
  activeScrollToBottom = false;
  isFirstScroll = true;
  isFirstLoading = true;
  isScrollBottomBtnActive = false;

  ngOnInit() {
    this.loadCurrentAuth();
    this.appointmentMessagesService.startHubConnection();
    this.setAppointmentId();
    this.loadMessages();
    this.loadAppointment();
    this.pushNewMessages();
    this.updateWritingStatus();
  }

  updateWritingStatus() {
    this.appointmentMessagesService.writingStatus$.subscribe(status => {
      this.isWriting = status;
      this.activeScrollToBottom = true;
    });
  }

  prevNotifiedStatus = false;
  notifyWritingStatus(status: boolean) {
    if (this.prevNotifiedStatus != status) {
      this.prevNotifiedStatus = status;
      this.appointmentMessagesService.notifyWritingStatus(this.getOtherUser()!.id, status);
    }
  }

  pushNewMessages() {
    this.appointmentMessagesService.newMessages$
      .subscribe(msg => {
        this.messages?.data.push(msg);
        this.activeScrollToBottom = true;
      });
  }

  loadAppointment() {
    const id = Number(this.activatedRoute.snapshot.queryParamMap.get('appontId'));
    this.appointmentsService.getById(id).subscribe(res => this.appointment = res);
  }

  getOtherUser() {
    if (this.appointment?.doctor)
      return this.appointment?.doctor;
    else if (this.appointment?.patient)
      return this.appointment?.patient;

    return null;
  }

  loadCurrentAuth() {
    this.accountService.currentAuth$.subscribe(auth => this.currentAuth = auth);
  }

  setAppointmentId() {
    this.filter.appointmentId = Number(this.activatedRoute.snapshot.queryParamMap.get('appontId'));
  }

  toMessageCardObj(msg: AppointmentMessage) {
    return {
      content: msg.content,
      messagedOn: msg.messagedOn,
      imageUrl: msg.sender.imageUrl ?? this.defaultUserImageUrl,
      isMain: this.currentAuth?.id == msg.sender.id,
      imageUrls: msg.medias?.filter(m => m.type == MediaTypes.Image)?.map(m => m.url)
    }
  }

  message(event: any) {
    console.log(event);
    const message: CreateAppointmentMessage = {
      content: event.content,
      images: event.files,
      appointmentId: this.filter.appointmentId
    }

    this.messages?.data.push({
      id: 0,
      content: message.content,
      messagedOn: null,
      medias: message.images?.map(img => {
        return {
          type: MediaTypes.Image,
          url: URL.createObjectURL(img)
        }
      }),
      sender: {
        id: this.currentAuth!.id,
        firstName: '',
        lastName: '',
        email: '',
        imageUrl: this.currentAuth!.imageUrl ?? this.defaultUserImageUrl,
        joinedOn: '',
        doctor: null!,
        role: this.currentAuth!.role
      }
    });

    this.activeScrollToBottom = true;

    this.loaderService.skipNextRequest();

    this.appointmentMessagesService.createMessage(message)
      .pipe(catchError(err => {
        this.messages?.data.pop();
        return throwError(() => err);
      }))
      .subscribe(res => {
        this.messages?.data.pop();
        this.messages?.data.push(res);
        this.activeScrollToBottom = true;
      });
  }

  loadMessages() {
    this.appointmentMessagesService.get(this.filter)
      .pipe(finalize(() => this.isFirstLoading = false))
      .subscribe(res => {
        if (this.messages) {
          res.data.unshift(...this.messages.data.reverse());
          this.returnScrollToPrevPos = true;
        }
        else
          this.activeScrollToBottom = true;

        this.messages = res;
        this.messages.data.reverse();
      });
  }

  prevScrollBottom: number = 0;
  returnScrollToPrevPos = false;

  loadMoreMessages() {
    if (this.messages?.hasNextPage) {

      const ele = this.msgsContainer?.nativeElement as HTMLElement;
      this.prevScrollBottom = ele.scrollHeight - ele.scrollTop;

      this.filter.pageNumber += 1;
      this.loadMessages();
    }
  }

  @ViewChild("messagesContainer") msgsContainer: ElementRef | null = null;

  scrollBottom() {
    const ele = this.msgsContainer?.nativeElement as HTMLElement;

    if (this.isFirstScroll) {
      this.renderer.removeClass(ele, 'scroll-smooth');
      this.isFirstScroll = false;
    }
    else
      this.renderer.addClass(ele, 'scroll-smooth');

    ele.scrollTop = ele.scrollHeight
    ele.style.height = ele.scrollHeight + 'px';
  }

  scrollPrevPos() {
    const ele = this.msgsContainer?.nativeElement as HTMLElement;
    this.renderer.removeClass(ele, 'scroll-smooth');
    ele.scrollTop = ele.scrollHeight - this.prevScrollBottom;
    this.renderer.addClass(ele, 'scroll-smooth');
  }

  ngAfterViewInit() {
    const ele = this.msgsContainer?.nativeElement as HTMLElement;

    this.renderer.listen(ele, 'scroll', e => {
      if (ele.scrollTop == 0 && this.messages?.hasNextPage)
        this.loadMoreMessages();

      if (ele.scrollHeight - ele.clientHeight - ele.scrollTop > 150)
        this.isScrollBottomBtnActive = true;
      else
        this.isScrollBottomBtnActive = false;
    })
  }

  ngAfterViewChecked() {
    if (this.activeScrollToBottom) {
      this.scrollBottom();
      this.activeScrollToBottom = false;
    }

    if (this.returnScrollToPrevPos) {
      this.scrollPrevPos();
      this.returnScrollToPrevPos = false;
    }
  }

  amIPatient() {
    return this.currentAuth?.role == Roles.Patient;
  }
  amIDoctor() {
    return this.currentAuth?.role == Roles.Doctor;
  }

  ngOnDestroy() {
    this.appointmentMessagesService.closeHubConnection();
  }
}
