import { Component, OnInit } from '@angular/core';
import {Vuelos } from '../../mock/vuelos';
import {Session } from '../../Models/session';
import { StorageService } from '../../storage/storage.service';
import {User } from '../../Models/User';
import {ProductsService} from '../../service/products.service'
import {CarritoService} from '../../service/carrito.service';
import {AppComponent} from '../../app.component';
import {Producto}from '../../Models/producto';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import {StorageParamsCompraService} from '../../storage/storage-compra'
import { transporte } from '../../Models/transporte';
import {MatDialog} from '@angular/material';
import {DatalleEventoComponent} from '../datalle-evento/datalle-evento.component';

@Component({
  selector: 'app-vuelos',
  templateUrl: './vuelos.component.html',
  styleUrls: ['./vuelos.component.css']
})
export class VuelosComponent implements OnInit {
  progressBar=false
  userInfo :User;
  ordenesCount:number;
  private subscription: Subscription;
  optionActual="T";
  session:any;
  infoTable: any[];
  processing:boolean;
  params:any;
  constructor(private sesion:StorageService, private productService: ProductsService, private dialog: MatDialog,
     private parent: AppComponent, private carritoService:CarritoService, private router: Router, private storageCompra:StorageParamsCompraService) { 

  }

  ngOnInit() {
    console.log("Inicia hotel");
    this.session = this.storageCompra.getParamsCompraSession();
    this.params={};
    this.params.opionPaquete=this.session.optionPaquete
    this.params.nombrePaso= this.optionActual;//esta en el paso de consultar eventos
   
    this.action();
  }

  action(){
    console.log("Eschua evento");
    this.processing=true;
    //console.log(this.username, this.password);
    var params={fecha:""}
     this.infoTable = Vuelos;
      
  }

  private correctLogin(data: User){
    var sessionData= new Session();
    sessionData.user=data;
   // this.storageService.setCurrentSession(sessionData);
    //this.router.navigate(['/']);
  }

  addProducto(item) {
  console.log(item);
  var route="carro";
  var producto = new Producto();
  producto.Transporte=item; 
  this.carritoService.addCarrito(producto);

  //Valida si hay session para enrutar
    if(this.session)
        this.session.routers.forEach(element => {
          if(element.optionActual==this.optionActual){
            route=element.route;
          }
        });
    this.session.orden.Transporte = item;
    this.storageCompra.setParamsCompraSession(this.session);
    this.router.navigate([route]);
  }

  openDetail( item): void {

    var orden = new Producto();
    var  hotels= new transporte();
   
    orden.Transporte = item;
    orden.tipoDetalle="Transporte";
    orden.codigo=item.codigo;
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
