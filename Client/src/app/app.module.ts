import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { Service } from './service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home.component';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { AdminComponent } from './admin.component';
import { HttpInterceptorService } from './http-interceptor.service';
import { AuthGuard } from './auth.guard.user';
import { AuthGuardAdmin } from './auth.guard.admin';
import { ParticipantComponent } from './participant.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'admin', component: AdminComponent, canActivate: [AuthGuardAdmin] },
  {
    path: 'participant',
    component: ParticipantComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    AdminComponent,
    ParticipantComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes),
    CommonModule,
    ReactiveFormsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpInterceptorService,
      multi: true
    },
    Service
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
