import {Component, OnInit} from '@angular/core';
import {Session } from '../../Models/session';
import { StorageService } from '../../storage/storage.service';
import {User } from '../../Models/User';
import {Producto } from '../../Models/producto';
import {Evento } from '../../Models/evento';
import {orden } from '../../Models/orden';
import {UserInfoService} from '../../service/user-info.service'
import {AppComponent} from '../../app.component';
import {ordenes } from '../../mock/oredenes';
import {EstadosOrden } from '../../mock/estadosoOrden';
import {DetalleOrdenComponent} from '../detalle-orden/detalle-orden.component';
import {MatDialog} from '@angular/material';
import {StorageParamsCompraService} from '../../storage/storage-compra'
import { Router } from '@angular/router';
import {DataCodigoHotel} from '../../mock/proveedores';
import {DataCodigoTransporte} from '../../mock/proveedores';
@Component({
  selector: 'app-ordenes',
  templateUrl: './ordenes.component.html',
  styleUrls: ['./ordenes.component.css']
})
export class OrdenesComponent implements OnInit {
  userInfo :User;
  ordenesCount:number;
  dataTitle = {
     fechas: 'fecha',valor: 'valor',evento: 'evento',transporte: 'transporte', hotel: "hotel", estado: 'estado', accion: 'accion'
  }
  infoTable: any[];
  processing:boolean;
  constructor(private router:Router,private sesion:StorageService, private userService: UserInfoService, private parent: AppComponent,
    private storageCompra:StorageParamsCompraService,
    public dialog: MatDialog) { 

  }

  ngOnInit() {
    this.userInfo =this.sesion.loadSessionData().user;
    
    this.action();
}

  action(){
    console.log("Eschua evento");
    this.processing=true;
    

    var params ={
              //FechaInicio: "2019-03-31",
              //FechaFin: "2019-04-29",
              IdUsuario: this.userInfo.userid,
              ConDetalle: "true",
              TipoConsulta: "ESTANDAR"
    }

    //consulta las ordenes del usuario
    this.userService.odersUser(params).subscribe(
      result => {
         if(result.codigo=="0"){
            console.log(result);

            result.Orders.forEach(elementOrder => {
              EstadosOrden.forEach(element => {
                if(elementOrder.EstadoOrden==element.codigo)
                elementOrder.EstadoValorOrden=element.valor;
              });

              if(elementOrder.Hotel){
                  DataCodigoHotel.forEach(element => {
                     if(elementOrder.Hotel.EmpresaHotel==element.codigo)
                       elementOrder.Hotel.EmpresaHotelNombre=element.valor;
                  });
               }
               if(elementOrder.Transporte){
                  DataCodigoTransporte.forEach(element => {
                    if(elementOrder.Transporte.EmpresaTransporte==element.codigo)
                    elementOrder.Transporte.EmpresaTransporteNombre=element.valor;
                  });
                }
            });
           
            this.infoTable = result.Orders;
            this.ordenesCount=this.infoTable.length; 
           

          }else{
            this.parent.openDialog( "",result.mensaje,"Alerta");
         }
         this.processing=false;
      },
      error => {
          console.log(error);
          this.processing = false;
          this.parent.openDialog( "","Servidor no disponible","Alerta");
          this.infoTable = ordenes;
      })
     
  }

  cancelarOrden(nav){
    console.log("Eschua evento");
    this.processing=true;
    var params ={
              EstadoOrden: "C",
              CodigoOrden: nav.CodigoOrden,
              IdUsuario: nav.IdUsuario,
     }

    this.userService.updateUser(params).subscribe(
      result => {
         if(result.codigo=="0"){
            console.log(result);
            this.parent.openDialog( "","Orden Cancelada !!","Alerta");
            this.action();
          }else{
            this.parent.openDialog( "",result.mensaje,"Alerta");
         }
         this.processing=false;
      },
      error => {
          console.log(error);
          this.processing = false;
          this.parent.openDialog( "","Servidor no disponible","Alerta");
          this.infoTable = ordenes;
      })
     
  }

  openDetail( item): void {
    const dialogRef = this.dialog.open(DetalleOrdenComponent, {
      width: '70%',
      data: item
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  private correctLogin(data: User){
    var sessionData= new Session();
    sessionData.user=data;
   // this.storageService.setCurrentSession(sessionData);
    //this.router.navigate(['/']);
  }


  //Pagar la orden en estado aprobado
  payment(Ordenes){
    
    var param={
      categoria:"",
      evento:"",
      cantidad:"1",
      orden:null,
      optionPaquete:"E",
      routers:[]
    }
   
    var producto = new Producto();

    producto.CodigoOrden= Ordenes.CodigoOrden;
    producto.userid = Ordenes.IdUsuario;
    producto.Evento=new Evento(); 
    producto.Evento= Ordenes.Evento;
    producto.Orden=Ordenes
    param.orden = producto;
    
    this.storageCompra.setParamsCompraSession(param);
    this.router.navigate(['crearOrden']);
  }
  
}
