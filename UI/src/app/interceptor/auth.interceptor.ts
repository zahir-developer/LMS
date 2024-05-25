import { HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { LoginUser } from '../model/login.user';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const userTokenString = localStorage.getItem('user')?.toString();

  if(!userTokenString) return next(req);

  const user : LoginUser = JSON.parse(userTokenString);

  const modifiedReq = req.clone({
    headers: req.headers.set('Authorization', `Bearer ${user.authToken.token}`)
  });

  return next(modifiedReq);
};
