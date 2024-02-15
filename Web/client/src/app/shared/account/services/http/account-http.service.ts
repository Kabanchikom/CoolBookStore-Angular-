import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ILoginRequest, IAuthResponse } from './types/account';
import HttpBase from 'src/app/shared/http/HttpBase';
import { IUser } from '../../types/IUser';

@Injectable({
  providedIn: 'root'
})
export default class AccountHttpService extends HttpBase {
  baseUrl = this.apiUrl + '/account';

  login(request: ILoginRequest): Observable<IAuthResponse> {
    return this.http.post<IAuthResponse>(`${this.baseUrl}/login`, request, {withCredentials: true});
  }

  logout(): Observable<IAuthResponse> {
    return this.http.patch<IAuthResponse>(`${this.baseUrl}/logout`, null, {withCredentials: true});
  }

  refresh(): Observable<IAuthResponse> {
    return this.http.get<IAuthResponse>(`${this.baseUrl}/refresh`, {withCredentials: true});
  }

  getUser(): Observable<IUser> {
    return this.http.get<IUser>(`${this.baseUrl}/user`, {withCredentials: true});
  }
}