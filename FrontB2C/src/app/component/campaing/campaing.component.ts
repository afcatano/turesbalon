import { Component, OnInit } from '@angular/core';
import { ProductsService} from '../../service/products.service';
import { buscadorPaginacion} from '../../Models/busquedaPaginacion';
@Component({
  selector: 'app-campaing',
  templateUrl: './campaing.component.html',
  styleUrls: ['./campaing.component.css']
})
export class CampaingComponent implements OnInit {
  progressBar=false;
  campaing:any[];
  constructor(private serviceProduct:ProductsService) { }

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
}
