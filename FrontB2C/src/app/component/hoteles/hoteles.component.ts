import { Component, OnInit } from '@angular/core';
import {Hoteles } from '../../mock/hoteles';
import {Session } from '../../Models/session';
import { StorageService } from '../../storage/storage.service';
import {User } from '../../Models/User';
import {ProductsService} from '../../service/products.service'
import {CarritoService} from '../../service/carrito.service';
import {AppComponent} from '../../app.component';
import {Producto}from '../../Models/producto';
import { Subscription } from 'rxjs';
import {StorageParamsCompraService} from '../../storage/storage-compra'
import {DatalleEventoComponent} from '../datalle-evento/datalle-evento.component';
import {MatDialog} from '@angular/material';

import { Router } from '@angular/router';
import { hotel } from '../../Models/hotel';
@Component({
  selector: 'app-hoteles',
  templateUrl: './hoteles.component.html',
  styleUrls: ['./hoteles.component.css']
})
export class HotelesComponent implements OnInit {
  userInfo :User;
  ordenesCount:number;
  private subscription: Subscription;
  
  infoTable: any[];
  processing:boolean;
  progressBar=false
  optionActual="H";
  params:any;
  session:any;
  constructor(private sesion:StorageService, private productService: ProductsService,private router:Router,
     private parent: AppComponent, private dialog: MatDialog,
     private carritoService:CarritoService, private storageCompra:StorageParamsCompraService) { 

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
    this.productService.hotels(params).subscribe(
      result => {
            console.log(result);
            this.infoTable = result;
            this.ordenesCount=this.infoTable.length; 
      },
      error => {
          console.log(error);
          this.processing = false;
          //this.parent.openDialog( "","Servidor no disponible","Alerta");
          this.infoTable = Hoteles;
      })
     
  }

  private correctLogin(data: User){
    var sessionData= new Session();
    sessionData.user=data;
   // this.storageService.setCurrentSession(sessionData);
    //this.router.navigate(['/']);
  }

  addProducto(item) {
  console.log(item);
  var producto = new Producto();
  
  console.log(item);
    var route="carro";
    var producto = new Producto();
   // item.cantidadPersonas=this.params.can
    producto.Hotel=item; 
    this.carritoService.addCarrito(producto);

    //Valida si hay session para enrutar
      if(this.session)
          this.session.routers.forEach(element => {
            if(element.optionActual==this.optionActual){
              route=element.route;
            }
          });
      this.session.orden.Hotel = item;
      this.storageCompra.setParamsCompraSession(this.session);
      this.router.navigate([route]);
  }

  openDetail( item): void {

    var orden = new Producto();
    var  hotels= new hotel();
   
    orden.Hotel = item;
    orden.tipoDetalle="Hotel";
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
