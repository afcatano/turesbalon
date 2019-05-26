import { Component, OnInit } from '@angular/core';
import {StorageParamsCompraService} from '../../storage/storage-compra'
import {StorageParamsService} from '../../storage/storage-params.service'
import {EstadosOrden} from '../../mock/estadosoOrden';
import { StorageService } from '../../storage/storage.service';
import {UserInfoService} from '../../service/user-info.service';
import {AppComponent} from '../../app.component';
@Component({
  selector: 'app-estado-compra',
  templateUrl: './estado-compra.component.html',
  styleUrls: ['./estado-compra.component.css']
})
export class EstadoCompraComponent implements OnInit {
  
  session:any;
  sessionUser:any;
  botonPagar=true;
  CodigoOrden: "";
  EstadoOrden:string="";
  FechaOrden: "";
  ValorOrden: "";
  progressBar=false;

  constructor( private parent: AppComponent,private userService: UserInfoService,private storageCompra:StorageParamsCompraService,private strorageUser: StorageParamsService, private sesion:StorageService) { }

  ngOnInit() {
    this.session = this.storageCompra.getParamsCompraSession();
    this.sessionUser = this.sesion.getCurrentUser();
    this.CodigoOrden=this.session.orden.Orden.CodigoOrden;
    this.EstadoOrden=this.session.orden.Orden.EstadoOrden;
    this.FechaOrden=this.session.orden.Orden.FechaOrden;
    this.ValorOrden=this.session.orden.Orden.ValorOrden;

    EstadosOrden.forEach(element => {
      if(this.EstadoOrden==element.codigo)
          this.EstadoOrden=element.valor;
    });
    this.actualizarOrden();
  }


  //Actualiza el estado de la orden
  actualizarOrden(){
   
  this.progressBar=true;
    console.log("Eschua evento");
    var params ={
              EstadoOrden: "PA",
              CodigoOrden: this.CodigoOrden,
              IdUsuario:  this.session.orden.userid,
     }

    this.userService.updateUser(params).subscribe(
      result => {
         if(result.codigo=="0"){
            console.log(result);
            this.parent.openDialog( "","Orden Actualizada !!","Alerta");
            this.EstadoOrden=this.session.orden.Orden.EstadoOrden="PA";
            EstadosOrden.forEach(element => {
              if(this.EstadoOrden==element.codigo)
                  this.EstadoOrden=element.valor;
            });
          }else{
            this.parent.openDialog( "",result.mensaje,"Alerta");
         }
         this.progressBar=false;
      },
      error => {
          console.log(error);
          this.parent.openDialog( "","Servidor no disponible","Alerta");
          this.progressBar=false;
       })
     
  }

}
