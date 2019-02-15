import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Login } from '../../models/login';
import { Account } from '../../models/account';


const Url = 'http://localhost:59146/api/accounts';
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class AccountService {

  constructor(
    private http: HttpClient
  ) { }

  login(login: Login): Observable<Account> {
    return this.http.post<Account>(`${Url}/login`, login, httpOptions)
      .pipe(
        tap(_ => console.log(`账户数据：${_}`)),
        catchError(this.handleError<Account>('用户登录'))
      )
  }

  /**
  * 处理HTTP失败的操作
  * @param operation - 失败操作的描述
  * @param result - 可选的，作为 observable<T> 来返回
  */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} 失败: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  private log(message: string) {
    alert(message);
  }
}
