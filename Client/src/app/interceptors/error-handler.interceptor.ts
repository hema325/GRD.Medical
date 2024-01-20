import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { ErrorCodes } from '../models/Errors/error-codes.enum';
import { ErrorResponse } from '../models/Errors/error-response';
import { ExceptionResponse } from '../models/Errors/exception-response';
import { ValidationResponse } from '../models/Errors/validation-response';

@Injectable()
export class ErrorHandlerInterceptor implements HttpInterceptor {

  constructor(private toastr: ToastrService,
    private router: Router) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(catchError(error => {

      if (error.status == 404)
        this.router.navigateByUrl('/not-found');
      else if (error.status == 500)
        this.router.navigateByUrl('/server-error', { state: { error: error.error } });
      else {
        const errorMessage = this.getErrorMessageByCode(error.error.errorCode);
        if (errorMessage)
          this.toastr.error(errorMessage);
      }

      return throwError(() => error.error as (ErrorResponse | ExceptionResponse | ValidationResponse));
    }));
  }


  getErrorMessageByCode(code: ErrorCodes) {
    switch (code) {
      case ErrorCodes.InvalidPassword:
        return "The password you provided was incorrect.";
      case ErrorCodes.InvalidEmail:
        return "The email you provided is not valid.";
      case ErrorCodes.InvalidData:
        return "The data you provided was invalid.";
      case ErrorCodes.InvalidJwtToken:
        return "Your session has expired.";
      case ErrorCodes.VerifiedEmail:
        return "Your email is verified.";
      case ErrorCodes.WrongEmailPassword:
        return "Email or password is incorrect.";
      case ErrorCodes.InvalidEmailVerificationToken:
        return "Failed to verify your email";
      case ErrorCodes.InvalidResetPasswordToken:
        return "Failed to reset your password";
      case ErrorCodes.AccessDenied:
        return "You are not allowed to access this action.";
      case ErrorCodes.UnSpecified:
      default:
        return null;
    }

  }
}
