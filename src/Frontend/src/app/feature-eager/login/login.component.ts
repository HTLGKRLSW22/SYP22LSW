import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  username: string | null = '';
  password: string | null = '';
  isRemembered: boolean | null = false;

  ngOnInit(): void {
    console.log('Login works');
  }

  onBtnLoginClick(): void {
    if (this.username !== null && this.username !== '' && this.password !== null && this.password !== '' && this.isRemembered !== null) {
      // TODO JWT-Auth
    }
  }
}
