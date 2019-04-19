import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { AppService } from '../app/app.service';
import { Account } from '../../models/app/account';
import { Login } from '../../models/login/login';

const Url = 'http://localhost:59146/api/accounts';
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  constructor(
    private http: HttpClient,
    private appService: AppService
  ) { }

  
  login(login: Login): Observable<Account> {
    return this.http.post<Account>(`${Url}/login`, login, httpOptions)
      .pipe(
        tap(_ => {
          AppService.setToLocalStorage(AppService.Consts.AccountKey, _);
        }),
        catchError(this.appService.handleError<Account>('用户登录'))
      )
  }
  
  logout(){
    localStorage.removeItem(AppService.Consts.AccountKey);
  }
  
}
