import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
import { firstValueFrom, Observable } from 'rxjs';
import { TokenResponse } from 'src/app/contracts/token/tokenResponse';

@Injectable({
  providedIn: 'root'
})
export class UserAuthService {

  constructor(private httpClientService : HttpClientService, private toastrService : CustomToastrService) { }

  async login(UsernameOrEmail, Password, callBackFunction? : () => void) : Promise<any>
  {
    const  observable : Observable<any | TokenResponse> = this.httpClientService.post<any | TokenResponse>({
      controller : "auth",
      action : "login"
    }, {UsernameOrEmail, Password})

    const tokenResponse : TokenResponse =  await firstValueFrom(observable) as TokenResponse;
    if(tokenResponse)
    {
      localStorage.setItem("accessToken", tokenResponse.token.accessToken);
      localStorage.setItem("refreshToken", tokenResponse.token.refreshToken);
      
      console.log("login oldun!")

      this.toastrService.message("Yönetim Paneline Yönlendiriliyorsunuz...", "Giriş Başarılı",
        {
          messageType : ToastrMessageType.Success,
          position : ToastrPosition.TopRight
        })
    }

    callBackFunction();
  }

  async refreshTokenLogin(refreshToken : string, callBackFunction? : () => void) : Promise<any>
  {
    const observable : Observable<any | TokenResponse> = this.httpClientService.post({
      action : "refreshtokenlogin",
      controller : "auth"
    }, {refreshToken : refreshToken});

    const tokenResponse : TokenResponse = await firstValueFrom(observable) as TokenResponse;

    if(tokenResponse)
    {
      localStorage.setItem("accessToken", tokenResponse.token.accessToken);
      localStorage.setItem("refreshToken", tokenResponse.token.refreshToken);
      
    }

    callBackFunction();
  }
}
