import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthResult } from 'src/app/models/account/auth-result';
import { Roles } from 'src/app/models/account/roles.enum';
import { User } from 'src/app/models/users/user';
import { AccountService } from 'src/app/services/account.service';
import { UsersService } from 'src/app/services/users.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent {

  defaultUserImageUrl = environment.defaultUserImageUrl;
  user: User | null = null;
  tab = -1;

  currentAuth: AuthResult | null = null;

  constructor(private usersService: UsersService,
    private activatedRoute: ActivatedRoute,
    private accountService: AccountService,
    router: Router) {
    this.user = router.getCurrentNavigation()?.extras.state as User;
  }

  ngOnInit() {
    this.initTab();
    this.loadUser();
    this.loadCurrentAuth();
  }

  loadUser() {
    this.activatedRoute.paramMap.subscribe(map => {
      const id = map.get('id');
      if (id)
        this.usersService.getUser(+id).subscribe(user => {
          this.user = user;
          this.initTab();
        });
    });
  }

  initTab() {
    this.activatedRoute.fragment.subscribe(frag => {
      if (frag)
        this.tab = Number(frag)
    });
    if (this.tab == -1 && this.user)
      this.tab = this.isDoctor() ? 0 : 2;
  }

  loadCurrentAuth() {
    this.accountService.currentAuth$.subscribe(auth => this.currentAuth = auth);
  }

  isDoctor() {
    return this.user?.role == Roles.Doctor
  }

  getLanguages() {
    return this.user?.doctor.languages.map(l => l.name).join(', ');
  }

  amIDoctor() {
    return this.currentAuth?.role == Roles.Doctor;
  }

}
