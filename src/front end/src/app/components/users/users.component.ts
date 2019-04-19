import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user/user';
import { UserService } from '../../services/user/user.service';


@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  users:User[];
  user: User;

  constructor(
    private userService: UserService
  ) { }

  ngOnInit() {
    this.get(1, 10);
  }

  get(page: number, size : number): void{
    this.userService.get(page, size)
      .subscribe(users => this.users = users);
  }

  create():void{
    this.userService.create(this.user)
      .subscribe(user => this.users.push(user));
  }

}
