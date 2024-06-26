import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';

import { routes } from './app.routes';
import { authInterceptor } from './interceptor/auth.interceptor';
import { errorInterceptor } from './interceptor/error.interceptor';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { PaginationConfig } from 'ngx-bootstrap/pagination';

/* toaster - notification service */
import { provideToastr } from 'ngx-toastr';
import { MatDialogHelper } from './model/common/mat.dialog.helper';
import { DatePipe } from '@angular/common';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptors([authInterceptor, errorInterceptor])),
    provideAnimationsAsync(),
    provideToastr(), // Toastr providers
    MatDialogHelper,
    PaginationConfig,
    DatePipe,
  ]
};
