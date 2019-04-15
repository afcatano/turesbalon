import { Component, OnInit,ViewChild ,Input,Output } from '@angular/core';
import { MatPaginator } from '@angular/material';
import {User } from '../../Models/User';
import {Evento } from '../../Models/evento';
import {paqueteInfo} from '../../Models/paqueteInfo';
import {ProductsService} from './../../service/products.service';
import {Producto}from '../../Models/producto';
import {CarritoService} from '../../service/carrito.service';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-paquetes',
  templateUrl: './paquetes.component.html',
  styleUrls: ['./paquetes.component.css']
})
export class PaquetesComponent implements OnInit {

  @Input() page: paqueteInfo;


  @ViewChild(MatPaginator) paginator: MatPaginator;
 
  private subscription: Subscription;

  params = {
    tmIni: null,
    tmFin: null,
    evento: null,
    cantidad: null,
    categoria: null,
    operador: null,
    //Variable para almacenar la pagina actual
    page: 0,
    // Variable para almacenar la cantidad de resultados por pagina
    pageSize: 10
  }
  dataCount: number=0;//tamaño para el paginador
  dataEventos:Evento[];
  
  constructor(private serviceProduct :ProductsService,
    private carritoService:CarritoService) {


   }

  ngOnInit() {
  }

  //Metodo que se ejecuta cuando cambia la pagina
  onPaginateChange(event) {
    this.params.page = event.pageIndex;
    this.params.pageSize = event.pageSize;
    this.getDataPaquetes();
  }

  action(data:paqueteInfo){
     console.log("Llega evento");
     console.log(data);
     this.getDataPaquetes();
  }

  getDataPaquetes() {
    let parametros = this.params
   // this.dataEventos=Eventos;
    //console.log(this.dataEventos);
    console.log("getDataSource()", parametros);
    /*this.api.getDataSource(parametros).subscribe((data) => {
      console.log("getDataSource()", data.data);
      this.size = data.total;
      let pend: number = 0;
      let bloq: number = 0
    });
    */
    this.serviceProduct.getEventos(parametros,result =>{
     
      if(result.data)
          this.dataEventos=result.data;

         this.dataCount=result.size;
         this.params.page = result.page;
         this.params.pageSize = result.pageSize;
         console.log(this.dataEventos);
    });
    
  }

  addProducto(item) {
    console.log(item);
    var producto = new Producto();
    producto.evento=item; 
    this.carritoService.addCarrito(producto);
    }

  
}
