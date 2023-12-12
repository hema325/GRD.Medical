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
        if (error.error.message)
          this.toastr.error(error.error.message);
        else
          this.toastr.error('Something went wrong.');
      }

      return throwError(() => error.error);
    }));
  }
}
