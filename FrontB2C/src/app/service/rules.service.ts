import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {ParameterInfo} from '../ParameterInfo';
import {StorageConfigService} from '../storage/storage-config.service'


@Injectable({
  providedIn: 'root'
})
export class RulesService {

  
  private pathTransport: string;
  private pathHotel: string;
 
   constructor(private http: HttpClient ,private config:StorageConfigService) {
   
     this.pathTransport= "/brms/transporte";
     this.pathHotel= "/brms/hotel";
    
    }
 


    //Invoca reglas de Hotel
    hotel(params): Observable<any> {
      var parameterInfo = new ParameterInfo();
  
      var headers = new HttpHeaders ();
      headers.append('Content-Type', 'application/json');
      headers.append('Access-Control-Allow-Origin', '*');
      headers.append('Access-Control-Allow-Methods', 'GET,POST,PUT,DELETE,OPTIONS');
      headers.append('Access-Control-Allow-Headers', 'Content-Type');
      
      console.log(params);
      console.log("url->"+parameterInfo.isLocal ? parameterInfo.pathApis: "");
      return this.http.post(
        (parameterInfo.isLocal ? parameterInfo.pathApis: "" )+this.pathHotel,
       params,
      { headers: headers}
       );
      }

    //Invoca reglas de transporte
    transporte(params): Observable<any> {
      var parameterInfo = new ParameterInfo();
  
      var headers = new HttpHeaders ();
      headers.append('Content-Type', 'application/json');
      headers.append('Access-Control-Allow-Origin', '*');
      headers.append('Access-Control-Allow-Methods', 'GET,POST,PUT,DELETE,OPTIONS');
      headers.append('Access-Control-Allow-Headers', 'Content-Type');
      
      console.log(params);
      console.log("url->"+parameterInfo.isLocal ? parameterInfo.pathApis: "");
      return this.http.post(
        (parameterInfo.isLocal ? parameterInfo.pathApis: "" )+this.pathTransport,
       params,
      { headers: headers}
       );
      }
}
