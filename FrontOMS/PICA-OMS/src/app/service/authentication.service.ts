import { Injectable } from '@angular/core';
import { HttpClient ,HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import {ParameterInfo} from "../ParameterInfo";

@Injectable({
  providedIn: 'root'
})
//Servicio encargado de consultar la autenticacion del usuario
export class AuthenticationService {

  private authAPI:string;

  constructor(private http:HttpClient) {
    this.authAPI = "/login/usuario";

    
  }

  signIn(user): Observable<any> {
    var parameterInfo = new ParameterInfo();

    var headers = new HttpHeaders ();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET,POST,PUT,DELETE,OPTIONS');
    headers.append('Access-Control-Allow-Headers', 'Content-Type');
    
    console.log(user.toJSON());
    console.log("url->"+parameterInfo.isLocal ? parameterInfo.pathApis: "");
    return this.http.post(
      (parameterInfo.isLocal ? parameterInfo.pathApis: "" )+this.authAPI,
     user.toJSON(),
    { headers: headers}
     );
  }

}
