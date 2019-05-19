import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {ParameterInfo} from '../ParameterInfo';
import {StorageConfigService} from '../storage/storage-config.service'
import { Cliente } from '../Models/Customer';

@Injectable({
  providedIn: 'root'
})
export class ClientesService {
  
  private pathUser: string;
  private pathCustomer: string;
  private Clientes: Cliente[];

  constructor(private http: HttpClient) {
    this.pathUser = "/usuario/user"
    this.pathCustomer = "/usuario/consulta"
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

  
   //Invoca api que consulta clientes
   CustomerAPI(params): Observable<any> {
    //this.initialDb();
    var parameterInfo = new ParameterInfo();
    var headers = new HttpHeaders ();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET,POST,PUT,DELETE,OPTIONS');
    headers.append('Access-Control-Allow-Headers', 'Content-Type');
    //console.log(params.toJSON());
    console.log("url->"+parameterInfo.isLocal ? parameterInfo.pathApis: "");
    return this.http.post(
      (parameterInfo.isLocal ? parameterInfo.pathApis: "" )+this.pathCustomer,
     params.toJSON(),
    { headers: headers}
     );
  }

  paginador(params,callback){
    var dataObject=[];
    var dataArrayObject=[];
    dataArrayObject= this.Clientes;
    var count=0;
    var rangoInicial=0;
    //params.page= params.page==0?1:params.page;
    //rangoInicial=(params.Pagina - 1)* params.RegistroXPagina; //14
    //var rangoFinal=rangoInicial + params.RegistroXPagina;//21
   
    dataArrayObject.forEach(element => {
     // if((rangoInicial <= count) && (rangoFinal > count))
          dataObject.push(element);
        //count=count+1;
    });
    callback(dataObject);
   }

   getClientes(params, callback){
     this.CustomerAPI(params).subscribe(
          result => {
                    var date="";
                      if(result.codigo=="0") {
                       // console.log(JSON.stringify(result, null, 4));
                       this.Clientes = result.Clientes;
                      } else {
                        console.log(JSON.stringify(result, null, 4));
                      }
                      console.log("Entra al api de Consultar Clientes" );
                      //callback(result);
                      this.paginador(params,result =>{
                        var retono= {clientes:result,paginaActual:params.Pagina,cantidadRegistros:result[0].TotalRegistros,tamanoPagina:params.RegistroXPagina,codigo:'0' }
                        console.log(retono);
                        console.log("Invocar paginador Clientes");
                        callback(retono);
                      });
                    },
                    error => {
                      console.log("Error al consultar clientes:" +error);
                      console.log(error);
                      callback(error);
      })
    }
  }
/*
    getProductos(params, callback){
      var config= this.config.getConfigSession();
      //Valida si aplica mock
      if(config.productos){
       //Solo entra si esta en modo dumy
       this.paginador(params,result =>{
          var retono= {eventos:result,paginaActual:params.pagina,cantidadRegistros:Eventos.length,tamanoPagina:params.tamanoPagina,codigo:'0' }
          console.log(retono);
          console.log("Invocar mock servicio eventos");
          callback(retono);
        });
      }else{
       this.eventos(params).subscribe(
            result => {
                      var date="";
                        if(result.codigo=="0") {
                         // console.log(JSON.stringify(result, null, 4));
                        } else {
                          console.log(JSON.stringify(result, null, 4));
                        }
                        console.log("Entra al api de buscar productos" );
                        callback(result);
                      },
                      error => {
                        console.log("Error al consultar productos:" +error);
                        console.log(error);
                        callback(error);
        })
      }
  */

