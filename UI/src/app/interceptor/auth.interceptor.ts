import { HttpInterceptorFn, HttpRequest } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  console.log('Auth Interceptor');
  
  const userTokenString = localStorage.getItem('user')?.toString();

  if(!userTokenString) return next(req);

  const user = JSON.parse(userTokenString);

  const modifiedReq = req.clone({
    headers: req.headers.set('Authorization', `Bearer ${user.token}`)
  });

  return next(modifiedReq);
};
