import { HttpErrorResponse, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req: HttpRequest<any>, next) => {
  return next(req).pipe(
    catchError((httpResponse: HttpErrorResponse) => {
      if (httpResponse.error) {
        switch (httpResponse.status) {
          case 401:
            if (httpResponse.error.errors) {
              const modelStateErrors = [];
              for (const key in httpResponse.error.errors) {
                if (httpResponse.error.errors[key])
                  modelStateErrors.push(httpResponse.error.errors[key])
              }
              throw modelStateErrors;
            }
            break;
        }
      }
      throw httpResponse;
    })
  );
};
