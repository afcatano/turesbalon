import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {ParameterInfo} from '../ParameterInfo';
import {StorageConfigService} from '../storage/storage-config.service'
@Injectable({
  providedIn: 'root'
})
export class OrdenesService {

  private pathOrder: string;
  private pathOrderUser: string;

  constructor(private http: HttpClient) {
    this.pathOrder = "/orden/order";
    this.pathOrderUser = "/orden/consulta";
   }


  //Invoca api que consulta las ordenes del usuario 
  odersUser(params): Observable<any> {
    var path = "";
    console.log(params);
    //Verifica que exita la propiedad config
    var param = new ParameterInfo();
    path = param.isLocal ? param.pathApis : param.serve;
    return this.http.post(path + this.pathOrderUser, params);
  }

  //Invoca api que actualiza la orden
  odersUserUpdate(params): Observable<any> {
    var path = "";
    console.log(params);
    //Verifica que exita la propiedad config
    var param = new ParameterInfo();
    path = param.isLocal ? param.pathApis : param.serve;
    return this.http.put(path + this.pathOrder, params);
  }

}
