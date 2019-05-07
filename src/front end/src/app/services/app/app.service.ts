import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Router } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd';
import { Account } from '../../models/app/account';


@Injectable({
  providedIn: 'root'
})
export class AppService {

  static Consts = {
    AccountKey : "Account"
  }

  constructor(
    private router : Router,
    private message: NzMessageService
  ) { }

  /**
  * 处理HTTP失败的操作
  * @param operation - 失败操作的描述
  * @param result - 可选的，作为 observable<T> 来返回
  */
   handleError<T>(operation = '操作', result?: T) {
    return (errorResponse: any): Observable<T> => {

      console.error(errorResponse);
      
      var statusCode = errorResponse.status;
      if(statusCode == '401'){
        this.router.navigate(['/login']);
      }
      if(statusCode == '0'){
        this.alert(operation, OperationResultType.Error, '连接服务器失败！');
      }else if(statusCode == '400'){
        this.alert(operation, OperationResultType.Warning, errorResponse.error);
      }else{
        this.alert(operation, OperationResultType.Error, errorResponse.error);
      }

      return of(result as T);
    };
  }

  /**
   * 弹出消息
   * @param operation - 失败操作的描述
   * @param type - 操作结果类型
   * @param message - 消息
   */
  alert(operation: string, type: OperationResultType, message: string) {
    this.message.create(type, `${operation}：${message}`);
  }

  /**
   * 设置LocalStorage
   * @param key 
   * @param value 
   */
  static setToLocalStorage<T>(key: string, value?: T){
    var type = typeof value;
    var v : any;
    if(value === undefined){
        v = "";
    }else if(type === 'object'){
      v = JSON.stringify(value);
    }else{
      v = value;
    }

    localStorage.setItem(key, v);
  }

  /**
   * 获取当前账户信息
   */
  static getCurrentAccount(): Account {
    return JSON.parse(localStorage.getItem(this.Consts.AccountKey)) as Account;
  }
}

/**
 * 操作结果类型
 */
export enum OperationResultType {
  Info = 'info',
  Success = 'sucess',
  Warning = 'warning',
  Error = 'error',
  Loading = 'loading'
}
