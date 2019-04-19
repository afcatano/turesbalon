import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {ParameterInfo} from '../ParameterInfo';
import {Producto} from '../Models/producto';
import { NgxIndexedDB } from 'ngx-indexed-db';

@Injectable({
  providedIn: 'root'
})
export class UserInfoService {

  private pathGetOverview: string;
  private pathOrder: string;
  private pathRegisterUser: string;
  private pathOrderUser: string;

  constructor(private http: HttpClient) {
    this.pathGetOverview = "/api/statistics/overview";
    this.pathOrder = "/orden/order";
    this.pathRegisterUser = "/usuario/user"
    this.pathOrderUser = "/orden/consulta";
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
      (parameterInfo.isLocal ? parameterInfo.pathApis: "" )+this.pathRegisterUser,
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
      (parameterInfo.isLocal ? parameterInfo.pathApis: "" )+this.pathRegisterUser,
     params.toJSON(),
    { headers: headers}
     );
  }

  //Invoca api que registra datos del usuario
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

  //Invoca api que crea la orden
  odersUserCreate(params): Observable<any> {
    var path = "";

    var producto =new Producto();
  
    console.log(params);

    producto.userid=params.userid
    producto.precio=params.precio;
    producto.Evento=params.Evento;
    producto.Hotel=params.Hotel;
    producto.Transporte=params.Transporte;

    //Verifica que exita la propiedad config
    var param = new ParameterInfo();
    path = param.isLocal ? param.pathApis : param.serve;
    return this.http.post(path + this.pathOrder, producto.toJSON());
  }

  
  initialDb(){
        let db = new NgxIndexedDB('myDb', 1);
        db.openDatabase(1, evt => {
          let objectStore = evt.currentTarget.result.createObjectStore('people', { keyPath: 'id', autoIncrement: true });
      
          objectStore.createIndex('name', 'name', { unique: false });
          objectStore.createIndex('email', 'email', { unique: true });
      });
      db.getByKey('people', 1).then(
        person => {
            console.log(person);
        },
        error => {
            console.log(error);
        }
    );
  }
}
