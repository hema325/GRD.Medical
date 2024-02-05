import { Injectable } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError, filter, take, throwError } from 'rxjs';
import { NotificationsService } from './notifications.service';
import { AccountService } from './account.service';
import { LoaderService } from './loader.service';

@Injectable({
  providedIn: 'root'
})
export class AppInitialiserService {

  constructor(private accountService: AccountService,
    private notificationsService: NotificationsService,
    private router: Router,
    private toastr: ToastrService,
    private loader: LoaderService) { }

  initialise() {
    this.scrollTopWhenOpeningAComponent();
    this.startNotificationHubConnection();
    this.displayUserNotifications();
    this.handleAutoRelogin();
  }

  private scrollTopWhenOpeningAComponent() {
    this.router.events
      .pipe(filter(e => e instanceof NavigationEnd))
      .subscribe(() => window.scrollTo({
        top: 0,
        left: 0,
        behavior: 'instant'
      }))
  }

  private displayUserNotifications() {
    this.notificationsService.notifiations$.subscribe(notify => this.toastr.info(notify.content));
  }

  private startNotificationHubConnection() {
    let notificationHubActive = false;
    this.accountService.currentAuth$.subscribe(auth => {
      if (auth && !notificationHubActive) { //login
        this.notificationsService.startHubConnection();
        notificationHubActive = true;
      }
      else if (notificationHubActive && !auth) { //logout
        this.notificationsService.closeHubConnection();
        notificationHubActive = false;
      }
    })
  }

  private handleAutoRelogin() {
    let autoReloginActive = false;
    this.accountService.currentAuth$.subscribe(auth => {
      let intervalId = undefined;
      if (!autoReloginActive && auth) { //login
        autoReloginActive = true;
        const span = new Date(auth.jwtTokenExpiresOn).getTime() - new Date().getTime();
        intervalId = setInterval(() => this.tryToRelogin(), span);
      }
      else if (autoReloginActive && !auth) //logout 
        clearInterval(intervalId);
    })
  }

  private tryToRelogin() {
    this.loader.skipNextRequest();
    this.accountService.relogin().pipe(take(1), catchError(error => {
      //logout if relogin failed
      this.accountService.logout().subscribe();
      this.router.navigateByUrl('/home');
      return throwError(() => error);
    })).subscribe()
  }

}
