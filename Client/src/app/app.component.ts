import { Component } from '@angular/core';
import { AccountService } from './services/account.service';
import { NavigationEnd, Router } from '@angular/router';
import { filter, finalize } from 'rxjs';
import { NotificationsService } from './services/notifications.service';
import { ToastrService } from 'ngx-toastr';
import { AuthResult } from './models/account/auth-result';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  isAuthenticating = true;
  currentAuth: AuthResult | null = null;

  constructor(private accountService: AccountService,
    private notificationsService: NotificationsService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.relogin();
    this.scrollTopWhenOpeningAComponent();
    this.startNotificationHubConnection();
    this.displayUserNotifications();
    this.loadCurrentAuth();
  }

  startNotificationHubConnection() {
    this.accountService.currentAuth$.subscribe(auth => {
      if (auth)
        this.notificationsService.startHubConnection(auth);
      else
        this.notificationsService.closeHubConnection();
    })
  }

  loadCurrentAuth() {
    this.accountService.currentAuth$.subscribe(auth => this.currentAuth = auth)
  }

  displayUserNotifications() {
    this.notificationsService.notifiations$.subscribe(notify => this.toastr.info(notify.content));
  }

  relogin() {
    this.accountService.relogin()
      .pipe(finalize(() => this.isAuthenticating = false))
      .subscribe();
  }

  scrollTopWhenOpeningAComponent() {
    this.router.events
      .pipe(filter(e => e instanceof NavigationEnd))
      .subscribe(() => window.scrollTo({
        top: 0,
        left: 0,
        behavior: 'instant'
      }))
  }

}
