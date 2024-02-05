import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs';
import { AuthResult } from 'src/app/models/account/auth-result';
import { PaginatedList } from 'src/app/models/paginated-list';
import { DoctorFilter } from 'src/app/models/users/doctor-filter';
import { User } from 'src/app/models/users/user';
import { AccountService } from 'src/app/services/account.service';
import { UsersService } from 'src/app/services/users.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-doctors',
  templateUrl: './doctors.component.html',
  styleUrls: ['./doctors.component.css']
})
export class DoctorsComponent {

  defaultImageUrl = environment.defaultUserImageUrl;

  doctors: PaginatedList<User> | null = null;

  doctorFilter: DoctorFilter = {
    pageNumber: 1,
    pageSize: 6,
    name: undefined,
    experience: undefined,
    specialityId: undefined,
    orderBy: undefined
  }
  isLoading = false;
  currentAuth: AuthResult | null = null;

  constructor(private usersService: UsersService,
    private activatedRouter: ActivatedRoute,
    private accountService: AccountService) {

  }

  ngOnInit() {
    this.doctorFilter.specialityId = Number(this.activatedRouter.snapshot.queryParamMap.get('specId'));
    this.loadDoctors();
    this.loadCurrentAuth();
  }

  loadCurrentAuth() {
    this.accountService.currentAuth$.subscribe(auth => this.currentAuth = auth);
  }

  applyFilter(filter: any) {
    this.doctorFilter.pageNumber = 1;
    this.doctorFilter.name = filter.name;
    this.doctorFilter.experience = filter.experience;
    this.doctorFilter.specialityId = filter.specialityId;
    this.doctorFilter.orderBy = filter.orderBy;
    this.loadDoctors();
  }

  loadDoctors() {
    this.isLoading = true;
    this.usersService.getDoctors(this.doctorFilter)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe(res => this.doctors = res);
  }

  handlePageEvent(event: PageEvent) {
    this.doctorFilter.pageSize = event.pageSize;
    this.doctorFilter.pageNumber = event.pageIndex + 1;
    this.loadDoctors();
  }

}
