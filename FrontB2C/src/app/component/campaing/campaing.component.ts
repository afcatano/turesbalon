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
  constructor(private serviceProduct:ProductsService,private route: Router, private storageCompra:StorageParamsCompraService) { }

  ngOnInit() {
    this.getCammpaign();
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
}
