import { Component, OnInit,ViewChild ,Input,Output } from '@angular/core';
import { MatPaginator } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import {User } from '../../Models/User';
import {paqueteInfo} from '../../Models/paqueteInfo';
import {ProductsService} from '../../service/products.service';

import {CarritoService} from '../../service/carrito.service';
import { Subscription } from 'rxjs';
import {MatDialog} from '@angular/material';
import {DatalleEventoComponent} from '../datalle-evento/datalle-evento.component';
import { Router } from '@angular/router';
import {StorageParamsCompraService} from '../../storage/storage-compra'
import {Evento} from '../../Models/evento';
import {Producto} from '../../Models/producto';
@Component({
  selector: 'app-paquetes',
  templateUrl: './paquetes.component.html',
  styleUrls: ['./paquetes.component.css']
})
export class PaquetesComponent implements OnInit {

  @Input() page: paqueteInfo;


  @ViewChild(MatPaginator) paginator: MatPaginator;
 

  private subscription: Subscription;
  progressBar=false
  session:any;
  optionActual="E"; //Para identificar que esta en la pagina evento
  params = {
    tmIni: null,
    tmFin: null,
    evento: null,
    cantidad: 0,
    categoria: null,
    operador: null,
    optionPaquete:null,
    nombrePaso:null,
    opionPaquete:null,
    routers:[],
    //Variable para almacenar la pagina actual
    page: 0,
    // Variable para almacenar la cantidad de resultados por pagina
    pageSize: 12,
    accion:""
  }
  dataCount: number=0;//tamaño para el paginador
  dataEventos:Evento[];
  
  constructor(private serviceProduct :ProductsService,
    private carritoService:CarritoService,
  private route:ActivatedRoute, public dialog: MatDialog, private router: Router, private storageCompra:StorageParamsCompraService) {


   }

  ngOnInit() {
    this.route.params
      .subscribe(params => {
        if(params['evento'])
           this.params.evento = params['evento'].toString();
        if(params['categoria'])
           this.params.categoria  = params['categoria'].toString();
        if(params['cantidad'])
           this.params.cantidad  = params['cantidad'].toString();
        if(params['option'])
           this.params.optionPaquete= params['option'].toString();
        if(this.params.categoria || this.params.evento)
          this.params.accion="buscar";

         this.session = this.storageCompra.getParamsCompraSession();
         this.params.opionPaquete=this.params.optionPaquete
         this.params.nombrePaso= this.optionActual;//esta en el paso de consultar eventos
         console.log(this.params);

      });

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
    this.progressBar=true;
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
         this.progressBar=false;
    });
    
  }

  addProducto(item) {
    console.log(item);
    var route="carro";
    var producto = new Producto();
    producto.Evento=item; 
    item.cantidadPersonas=this.params.cantidad;
    this.carritoService.addCarrito(producto);

    //Valida si hay session para enrutar
      if(this.session)
          this.session.routers.forEach(element => {
            if(element.optionActual==this.optionActual){
              route=element.route;
            }
          });

      this.session.orden = producto;
      this.storageCompra.setParamsCompraSession(this.session);
      this.router.navigate([route]);
    }

    openDetail( item): void {

      var orden = new Producto();
      var  evento= new Evento();
      orden.Evento = item;
      orden.tipoDetalle="Evento";
      orden.codigo=item.CodigoEvento;
      console.log(orden);
      const dialogRef = this.dialog.open(DatalleEventoComponent, {
        width: '70%',
        data: orden
      });
  
      dialogRef.afterClosed().subscribe(result => {
        console.log('The dialog was closed');
      });
    }
  
}
