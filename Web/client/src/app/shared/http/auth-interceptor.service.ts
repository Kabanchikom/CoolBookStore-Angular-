import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, exhaustMap, take, tap } from "rxjs";
import { AccountService } from "../account/services/account.service";

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
    constructor(
        private accountService: AccountService
    ) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return this.accountService._accessToken.pipe(
            take(1),
            exhaustMap(accessToken => {
                let authReq = req;
                if (accessToken) {
                    authReq = req.clone({
                        headers: req.headers.set('Authorization', `Bearer ${accessToken}`)
                      });
                }
                return next.handle(authReq);
            })
        );
    }
}