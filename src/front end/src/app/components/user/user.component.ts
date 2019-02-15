import { Component, OnInit } from '@angular/core';

import { User } from '../../models/user';
import { UserService } from '../../services/user/user.service';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  users:User[];
  user: User;

  constructor(
    private userService: UserService
  ) { }

  ngOnInit() {
    this.get();
  }

  get(page = 1, size = 10): void{
    this.userService.get(page, size)
      .subscribe(users => this.users = users);
  }

  create():void{
    this.userService.create(this.user)
      .subscribe(user => this.users.push(user));
  }

}
