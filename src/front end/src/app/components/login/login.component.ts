import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

import { Login } from '../../models/login/login';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  isSpinning = false;
  title = "Template App";
  validateForm: FormGroup;

  constructor(
    private authService: AuthService,
    private router: Router,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.clear();
  }

  login(): void {
    this.isSpinning = true;
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[i].markAsDirty();
      this.validateForm.controls[i].updateValueAndValidity();
    }

    var loginModel: Login = {
      userNameOrEmail: this.validateForm.controls["userNameOrEmail"].value,
      password: this.validateForm.controls["password"].value
    };
    this.authService.login(loginModel)
      .subscribe(_ => {
        if (typeof _ !== 'undefined') {
          console.log('账户信息');
          console.log(_);
          this.router.navigate(['']);
        }
        this.isSpinning = false;
      });
  }

  private clear(): void {
    this.validateForm = this.fb.group({
      userNameOrEmail: [null, [Validators.required]],
      password: [null, [Validators.required]],
      remember: [true]
    });
  }
}
