import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {ParameterInfo} from '../ParameterInfo';
import {Evento} from '../Models/evento';
import {Eventos} from '../mock/mockEventos';
import {StorageConfigService} from '../storage/storage-config.service'

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

 private pathHotels: string;

  constructor(private http: HttpClient ,private config:StorageConfigService) {
  
    this.pathHotels= "/api/products/hotels"
   }


   //Dumy paginador
   paginadorDumy(params,callback){
    var data= Eventos;
    var dataObject=[];
    var dataArrayObject=[];
    dataArrayObject=Eventos;
    var total = Eventos.length;
    var count=0;
    var rangoInicial=0;
    //params.page= params.page==0?1:params.page;
    rangoInicial=(params.page)* params.pageSize; //14
    var rangoFinal=rangoInicial + params.pageSize;//21
   
    dataArrayObject.forEach(element => {
      if((rangoInicial <= count) && (rangoFinal > count))
          dataObject.push(element);

        count=count+1;
    });
    callback(dataObject);
   }

   getEventos(params, callback){

    var config= this.config.getConfigSession();

    //Valida si aplica mock
    if(config.eventos){
     //Solo entra si esta en modo dumy
      this.paginadorDumy(params,result =>{
        var retono= {data:result,page:params.page,size:Eventos.length,pageSize:params.pageSize }
        console.log(retono);
        console.log("Invocar servicio eventos");
        callback(retono);
      });
    }
    /*
      this.eventos(params).subscribe(
          result => {
                    var date="";
                      if(result.code=="0") {
                        
                      } else {
                        console.log(JSON.stringify(result, null, 4));
                      }
                      callback();
                    },
                    error => {
                      console.log("Error al actualizar la ultima fecha del localstorage:" +error);
                        console.log(error);
                        callback();
      })*/



   }

   //Invoca api que consulta los eventos
  eventos(params): Observable<any> {
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
