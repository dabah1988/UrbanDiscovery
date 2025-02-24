import { Component } from '@angular/core';
import { AccountService } from './services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  [x: string]: any;
  title = 'List of Cities';

  constructor(public accountService: AccountService, private router: Router) {

  }

  get isUserConnected():boolean {
   // return !this.accountService.currentUserName;
    return !localStorage["currentUserName"];
  }

  onLogOutClicked() {
    this.accountService.getLogout().subscribe(
      {
        next: (response: string) =>
        {
          this.accountService.currentUserName = null;
          localStorage.removeItem("token");
          localStorage.removeItem("currentUserName");
          this.router.navigate(['/login']);
        },
        error: () => { },
        complete: () => { }
      });
  }

  refreshTokenClicked(): void {
    this.accountService.refreshToken().subscribe(
      {
        next: (response: any) => {
          localStorage["token"] = response.token;
          localStorage["refreshToken"] = response.refreshToken;
        },
        error: (error: Error) => { console.log(error.message); },
        complete: () => { }
      });
  }
}
