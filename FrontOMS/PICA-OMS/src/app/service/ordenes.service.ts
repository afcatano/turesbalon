import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {ParameterInfo} from '../ParameterInfo';
import { Orders} from '../Models/Orders'
import {StorageConfigService} from '../storage/storage-config.service'
@Injectable({
  providedIn: 'root'
})
export class OrdenesService {

  private pathOrder: string;
  private pathOrderUser: string;
  private Ordenes: Orders[];

  constructor(private http: HttpClient) {
    this.pathOrder = "/orden/order";
    this.pathOrderUser = "/orden/consulta";
   }


  //Invoca api que consulta las ordenes del usuario 
  OrdenesAPI(params): Observable<any> {
    var path = "";
    console.log(params);
    //Verifica que exita la propiedad config
    var param = new ParameterInfo();
    var headers = new HttpHeaders ();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET,POST,PUT,DELETE,OPTIONS');
    headers.append('Access-Control-Allow-Headers', 'Content-Type');
    path = param.isLocal ? param.pathApis : param.serve;
    return this.http.post(path + this.pathOrderUser, params.toJSON(),  
    { headers: headers}
    );
  }

  paginador(params,callback){
    var dataObject=[];
    var dataArrayObject=[];
    dataArrayObject= this.Ordenes;
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

   getOrdenes(params, callback){
     this.OrdenesAPI(params).subscribe(
          result => {
                    var date="";
                      if(result.codigo=="0") {
                       this.Ordenes = result.Orders;
                       console.log("Invocar paginador Ordenes");
                       this.paginador(params,result =>{
                        var retono= {Orders:result,paginaActual:params.Pagina,cantidadRegistros:result[0].TotalRegistros,tamanoPagina:params.RegistroXPagina,codigo:'0' }
                        console.log(retono);
                        callback(retono);
                      });
                      } else {
                        var retono= {mensaje:result.mensaje,paginaActual:params.Pagina,cantidadRegistros:0,tamanoPagina:params.RegistroXPagina, codigo:result.codigo }
                        callback(retono);
                        console.log(JSON.stringify(result, null, 4));
                      }
                    },
                    error => {
                      console.log("Error al consultar Ordenes:" +error);
                      console.log(error);
                      callback(error);
      })
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
