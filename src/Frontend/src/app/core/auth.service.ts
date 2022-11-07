import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl:string = "insert_url_here";

  constructor(private http:HttpClient) { }

  public loginWithCredentials(username:string, password:string):Observable<AuthDto> {
    console.log(`AuthService::loginWithCredentials ${username}`);
    return this.http.post<AuthDto>(`${this.baseUrl}/`, {
      username: username, password: password
    });
  }
}
