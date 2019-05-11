import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {ParameterInfo} from '../ParameterInfo';
import {StorageConfigService} from '../storage/storage-config.service'
import {Evento} from '../Models/evento';
import {Eventos} from '../mock/mockEventos';

@Injectable({
  providedIn: 'root'
})
export class ProductosService {

  private pathHotels: string;
  private pathCampaign: string;
  private pathProducts: string;
 
   constructor(private http: HttpClient , private config:StorageConfigService) {
   
     this.pathProducts= "/producto/consulta";
 
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

    getProductos(params, callback){
      var config= this.config.getConfigSession();
      //Valida si aplica mock
      if(config.productos){
       //Solo entra si esta en modo dumy
       this.paginadorDumy(params,result =>{
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
    }
  
     //Invoca api que consulta los productos
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
  
}
