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
import { Transporte } from '../../Models/transporte';
import {MatDialog} from '@angular/material';
import {DatalleEventoComponent} from '../datalle-evento/datalle-evento.component';
import {parametrosBusqueda} from '../../Models/parametrosBusqueda';
import {TransportService} from '../../service/transport.service';
import {RulesService} from '../../service/rules.service';

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
  paramsBusqueda:parametrosBusqueda;
  transportes:String;
  constructor(private sesion:StorageService, private rulesService: RulesService,
    private productService: ProductsService, private dialog: MatDialog,private transportService:TransportService,
     private parent: AppComponent, private carritoService:CarritoService, private router: Router, private storageCompra:StorageParamsCompraService) { 

  }

  ngOnInit() {
    console.log("Inicia transporte");
    this.session = this.storageCompra.getParamsCompraSession();
    this.params={};
    this.session.orden.Evento.esInternacional

    var param={
      tipoEvento: this.session.orden.Evento.esInternacional,
      hotel: "H" //TODO - Se debe Validar que si viene el hotel lo ponga, si no lo tiene enviar vacio.
    }
    //Consulta el metor de reglas para traer los proveedores de transporte a consultar
    this.rulesService.transporte(param).subscribe(request=>{
      if(request.resultado)
         this.transportes=request.resultado;
         
      console.log("los codigos de transportes a consultar son:"+this.transportes );
    })
    
    this.params.opionPaquete=this.session.optionPaquete
    this.params.nombrePaso= this.optionActual;//esta en el paso de consultar eventos
   }

  action(item:parametrosBusqueda){
    console.log("Eschua evento");
    this.processing=true;
    this.paramsBusqueda=item
    // this.infoTable = Vuelos;

     var params ={
                  Proveedor: this.transportes, //TODO - Validar si hay mas de uno
                  Origen: item.origen,//"EOH",
                  Destino: item.destino,//"BOG",
                  FechaIda: item.fechaInicial,
                  FechaRegreso: item.fechaFinal,
                  CodigoProm: "",
                  Sillas: item.cantidadPersonas
                }

     //AQUI VA LA CONSULTA DEL TRANSPORTE.
     this.progressBar= true;
     this.transportService.transporte(params).subscribe(
      result =>{
       console.log("Entra Transporte");
       if(result.Viajes){

        result.Viajes.forEach(element => {
          if(params.Proveedor=="AV"){
            element.imagen="/../../../assets/vuelos/avianca.jpg";
           }
           if(params.Proveedor=="AA"){
            element.imagen="/../../../assets/vuelos/american.jpg";
           }
           if(params.Proveedor=="BOL"){
            element.imagen="/../../../assets/vuelos/logo-bolivariano.jpg";
           }
           if(element.PrimerClase)
            { 
              element.Precio = element.PrimerClase.Precio;//.toString().replace(".",",");

             var nueElement ={
                            IdViaje: element.IdViaje,
                            CiudadOrigen: element.CiudadOrigen,
                            CiudadDestino: element.CiudadDestino,
                            FechaLlegada: element.FechaLlegada,
                            FechaSalida: element.FechaSalida,
                            Precio:element.ClaseEconomica.Precio,
                            imagen:element.imagen
                          }
               result.Viajes.push(nueElement);
            }
        });
        this.infoTable= result.Viajes;
        this.progressBar=false;
       }else{
 
       }
       this.progressBar=false;
     }
     ,
     error => {
       console.log("Error al consultar eventos:" +error);
       console.log(error);
       this.parent.openDialog( "","Servidor no disponible","Alerta");
       this.progressBar=false;
      }
    );
      
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
  var transporte = new Transporte();
  
  transporte.codigo=item.IdViaje;
  //transporte.proveedor=;
  transporte.ciudadOrigen=item.CiudadOrigen;
  transporte.ciudadDestino=item.CiudadDestino;
  //transporte.paisOrigen=item.;
  //transporte.paisDestino=item.;
  transporte.fechaIda=item.FechaSalida;
  transporte.fechaRegreso=item.FechaLlegada;
  //transporte.nombre=item.;
  transporte.valor=item.Precio;
  //transporte.descripcion=item.;
  //transporte.tipo=item.;
  transporte.imagen=item.imagen;
  //transporte.accion=item.;
  transporte.numSillas=this.paramsBusqueda.cantidadPersonas;
  //transporte.fechaRegresoDespegue=item.;
  //transporte.fechaRegresoAterrizaje=item.;

  producto.Transporte=transporte; 

  producto.Transporte.numSillas= this.paramsBusqueda.cantidadPersonas;
  this.carritoService.addCarrito(producto);

  //Valida si hay session para enrutar
    if(this.session)
        this.session.routers.forEach(element => {
          if(element.optionActual==this.optionActual){
            route=element.route;
          }
        });
    this.session.orden.Transporte = transporte;
    this.storageCompra.setParamsCompraSession(this.session);
    this.router.navigate([route]);
  }

  openDetail( item): void {

    var orden = new Producto();
    var  hotels= new Transporte();
   
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


  get sortData() {
    if(this.infoTable){
    var data =this.infoTable.sort((a, b) => {
      return <any>a.Precio - <any>b.Precio;
    });
    this.infoTable[0].select = true;
    return this.infoTable;
  }
  }
}
