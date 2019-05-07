import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';

import { Account } from '../../../models/app/account';
import { AppService } from '../../../services/app/app.service';
import { AuthService } from '../../../services/auth/auth.service';



@Component({
  selector: 'header-nav-user',
  templateUrl: './header-nav-user.component.html',
  styleUrls: ['./header-nav-user.component.scss']
})
export class HeaderNavUserComponent implements OnInit {

  account : Account;
  

  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.account = AppService.getCurrentAccount();
  }

  getUserNameFirstCharacter(){
    return this.account.user.userName[0].toUpperCase();
  }

  logout(){
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
