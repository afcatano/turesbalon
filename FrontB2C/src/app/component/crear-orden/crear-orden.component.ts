import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { CarritoService } from '../../service/carrito.service';
import { Producto } from '../../Models/producto';
import {AppComponent} from '../../app.component';
import {StorageParamsCompraService} from '../../storage/storage-compra'
import { Router } from '@angular/router';


import {Session } from '../../Models/session';
import { StorageService } from '../../storage/storage.service';
import {User } from '../../Models/User';
import {orden } from '../../Models/orden';
import {UserInfoService} from '../../service/user-info.service'

import {ordenes } from '../../mock/oredenes';
import {DetalleOrdenComponent} from '../detalle-orden/detalle-orden.component';
import {MatDialog} from '@angular/material';
@Component({
  selector: 'app-crear-orden',
  templateUrl: './crear-orden.component.html',
  styleUrls: ['./crear-orden.component.css']
})
export class CrearOrdenComponent implements OnInit {

  constructor(private storageCompra:StorageParamsCompraService,
    private sesion:StorageService, private userService: UserInfoService, private parent: AppComponent
    , public dialog: MatDialog
  ) { }
  session:any;
  CodigoOrden: "";
  EstadoOrden: "";
  FechaOrden: "";
  ValorOrden: "";

  ngOnInit() {
    this.session = this.storageCompra.getParamsCompraSession();
    this.createOrder();
  }


  createOrder(){


    console.log(this.session.orden);
    var params = this.session.orden;
    this.userService.odersUserCreate(params).subscribe(
      result => {
         if(result.codigo=="0"){
            console.log(result);
            this.parent.openDialog( "","Orden Creada !!","Alerta");
            this.CodigoOrden=result.CodigoOrden;
            this.EstadoOrden=result.EstadoOrden;
            this.FechaOrden=result.FechaOrden;
            this.ValorOrden=result.ValorOrden;
            this.storageCompra.removeParamsCompraSession();
           // this.action();
          }else{
            this.parent.openDialog( "",result.mensaje,"Alerta");
         }
         //this.processing=false;
      },
      error => {
          console.log(error);
         // this.processing = false;
          this.parent.openDialog( "","Servidor no disponible","Alerta");
         
      })
  }
}
