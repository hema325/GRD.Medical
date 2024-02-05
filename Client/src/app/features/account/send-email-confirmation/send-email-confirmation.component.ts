import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { ErrorCodes } from 'src/app/models/Errors/error-codes.enum';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-send-email-confirmation',
  templateUrl: './send-email-confirmation.component.html',
  styleUrls: ['./send-email-confirmation.component.css']
})
export class SendEmailConfirmationComponent {

  email: String | null = null;
  counter: number = 60;

  constructor(private accountService: AccountService,
    private router: Router) {

    this.email = router.getCurrentNavigation()?.extras.state as String;
  }

  ngOnInit() {
    this.send();
  }

  send() {
    this.accountService.sendEmailConfirmation({ email: this.email }).pipe(finalize(() => this.activateTimer())).subscribe({
      error: err => {
        if (ErrorCodes.VerifiedEmail == err.errorCode)
          this.router.navigateByUrl('/account/login');
      }
    });
  }

  activateTimer() {
    this.counter = 60;
    const intervalId = setInterval(() => {

      --this.counter;

      if (!this.counter)
        clearInterval(intervalId);

    }, 1000);
  }

}
