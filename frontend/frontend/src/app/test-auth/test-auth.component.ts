import { Component } from '@angular/core';

import {
  GoogleLoginProvider, SocialAuthService,
} from '@abacritt/angularx-social-login';

@Component({
  selector: 'app-test-auth',
  templateUrl: './test-auth.component.html',
  styleUrls: ['./test-auth.component.scss']
})
export class TestAuthComponent {
  user: any;
  loggedIn: any;
  constructor(private authService: SocialAuthService) { }

  ngOnInit() {
    this.authService.authState.subscribe((user) => {
      this.user = user;
      this.loggedIn = (user != null);
      console.log(this.user)
    });
  }


  signInWithGoogle(): void {
    const headers = { 'Referrer-Policy': 'strict-origin-when-cross-origin' };
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID, { headers }).then((user) => {
      this.user = user;
      this.loggedIn = true;
      console.log(this.user);
    });
  }
}
