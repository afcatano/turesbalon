import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {ParameterInfo} from '../ParameterInfo';
import {StorageConfigService} from '../storage/storage-config.service'
@Injectable({
  providedIn: 'root'
})
export class ClientesService {

  private pathUser: string;

  constructor(private http: HttpClient) {
    this.pathUser = "/usuario/user"
   }




   
   //Invoca api que actualiza datos del usuario
  updateUser(params): Observable<any> {
    //this.initialDb();
    var parameterInfo = new ParameterInfo();
    var headers = new HttpHeaders ();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET,POST,PUT,DELETE,OPTIONS');
    headers.append('Access-Control-Allow-Headers', 'Content-Type');
    console.log(params.toJSON());
    console.log("url->"+parameterInfo.isLocal ? parameterInfo.pathApis: "");
    return this.http.put(
      (parameterInfo.isLocal ? parameterInfo.pathApis: "" )+this.pathUser,
     params.toJSON(),
    { headers: headers}
     );
  }

  //Invoca api que registra datos del usuario
  registerUser(params): Observable<any> {
    
    var parameterInfo = new ParameterInfo();
    var headers = new HttpHeaders ();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET,POST,PUT,DELETE,OPTIONS');
    headers.append('Access-Control-Allow-Headers', 'Content-Type');
    
    console.log(params.toJSON());
    console.log("url->"+parameterInfo.isLocal ? parameterInfo.pathApis: "");
    return this.http.post(
      (parameterInfo.isLocal ? parameterInfo.pathApis: "" )+this.pathUser,
     params.toJSON(),
    { headers: headers}
     );
  }
}
