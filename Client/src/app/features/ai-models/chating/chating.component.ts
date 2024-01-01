import { AfterViewChecked, AfterViewInit, ChangeDetectorRef, Component, ElementRef, Renderer2, ViewChild } from '@angular/core';
import { AuthResult } from 'src/app/models/account/auth-result';
import { PaginatedList } from 'src/app/models/paginated-list';
import { UserChatBotMessage } from 'src/app/models/chat-bot/user-chat-bot-message';
import { AccountService } from 'src/app/services/account.service';
import { ChatBotService } from 'src/app/services/chat-bot.service';
import { LoaderService } from 'src/app/services/loader.service';
import { catchError, finalize, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-chating',
  templateUrl: './chating.component.html',
  styleUrls: ['./chating.component.css']
})
export class ChatingComponent {

  currentAuth: AuthResult | null = null;
  paginatedList: PaginatedList<UserChatBotMessage> | null = null;

  paginationFilter = {
    pageNumber: 1,
    pageSize: 20
  }

  isWriting = false;
  activeScrollToBottom = false;
  isFirstScroll = true;

  constructor(private accountService: AccountService,
    private chatBotService: ChatBotService,
    private loaderService: LoaderService,
    private renderer: Renderer2,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.loadCurrentAuth();
    this.loadMessages();
  }

  loadCurrentAuth() {
    this.accountService.currentAuth$.subscribe(auth => this.currentAuth = auth);
  }

  loadMessages() {
    this.chatBotService.getMessages(this.paginationFilter.pageNumber, this.paginationFilter.pageSize).subscribe(res => {

      if (this.paginatedList) {
        res.data.unshift(...this.paginatedList.data);
        this.returnScrollToPrevPos = true;
      }
      else
        this.activeScrollToBottom = true;

      this.paginatedList = res;
    });
  }

  prevScrollBottom: number = 0;
  returnScrollToPrevPos = false;

  loadMoreMessages() {
    if (this.paginatedList?.hasNextPage) {

      const ele = this.msgsContainer?.nativeElement as HTMLElement;
      this.prevScrollBottom = ele.scrollHeight - ele.scrollTop;

      this.paginationFilter.pageNumber += 1;
      this.loadMessages();
    }
  }

  message(content: string) {

    this.paginatedList?.data.unshift({
      id: 0,
      content: content,
      messagedOn: null,
      isBotMessage: false
    });

    this.activeScrollToBottom = true;
    this.isWriting = true;

    this.loaderService.deactivate();

    this.chatBotService.generateResponse(content)
      .pipe(finalize(() => {
        this.isWriting = false;
        this.loaderService.activate();
      }),
        catchError(err => {
          this.paginatedList?.data.shift();
          return throwError(() => err);
        }))
      .subscribe(res => {
        this.paginatedList?.data.shift();
        this.paginatedList?.data.unshift(res.userMessage);
        if (!res.isFailedToGenerateResponse)
          this.paginatedList?.data.unshift(res.chatBotMessage);
        else
          this.toastr.error("Failed to generate response.");

        this.activeScrollToBottom = true;
      });
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

    ele.scrollTop = ele.scrollHeight;
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
      if (ele.scrollTop == 0 && this.paginatedList?.hasNextPage)
        this.loadMoreMessages();
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

}

