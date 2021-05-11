import { Component } from '@angular/core';
import { Service, Convention, User, LoginRequest, AuthUser } from './service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  userDataSubscription: any;
  authUser = new AuthUser();

  constructor(private service: Service, private router: Router) {
    if (localStorage.getItem('accessToken')) {
      this.service.setUserDetails();
    }

    this.userDataSubscription = this.service.userData
      .asObservable()
      .subscribe(data => {
        this.authUser = data;
      });
  }

  logout() {
    this.service.logout();
    this.router.navigate(['/']);
  }
}
