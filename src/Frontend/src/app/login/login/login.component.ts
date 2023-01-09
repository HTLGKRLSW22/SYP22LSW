import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

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

  constructor(private router: Router) { }

  onBtnLoginClick(): void {
    if (this.username !== null && this.username !== '' && this.password !== null && this.password !== '' && this.isRemembered !== null) {
      // TODO JWT-Auth

      // Test Weiterleitung zu Teacher View Seite
      this.router.navigate([environment.teacherRouting, environment.viewRouting]);
    }
  }
}
