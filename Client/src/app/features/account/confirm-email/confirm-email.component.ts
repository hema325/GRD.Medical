import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css']
})
export class ConfirmEmailComponent {

  constructor(private accountService: AccountService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService) { }


  ngOnInit() {
    this.confirmEmail();
    this.router.navigateByUrl('/home');
  }

  confirmEmail() {
    let userId = this.activatedRoute.snapshot.queryParamMap.get('userId');
    let token = this.activatedRoute.snapshot.queryParamMap.get('token');
    this.accountService.confrimEmail({ userId, token }).pipe(take(1)).subscribe(res => this.toastr.success('Email confirmed successfully.'));
  }

}
