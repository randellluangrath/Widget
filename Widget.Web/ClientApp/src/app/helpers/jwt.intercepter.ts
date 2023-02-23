import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '@env/environment';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor() {}
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // Not really a JWT Intercepter, but using this pattern to set the API Key.
    request = request.clone({
      setHeaders: {
        'X-API-Key': environment.widgetApiKey,
        'Access-Control-Allow-Origin': '*',
      },
    });
    return next.handle(request);
  }
}
