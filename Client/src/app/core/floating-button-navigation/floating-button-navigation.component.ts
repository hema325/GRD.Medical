import { Component } from '@angular/core';
import { finalize } from 'rxjs';
import { AuthResult } from 'src/app/models/account/auth-result';
import { AccountService } from 'src/app/services/account.service';
import { LoaderService } from 'src/app/services/loader.service';
import { NotificationsService } from 'src/app/services/notifications.service';

@Component({
  selector: 'app-floating-button-navigation',
  templateUrl: './floating-button-navigation.component.html',
  styleUrls: ['./floating-button-navigation.component.css']
})
export class FloatingButtonNavigationComponent {

  currentAuth: AuthResult | null = null;
  activeNav: boolean = false;
  notificationsCount: number = 0;

  constructor(private notificationsService: NotificationsService,
    private loaderService: LoaderService) { }

  ngOnInit() {
    this.recieveNotifications();
    this.loadNotificationsCount();
  }


  loadNotificationsCount() {
    this.loaderService.skipNextRequest();
    this.notificationsService.getUnReadNotificationsCount().subscribe();
  }

  closeNav() {
    this.activeNav = false;
  }

  recieveNotifications() {
    this.notificationsService.notificationsCount$.subscribe(count => this.notificationsCount = count);
  }
}
