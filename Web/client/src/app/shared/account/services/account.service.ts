import { EventEmitter, Injectable, OnInit } from '@angular/core';
import { IUser } from '../types/IUser';
import { BehaviorSubject, Observable, Subject, catchError, finalize, map, tap, throwError } from 'rxjs';
import AccountHttpService from './http/account-http.service';
import { IAuthResponse, ILoginRequest } from './http/types/account';
import { HttpErrorResponse } from '@angular/common/http';
import { parseJwt } from '../../helpers/parseJwt';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  isLoginModalActiveSubject = new Subject<boolean>();
  isLoadingSubject = new Subject<boolean>();
  _accessToken = new BehaviorSubject<string | null>(null);
  _refreshToken = new BehaviorSubject<string | null>(null);

  success = new Subject<void>();
  failure = new Subject<string>();

  accessTokenExpirationTimer: any;

  constructor(
    private http: AccountHttpService
  ) { }

  login(request: ILoginRequest) {
    this.isLoadingSubject.next(true);

    return this.http.login(request).pipe(
      tap((response: IAuthResponse) => {
        this.handleLogin(response);
        return response;
      }),
      catchError((error: HttpErrorResponse) => {
        this.failure.next(error.message);
        this.isLoadingSubject.next(false);

        return throwError(() => new Error('Ого, что-то пошло не так! Попробуйте еще раз.'));
      })
    );
  }

  autoLogin() {
    const token = JSON.parse(localStorage.getItem('accessToken') ?? '');

    if (!token) {
      return;
    }

    const expiresAt = new Date(parseJwt(token)['exp'] * 1000);

    if (expiresAt <= new Date()) {
      this.refresh().subscribe();
      return;
    }

    this._accessToken.next(token);
  }

  autoLogout(expirationDuration: number) {
    this.accessTokenExpirationTimer = setTimeout(() => {
      this.refresh().subscribe();
    }, expirationDuration);
  }

  refresh() {
    return this.http.refresh().pipe(
      tap((response) => {
        this.handleLogin(response);
      }),
      catchError((error: HttpErrorResponse) => {
        this.handleLogout();
        return throwError(error.message);
      })
    );
  }

  logout() {
    return this.http.logout().pipe(
      catchError((error: HttpErrorResponse) => {
        this.failure.next(error.message);
        return throwError(() => new Error('Ого, что-то пошло не так! Попробуйте еще раз.'));
      }),
      finalize(() => {
        this.handleLogout();
      })
    );
  }

  handleLogout() {
    this._accessToken.next(null);
    this._refreshToken.next(null);
    localStorage.removeItem('accessToken');
    this.isLoadingSubject.next(false);
  }

  handleLogin(response: IAuthResponse) {
    this._accessToken.next(response.accessToken);
    this._refreshToken.next(response.refreshToken);

    this.success.next();
    this.isLoadingSubject.next(false);
    this.isLoginModalActiveSubject.next(false);
    localStorage.setItem('accessToken', JSON.stringify(response.accessToken));

    const expiresIn = parseJwt(response.accessToken)['exp'] * 1000;
    const expirationDuration = new Date(expiresIn).getTime() - new Date().getTime();

    this.autoLogout(expirationDuration);
  }

  getUser(): Observable<IUser> {
    return this.http.getUser();
  }
}