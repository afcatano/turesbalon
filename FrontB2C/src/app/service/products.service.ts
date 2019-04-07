import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {ParameterInfo} from '../ParameterInfo';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

 private pathHotels: string;

  constructor(private http: HttpClient) {
  
    this.pathHotels= "/api/products/hotels"
   }

  //Invoca api que registra datos del usuario
  hotels(params): Observable<any> {
    var path = "";
    //Verifica que exita la propiedad config
    var parameterInfo = new ParameterInfo();
   var headers = new HttpHeaders ();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET,POST,PUT,DELETE,OPTIONS');
    headers.append('Access-Control-Allow-Headers', 'Content-Type');
    
    //console.log(params.toJSON());
    console.log("url->"+parameterInfo.isLocal ? parameterInfo.pathApis: "");
    return this.http.post(path + this.pathHotels,{ headers: headers});
  }

}
