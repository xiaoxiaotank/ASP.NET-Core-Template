import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { User } from '../../models/user';

const Url = 'http://localhost:59146/api/users';
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class UserService {

  constructor(
    private http:HttpClient
  ) { }

  get(page: number, size: number):Observable<User[]>{
    return this.http.get<User[]>(`${Url}/?page=${page}&size=${size}`)
      .pipe(
        tap(_ => console.log(_)),
        catchError(this.handleError<User[]>("获取用户列表"))
      )
  }

  create(user: User): Observable<User>{
    return this.http.post<User>(Url, user, httpOptions)
      .pipe(
        tap(_ => console.log(_)),
        catchError(this.handleError<User>("新增用户"))
      )
  }

  /**
  * 处理HTTP失败的操作
  * @param operation - 失败操作的描述
  * @param result - 可选的，作为 observable<T> 来返回
  */
  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} 失败: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  private log(message: string){
    alert(`HeroService: ${message}`);
  }
}
