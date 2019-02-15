import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

import { Login } from '../../models/login';
import { AccountService } from '../../services/account/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: Login ={
    userNameOrEmail:'',
    password:''
  };

  constructor(
    private accountService: AccountService,
    private location: Location
  ) { }

  ngOnInit() {
  }

  login(): void{
    this.accountService.login(this.user)
      .subscribe(() => location.href = '/')
  }
}
