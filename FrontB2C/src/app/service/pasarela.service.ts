import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {ParameterInfo} from '../ParameterInfo';
import {StorageConfigService} from '../storage/storage-config.service'

@Injectable({
  providedIn: 'root'
})
export class PasarelaService {

  private pathVerify: string;
  private pathPay: string;
 
  constructor(private http: HttpClient ,private config:StorageConfigService) {
  
    this.pathVerify= "/pasarela/verificar";
    this.pathPay= "/pasarela/pagar";
   
   }

   //Invoca pagar tarjeta
   pasarelaPagar(params): Observable<any> {
    var parameterInfo = new ParameterInfo();

    var headers = new HttpHeaders ();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET,POST,PUT,DELETE,OPTIONS');
    headers.append('Access-Control-Allow-Headers', 'Content-Type');
    
    console.log(params);
    console.log("url->"+parameterInfo.isLocal ? parameterInfo.pathApis: "");
    return this.http.post(
      (parameterInfo.isLocal ? parameterInfo.pathApis: "" )+this.pathPay,
     params,
    { headers: headers}
     );
    }

   //Invoca verificar tarjeta
   pasarelaVerificar(params): Observable<any> {
     var parameterInfo = new ParameterInfo();
 
     var headers = new HttpHeaders ();
     headers.append('Content-Type', 'application/json');
     headers.append('Access-Control-Allow-Origin', '*');
     headers.append('Access-Control-Allow-Methods', 'GET,POST,PUT,DELETE,OPTIONS');
     headers.append('Access-Control-Allow-Headers', 'Content-Type');
     
     console.log(params);
     console.log("url->"+parameterInfo.isLocal ? parameterInfo.pathApis: "");
     return this.http.post(
       (parameterInfo.isLocal ? parameterInfo.pathApis: "" )+this.pathVerify,
      params,
     { headers: headers}
      );
     }
}
