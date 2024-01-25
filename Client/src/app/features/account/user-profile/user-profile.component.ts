import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Roles } from 'src/app/models/account/roles.enum';
import { User } from 'src/app/models/users/user';
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
  tab = 0;

  constructor(private usersService: UsersService,
    private activatedRoute: ActivatedRoute,
    private router: Router) {
    this.user = router.getCurrentNavigation()?.extras.state as User;
    if (this.user)
      this.tab = this.isDoctor() ? 0 : 2;
  }

  ngOnInit() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    if (id && !this.user)
      this.usersService.getUser(+id).subscribe(user => {
        console.log(user);
        this.user = user;
        this.tab = this.isDoctor() ? 0 : 2;
      });
  }

  isDoctor() {
    return this.user?.role == Roles.Doctor
  }

  getLanguages() {
    return this.user?.doctor.languages.map(l => l.name).join(', ');
  }

}
