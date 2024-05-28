import { HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { Router } from '@angular/router';

export const errorInterceptor: HttpInterceptorFn = (req: HttpRequest<any> , next) => {
  return next(req);
};
