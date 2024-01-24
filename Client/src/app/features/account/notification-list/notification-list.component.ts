import { Component } from '@angular/core';
import { finalize } from 'rxjs';
import { NotificationsFilter } from 'src/app/models/notifications/notifications-filter';
import { Notification } from 'src/app/models/notifications/notification';
import { PaginatedList } from 'src/app/models/paginated-list';
import { NotificationsService } from 'src/app/services/notifications.service';

@Component({
  selector: 'app-notification-list',
  templateUrl: './notification-list.component.html',
  styleUrls: ['./notification-list.component.css']
})
export class NotificationListComponent {
  paginatedList: PaginatedList<Notification> | null = null;
  filter: NotificationsFilter = {
    pageNumber: 1,
    pageSize: 10,
    before: new Date().toUTCString()
  }
  isLoading: boolean = false;

  constructor(private notificationsService: NotificationsService) { }

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
        console.log(res);
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

  getPath(notification: Notification) {
    switch (notification.referenceType) {
      case 'Comment':
      case 'Reply':
        return '/posts/post-for-comment/' + notification.referenceId;
      default:
        return '/notfound'
    }
  }
}
