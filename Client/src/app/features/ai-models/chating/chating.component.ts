import { AfterViewChecked, AfterViewInit, ChangeDetectorRef, Component, ElementRef, Renderer2, ViewChild } from '@angular/core';
import { AuthResult } from 'src/app/models/account/auth-result';
import { PaginatedList } from 'src/app/models/paginated-list';
import { UserChatBotMessage } from 'src/app/models/chat-bot/user-chat-bot-message';
import { AccountService } from 'src/app/services/account.service';
import { ChatBotService } from 'src/app/services/chat-bot.service';
import { LoaderService } from 'src/app/services/loader.service';
import { catchError, finalize, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { UserChatBotFilter } from 'src/app/models/chat-bot/user-chat-bot-filter';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-chating',
  templateUrl: './chating.component.html',
  styleUrls: ['./chating.component.css']
})
export class ChatingComponent {

  defaultImageUrl = environment.defaultUserImageUrl;

  suggestions: string[] =
    [
      "Do you have any allergies?",
      "How often do you exercise?",
      "What is your average daily water intake?",
      "Do you smoke or use tobacco products?"
    ];

  currentAuth: AuthResult | null = null;
  paginatedList: PaginatedList<UserChatBotMessage> | null = null;

  paginationFilter: UserChatBotFilter = {
    pageNumber: 1,
    pageSize: 20,
    before: new Date().toISOString()
  }

  isWriting = false;
  activeScrollToBottom = false;
  isFirstScroll = true;
  isFirstLoading = true;
  isScrollBottomBtnActive = false;

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
    this.chatBotService.getMessages(this.paginationFilter)
      .pipe(finalize(() => this.isFirstLoading = false))
      .subscribe(res => {
        if (this.paginatedList) {
          res.data.unshift(...this.paginatedList.data.reverse());
          this.returnScrollToPrevPos = true;
        }
        else
          this.activeScrollToBottom = true;

        this.paginatedList = res;
        this.paginatedList.data.reverse();
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

    this.paginatedList?.data.push({
      id: 0,
      content: content,
      messagedOn: null,
      isBotMessage: false
    });

    this.activeScrollToBottom = true;
    this.isWriting = true;

    this.loaderService.skipNextRequest();

    this.chatBotService.generateResponse(content)
      .pipe(finalize(() => this.isWriting = false),
        catchError(err => {
          this.paginatedList?.data.pop();
          return throwError(() => err);
        }))
      .subscribe(res => {
        this.paginatedList?.data.pop();
        this.paginatedList?.data.push(res.userMessage);
        if (!res.isFailedToGenerateResponse)
          this.paginatedList?.data.push(res.chatBotMessage);
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

}

