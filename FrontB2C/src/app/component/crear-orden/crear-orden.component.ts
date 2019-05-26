import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { CarritoService } from '../../service/carrito.service';
import { Producto } from '../../Models/producto';
import {AppComponent} from '../../app.component';
import {StorageParamsService} from '../../storage/storage-params.service'
import {StorageParamsCompraService} from '../../storage/storage-compra'
import {EstadosOrden} from '../../mock/estadosoOrden';
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

  constructor(private router:Router,private storageCompra:StorageParamsCompraService,private strorageUser: StorageParamsService,
    private sesion:StorageService, private userService: UserInfoService, private parent: AppComponent
    , public dialog: MatDialog
  ) { }
  session:any;
  sessionUser:any;
  botonPagar=true;
  CodigoOrden: "";
  EstadoOrden:string="";
  FechaOrden: "";
  ValorOrden: "";

  ngOnInit() {
    this.session = this.storageCompra.getParamsCompraSession();
    this.sessionUser = this.sesion.getCurrentUser();
    this.createOrder();
  }


  createOrder(){

    console.log(this.session.orden);
    var params = this.session.orden;
    if(!this.session.orden.Orden)
     // if(this.session.orden.Orden.CodigoOrden)
        this.userService.odersUserCreate(params).subscribe(
          result => {
            if(result.codigo=="0"){
                console.log(result);
                this.parent.openDialog( "","Orden Creada !!","Alerta");
                this.CodigoOrden=result.CodigoOrden;
                this.EstadoOrden=result.EstadoOrden;
                this.FechaOrden=result.FechaOrden;
                this.ValorOrden=result.ValorOrden;
                var ordens = new orden();
                ordens.CodigoOrden=result.CodigoOrden;
                ordens.FechaOrden=result.FechaOrden;
                ordens.EstadoOrden=result.EstadoOrden;
                ordens.ValorOrden=result.ValorOrden;
                this.session.orden.Orden=ordens;
                this.storageCompra.setParamsCompraSession(this.session);
              // this.action();
              }else{
                this.parent.openDialog( "",result.mensaje+" orden: "+result.CodigoOrden,"Alerta");
            }
            //this.processing=false;
          },
          error => {
              console.log(error);
            // this.processing = false;
              this.parent.openDialog( "","Servidor no disponible","Alerta");
            
          })
          else{
            this.CodigoOrden=this.session.orden.Orden.CodigoOrden;
            this.EstadoOrden=this.session.orden.Orden.EstadoOrden;
            this.FechaOrden=this.session.orden.Orden.FechaOrden;
            this.ValorOrden=this.session.orden.Orden.ValorOrden;

              EstadosOrden.forEach(element => {
                if(this.EstadoOrden==element.codigo)
                   this.EstadoOrden=element.valor;
              });
            
          }
  }


  //Metodo que valida la orden para saber si lo envia a BPM o va a la pasarela.
  validarOrden(){

    console.log(this.session.orden);
     
      var params = 
          {
            CategoriaCliente: this.sessionUser.estadoCliente,
            ValorOrden: this.session.orden.Orden.ValorOrden,
            CodigoOrden: this.session.orden.Orden.CodigoOrden,
            FechaOrden: this.session.orden.Orden.FechaOrden,
            IdUsuario: this.session.orden.IdUsuario,
            EstadoOrden: this.session.orden.Orden.EstadoOrden,
            NombreCliente: this.sessionUser.nombre,
            ApellidoCliente: this.sessionUser.apellido,
            IdTipo: this.sessionUser.tipoDocumento,
            IdNumero: this.sessionUser.userid,
            Correo: this.sessionUser.correo
       }
       if(this.session.orden.Orden.EstadoOrden!="PE" && this.session.orden.Orden.EstadoOrden!="AP"){
       this.userService.validateOrder(params).subscribe(
          result => {
            if(result.codigo=="0"){
                console.log(result);
                this.parent.openDialog( "",result.mensaje,"Alerta");
                this.router.navigate(['pasarela']);
              // this.action();
              }else{
                this.parent.openDialog( "",result.mensaje,"Alerta");
                this.session.orden.Orden.EstadoOrden="PE";
                this.botonPagar=false;
                this.storageCompra.setParamsCompraSession(this.session);
              }
             //this.processing=false;
          },
          error => {
              console.log(error);
            // this.processing = false;
              this.parent.openDialog( "","Servidor no disponible","Alerta");
            
          })
        }else{
        
        if(this.session.orden.Orden.EstadoOrden=="AP")
        {
          this.router.navigate(['pasarela']);
        }else{
          this.botonPagar=false;
          this.parent.openDialog( "","Tu orden estan siendo revisada por un analista de compra, te estaremos notificando via correo electronico !!","Alerta");
         }
        }
  }
  
}
