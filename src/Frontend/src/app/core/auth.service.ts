import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, tap} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl:string = "insert_url_here";

  constructor(private http:HttpClient) { }

  public loginWithCredentials(username:string, password:string, saveCredentials:boolean):Observable<AuthDto> {
    console.log(`AuthService::loginWithCredentials ${username}`);
    return this.http.post<AuthDto>(`${this.baseUrl}/`, {
      username: username, password: password
    }).pipe(
      tap(user => {
        if (user && user.token && saveCredentials) {
          sessionStorage.setItem('userToken', JSON.stringify(user));
        }
      })
    )
  }

  public logout():void {
    console.log(`AuthService::logout`);
    sessionStorage.removeItem('userToken');
  }
}
