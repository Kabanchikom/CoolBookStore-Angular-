import { Injectable } from '@angular/core';
import { CanActivate, CanActivateChild, Router, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, catchError, map, of, throwError } from 'rxjs';
import { AccountService } from './account.service';
import { IUser } from '../types/IUser';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate, CanActivateChild {
  constructor(
    private accountService: AccountService,
    private router: Router,
  ) {

  }

  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
    )
  : boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    return this.canActivate(childRoute, state);
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
    ): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    return this.accountService.getUser().pipe(
      map(
        (user: IUser | null) => {
          if (user) {
            return true;
          } else {
            this.accountService.isLoginModalActiveSubject.next(true);
            return false;
          }
        }
      ),
      catchError((err: any, caugt: Observable<boolean>) => {
        this.accountService.isLoginModalActiveSubject.next(true);
        return of(false);
      })
    );
  }
}