import { Injectable } from '@angular/core';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';

export class Auth {
  loggedIn: Boolean = false;
}
export class AuthUser {
  email: string = '';
  role: string = '';
  isLoggedIn: boolean = false;
}
export class ConventionRelatedData {
  characters: Character[] = [];
  speakers: Speaker[] = [];
}
export class Character {
  name: string = '';
  description: string = '';
}
export class NewConvention {
  name: string = '';
  topic: string = '';
  maxCap: number = 0;
  isFull: boolean = false;
  description: string = '';
  speakerId: string = '';
}
export class Convention {
  name: string = '';
  topic: string = '';
  isFull: string = '';
  hasSpeaker: string = '';
  id: string = '';
}
export class User {
  name: string = '';
  email: string = '';
  password: string = '';
  role: string = '';
  address: Address = null;
}
export class LoginRequest {
  email: string = '';
  password: string = '';
}
export class Address {
  city: string = '';
  road: string = '';
  number: string = '';
  postalCode = '';
}
export class Speaker {
  id: string = '';
  email: string = '';
  name: string = '';
}
@Injectable({
  providedIn: 'root'
})
export class Service {
  private marvelConventionUrl = 'https://localhost:44376/api/marvelconvention'; // URL to web api
  private userManagementUrl = 'https://localhost:44376/api/usermanagement'; //Url to usermanangement api
  private KEY_BEARER_TOKEN = 'bearerToken';
  public userData = new BehaviorSubject<AuthUser>(new AuthUser());

  constructor(private http: HttpClient, private router: Router) {}

  login(loginRequest: LoginRequest): Observable<AuthUser> {
    return this.http
      .post<any>(this.userManagementUrl + '/login', loginRequest)
      .pipe(
        map(resp => {
          debugger;

          localStorage.setItem('accessToken', resp.token);
          window.sessionStorage.setItem(this.KEY_BEARER_TOKEN, resp.token);
          this.setUserDetails();
          return resp;
        })
      );
  }

  setUserDetails() {
    if (localStorage.getItem('accessToken')) {
      const decodeUserDetails = JSON.parse(
        window.atob(localStorage.getItem('accessToken').split('.')[1])
      );

      var user = new AuthUser();
      user.email = decodeUserDetails.sub;
      user.role = decodeUserDetails.role;
      user.isLoggedIn = true;
      this.userData.next(user);
    }
  }

  getConventions(): Observable<Convention[]> {
    return this.http
      .get<Convention[]>(this.marvelConventionUrl + '/getconventions')
      .pipe();
  }

  getConventionRelatedData(): Observable<ConventionRelatedData> {
    return this.http
      .get<ConventionRelatedData>(
        this.marvelConventionUrl + '/getConventionRelatedData'
      )
      .pipe();
  }

  createConvention(convention: NewConvention): Observable<NewConvention> {
    return this.http
      .post<NewConvention>(
        this.marvelConventionUrl + '/createconvention',
        convention
      )
      .pipe();
  }

  createUser(user: User): Observable<User> {
    return this.http
      .post<User>(this.userManagementUrl + '/createuser', user)
      .pipe();
  }
  logout() {
    localStorage.removeItem('accessToken');
    this.router.navigate(['/login']);
    this.userData.next(new AuthUser());
  }

  allocateConvention(id: string, reserve: boolean): Observable<any> {
    return this.http
      .post<any>(this.marvelConventionUrl + '/allocateConvention', {
        conventionId: id,
        isReserved: reserve
      })
      .pipe();
  }
}

//private handleError<T>(operation = 'operation', result?: T) {
//  return (error: any): Observable<T> => {
//    // TODO: send the error to remote logging infrastructure
//    console.error(error); // log to console instead

//    // TODO: better job of transforming error for user consumption
//    //this.log(`${operation} failed: ${error.message}`);

//    // Let the app keep running by returning an empty result.
//    return of(result as T);
//  };
//}

/** Log a HeroService message with the MessageService */
