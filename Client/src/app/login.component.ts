import { Component } from '@angular/core';
import { Service, LoginRequest } from './service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  public loginRequest = new LoginRequest();

  constructor(
    private service: Service,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  onSubmit() {
    const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl') || '/';
    this.service.login(this.loginRequest).subscribe(() => {
      this.router.navigate([returnUrl]);
    });
  }
}
