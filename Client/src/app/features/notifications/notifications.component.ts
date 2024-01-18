import { Component } from '@angular/core';
import { NotificationsFilter } from 'src/app/models/notifications/notifications-filter';
import { PaginatedList } from 'src/app/models/paginated-list';
import { NotificationsService } from 'src/app/services/notifications.service';
import { Notification } from 'src/app/models/notifications/notification';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent {

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
