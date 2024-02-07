import { Component } from '@angular/core';
import { finalize } from 'rxjs';
import { NotificationsFilter } from 'src/app/models/notifications/notifications-filter';
import { Notification } from 'src/app/models/notifications/notification';
import { PaginatedList } from 'src/app/models/paginated-list';
import { NotificationsService } from 'src/app/services/notifications.service';
import { Router } from '@angular/router';
import { ReferenceTypes } from 'src/app/models/notifications/reference-types.enum';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-notification-list',
  templateUrl: './notification-list.component.html',
  styleUrls: ['./notification-list.component.css']
})
export class NotificationListComponent {
  defaultUserImageUrl = environment.defaultUserImageUrl;
  paginatedList: PaginatedList<Notification> | null = null;
  filter: NotificationsFilter = {
    pageNumber: 1,
    pageSize: 10,
    before: new Date().toUTCString()
  }
  isLoading: boolean = false;

  constructor(private notificationsService: NotificationsService,
    private router: Router) { }

  ngOnInit() {
    this.loadNotifications();
    this.displayUserNotifications();
  }

  displayUserNotifications() {
    this.notificationsService.notifiations$.subscribe(notify => this.paginatedList?.data.unshift(notify));
  }

  loadNotifications() {
    this.isLoading = true;
    this.notificationsService.get(this.filter)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe(res => {
        if (this.paginatedList)
          res.data.unshift(...this.paginatedList.data);

        this.paginatedList = res
      });
  }

  loadMoreNotifications() {
    if (this.paginatedList?.hasNextPage && !this.isLoading) {
      this.filter.pageNumber += 1;
      this.loadNotifications();
    }
  }

  markAsRead(notification: Notification) {
    if (!notification.isRead)
      this.notificationsService.markAsRead(notification.id).subscribe();
  }

  navigateToNotificationSrc(notification: Notification) {
    switch (notification.referenceType) {
      case ReferenceTypes.Comment:
      case ReferenceTypes.Reply:
        this.router.navigateByUrl('/posts/post-for-comment/' + notification.referenceId);
        break;
      case ReferenceTypes.Appointment:
        this.router.navigate(['/account'], { fragment: '4' });
        break;
      case ReferenceTypes.Review:
        this.router.navigate(['/account'], { fragment: '7' });
        break;
      case ReferenceTypes.Reminder:
        this.router.navigate(['/appointments/chat-count-down'], { queryParams: { appontId: notification.referenceId } });
        break;
      default:
        this.router.navigateByUrl('/notfound');
    }
  }

  getImageSrc(notification: Notification) {
    switch (notification.referenceType) {
      case ReferenceTypes.Reminder:
        return 'assets/images/reminder.jpg';
      default:
        return notification?.initiator?.imageUrl ?? this.defaultUserImageUrl;
    }
  }
}
