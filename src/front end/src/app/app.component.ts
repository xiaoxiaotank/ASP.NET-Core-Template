import { Component, Input, OnInit } from '@angular/core';
import { Router, NavigationStart, NavigationEnd, NavigationError, NavigationCancel} from '@angular/router';

import { AppService } from './services/app/app.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'template app';
  isSpinning = true;

  constructor(
    private router : Router
  ){}

  ngOnInit() {
    var account = AppService.getCurrentAccount();
    if(account == null){
      this.router.navigate(['/login']);
    }

    this.router.events.subscribe((event) => {
			if (event instanceof NavigationStart) {
				this.isSpinning = true;
			}
			if (event instanceof NavigationEnd ||
				event instanceof NavigationError ||
				event instanceof NavigationCancel) {
          this.isSpinning = false;;
			}
		});
  }
}
