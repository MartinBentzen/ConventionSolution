import { Component } from '@angular/core';
import { Service, AuthUser } from './service';
import { Observable, of, BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  userDataSubscription: any;
  authUser = new AuthUser();
  constructor(private service: Service) {
    this.userDataSubscription = this.service.userData
      .asObservable()
      .subscribe(data => {
        debugger;
        this.authUser = data;
      });
  }

  ngOnInit() {}
}
