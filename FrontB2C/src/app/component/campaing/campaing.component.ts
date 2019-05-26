import { Component, OnInit } from '@angular/core';
import { ProductsService} from '../../service/products.service';
import { buscadorPaginacion} from '../../Models/busquedaPaginacion';
import {StorageParamsCompraService} from '../../storage/storage-compra'
import { Router } from '@angular/router';
@Component({
  selector: 'app-campaing',
  templateUrl: './campaing.component.html',
  styleUrls: ['./campaing.component.css']
})
export class CampaingComponent implements OnInit {
  progressBar=false;
  campaing:any[];
  topProduct:any[];

  constructor(private serviceProduct:ProductsService,private route: Router, private storageCompra:StorageParamsCompraService) { }

  ngOnInit() {
    this.getCammpaign();
    this.getTopProducts();
  }

  getCammpaign(){
    this.progressBar= true;
    var data=new buscadorPaginacion();
    data.pagina=0;
    data.tamanoPagina=20;
    this.serviceProduct.getCampaing(data,result =>{
     
      if(result.data){
         this.campaing=result.data;
         this.progressBar=false;
      }else{

      }
      this.progressBar=false;
    });
  }


  getTopProducts(){

    var params={
      "FechaInicio": "2019-03-31",
      "FechaFin": "2019-07-17",
      "ConDetalle": "true",
      "TipoConsulta": "TOP-5"
    }

    this.serviceProduct.topProducts(params).subscribe(result =>{
     if(result.codigo=="0"){
         this.topProduct = result.Orders;
         var countImagen=1;
         this.topProduct.forEach(element => {
          element.imagen="../../../assets/paquetes/Captura"+countImagen+".PNG";
          countImagen=countImagen+1;
         var data=new buscadorPaginacion();
          data.pagina=1;
          data.tamanoPagina=1;
          data.nombre='CO_'+element.Evento.CodigoEvento;
          data.fechaInicial="2017-09-09";//TODO
          data.fechaFinal="2022-09-09";//TODO
          data.codigo ='CO_'+element.Evento.CodigoEvento; //TODO - Categoria
          /*this.serviceProduct.getEventos(data,result =>{
            if(result.codigo=='0'){
               if(result.eventos){
                 if(result.eventos[0])
                   element.imagen=  result.eventos[0].imagen;
                }
              }
          });*/
         });
      }else{

      }
    });
  }


  verEvento(item){
   var  params={
      categoria:"Futbol",//TODO
      evento:"",
      cantidad:"1",
      optionPaquete:"E",
      routers:[]
    }
    params.optionPaquete='E';
    params.routers=[{route:"carro",optionActual:"E"}];
    this.storageCompra.setParamsCompraSession(params);
    this.route.navigate(['paquetes', params.categoria,item.codigo,params.cantidad, params.optionPaquete]);
  }

  verEventoTOP(codigo){
    console.log("Codigo top "+codigo);
    var  params={
       categoria:"Futbol",//TODO
       evento:"",
       cantidad:"1",
       optionPaquete:"E",
       routers:[]
     }
     params.optionPaquete='E';
     params.routers=[{route:"carro",optionActual:"E"}];
     this.storageCompra.setParamsCompraSession(params);
     this.route.navigate(['paquetes', params.categoria,codigo,params.cantidad, params.optionPaquete]);
   }
}
