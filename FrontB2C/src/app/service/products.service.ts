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
 private pathCampaign: string;
 private pathProducts: string;
 private pathOrden: string;
 
  constructor(private http: HttpClient ,private config:StorageConfigService) {
  
    this.pathHotels= "/api/products/hotels";
    this.pathCampaign= "/campanas/consulta";
    this.pathProducts= "/producto/consulta";
    this.pathOrden= "/orden/consulta";
    

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
    rangoInicial=(params.pagina)* params.tamanoPagina; //14
    var rangoFinal=rangoInicial + params.tamanoPagina;//21
   
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
        var retono= {data:result,page:params.pagina,size:Eventos.length,pageSize:params.tamanoPagina }
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
                      console.log("Entra al api de buscar eventos" );
                      callback(result);
                    },
                    error => {
                      console.log("Error al consultar eventos:" +error);
                      console.log(error);
                      callback(error);
      })
    }



   }


  //Top products
  topProducts(params): Observable<any> {
    var parameterInfo = new ParameterInfo();

    var headers = new HttpHeaders ();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET,POST,PUT,DELETE,OPTIONS');
    headers.append('Access-Control-Allow-Headers', 'Content-Type');
    
    console.log(params);
    console.log("url->"+parameterInfo.isLocal ? parameterInfo.pathApis: "");
    return this.http.post(
      (parameterInfo.isLocal ? parameterInfo.pathApis: "" )+this.pathOrden,
     params,
    { headers: headers}
     );
    }

   //Invoca api que consulta los eventos
  eventos(params): Observable<any> {
    var parameterInfo = new ParameterInfo();

    var headers = new HttpHeaders ();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET,POST,PUT,DELETE,OPTIONS');
    headers.append('Access-Control-Allow-Headers', 'Content-Type');
    
    console.log(params);
    console.log("url->"+parameterInfo.isLocal ? parameterInfo.pathApis: "");
    return this.http.post(
      (parameterInfo.isLocal ? parameterInfo.pathApis: "" )+this.pathProducts,
     params,
    { headers: headers}
     );
    }

  
  getCampaing(params, callback){

    var config= this.config.getConfigSession();
    console.log("Entra a buscar campañas" );
    //Valida si aplica mock
    if(config.campaing){
      var result={codigo:"01", mensaje:""};
     //Solo entra si esta en modo dumy
     console.log("Entra al mock de buscar campañas" );
     callback(result);
    }else{
    
      this.campaign(params).subscribe(
          result => {
                    var date="";
                      if(result.codigo=="0") {
                       // console.log(JSON.stringify(result, null, 4));
                      } else {
                        console.log(JSON.stringify(result, null, 4));
                      }
                      console.log("Entra al api de buscar campañas" );
                      callback(result);
                    },
                    error => {
                      console.log("Error al consultar campañas:" +error);
                      console.log(error);
                      callback(error);
      })
    }
 }

  //Invoca api que consulta las campañas
  campaign(params): Observable<any> {
    var parameterInfo = new ParameterInfo();

    var headers = new HttpHeaders ();
    headers.append('Content-Type', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');
    headers.append('Access-Control-Allow-Methods', 'GET,POST,PUT,DELETE,OPTIONS');
    headers.append('Access-Control-Allow-Headers', 'Content-Type');
    
    console.log(params);
    console.log("url->"+parameterInfo.isLocal ? parameterInfo.pathApis: "");
    return this.http.post(
      (parameterInfo.isLocal ? parameterInfo.pathApis: "" )+this.pathCampaign,
     params,
    { headers: headers}
     );
    }
}
