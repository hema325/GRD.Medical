import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, delay, finalize, identity } from 'rxjs';
import { LoaderService } from '../services/loader.service';
import { environment } from 'src/environments/environment.development';
import * as cuid from 'cuid';

@Injectable()
export class LoaderInterceptor implements HttpInterceptor {

  constructor(private loader: LoaderService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    const requestId = cuid();
    this.loader.show(requestId);

    return next.handle(request).pipe(
      environment.isProduction ? identity : delay(1000),
      finalize(() => this.loader.hide(requestId)));
  }
}
