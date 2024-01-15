import { Component } from '@angular/core';
import { AccountService } from './services/account.service';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';
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
    this.loadCurrentAuth();
    this.displayUserNotifications();
  }

  loadCurrentAuth() {
    this.accountService.currentAuth$.subscribe(auth => {
      if (auth) {
        this.currentAuth = auth;
        this.notificationsService.openHubConnection(auth);
      }
      else
        this.notificationsService.closeHubConnection();
    })
  }

  displayUserNotifications() {
    this.notificationsService.notifiations$.subscribe(notify => this.toastr.info(notify.content));
  }

  relogin() {
    this.accountService.relogin().subscribe({
      next: res => this.isAuthenticating = false,
      error: err => this.isAuthenticating = false
    });
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
