import {Injectable} from '@angular/core';
import {SocialUser} from "@abacritt/angularx-social-login";
import {CookieService} from "ngx-cookie-service";
import {HttpClient} from "@angular/common/http";
import {environment} from "src/environments/environment";
import {urlJoin} from "@angular-devkit/build-angular/src/utils";
import {firstValueFrom} from "rxjs";
import {AuthRequestModel} from "../models/authRequestModel";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUri = environment.apiEndpoint;

  constructor(private _cookieService: CookieService, private http: HttpClient) {
  }

  public getUser(): SocialUser | undefined {
    // TODO

    return undefined;
  }

  public async authUser(authRequest: AuthRequestModel): Promise<string | undefined> {
    try {
      // Fetch user token from backend
      let authToken = await firstValueFrom(this.http.post<string>(urlJoin(this.apiUri, "auth"), authRequest));

      // Set token to cookies
      this._cookieService.set("auth_token", authToken);

      // Return token
      return authToken

    } catch (error) {
      console.error(error);
      return undefined;
    }
  }

}