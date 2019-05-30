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
  infoTablelLength:number=0;
  processing:boolean;
  params:any;
  paramsBusqueda:parametrosBusqueda;
  transportes:string;
  isNacional:string;
  isAA=false;
  isAV=false; 
  isBOL=false;
  isHotel=false;
  isProveedorHotel:string;

  constructor(private sesion:StorageService, private rulesService: RulesService,
    private productService: ProductsService, private dialog: MatDialog,private transportService:TransportService,
     private parent: AppComponent, private carritoService:CarritoService, private router: Router, private storageCompra:StorageParamsCompraService) { 

  }

  ngOnInit() {
    console.log("Inicia transporte");
    this.session = this.storageCompra.getParamsCompraSession();
    this.params={};
    this.session.orden.Evento.esInternacional
    this.isNacional=this.session.orden.Evento.esInternacional;
    
    var param={
      tipoEvento: this.session.orden.Evento.esInternacional,
      hotel: this.session.orden.Hotel? this.session.orden.Hotel.proveedor:'' //TODO - Se debe Validar que si viene el hotel lo ponga, si no lo tiene enviar vacio.
    }
    //Valida si hay hoteles seleccionados
    if( this.session.orden.Hotel)
      this.isHotel=true;
    this.isProveedorHotel=param.hotel;
    //Consulta el metor de reglas para traer los proveedores de transporte a consultar
    this.rulesService.transporte(param).subscribe(request=>{
      if(request.resultado)
         this.transportes=request.resultado;
         
      console.log("los codigos de transportes a consultar son:"+this.transportes );
      var splitDataHotel= this.transportes.split(',');
        splitDataHotel.forEach(element => {
          if( element=='AA')
            this.isAA=true;
          if( element=='AV')
            this.isAV=true; 
          if( element=='BOL')
            this.isBOL=true; 
        });
    })
    
    this.params.opionPaquete=this.session.optionPaquete
    this.params.nombrePaso= this.optionActual;//esta en el paso de consultar eventos
   }

  action(item:parametrosBusqueda){
    console.log("Eschua evento");
    this.infoTable=[];
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
       var countPrecio=12770;
       if(result.codigo=="0"){
            if(result.Viajes){

                if(result.Viajes.length >0 ){
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
                      //Si no tiene precio pongo un precio
                      if(!element.Precio)
                        element.Precio=countPrecio;
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
                        }else{

                        }
                        countPrecio=countPrecio+1240;
                    });
                this.infoTable= result.Viajes;
            }
            else{
            }
            
            this.progressBar=false;
          }else{
    
          }
      }else{
        //No se encontro transporte
        this.parent.openDialog( "","No hay transporte disponible","Alerta");
      }
       this.infoTable
       this.infoTablelLength=this.infoTable?this.infoTable.length:0;
       
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
  
  var fecha = new Date();
  var idFecha = fecha.getFullYear()+""+(fecha.getMonth())+""+fecha.getDate()+ ""+fecha.getMinutes()+""+fecha.getSeconds()+""+fecha.getMilliseconds();
  console.log("Validación de la id reserva para el proveedor:"+item.proveedor);
 transporte.proveedor= this.transportes;
  transporte.codigo=transporte.proveedor=="AV"? item.IdViaje +"-"+item.Consecutivo:transporte.proveedor=="BOL"?idFecha +"-"+item.IdViaje: item.IdViaje ;  //TODO AV IdViaje-Consecutivo, BOL IdViaje-
  transporte.ciudadOrigen=item.CiudadOrigen;
  transporte.ciudadDestino=item.CiudadDestino;
  transporte.paisOrigen="Colombia";//TODO - poner pais
  transporte.paisDestino="Estados unidos";//TODO - poner pais
  transporte.fechaIda=item.FechaSalida;
  transporte.fechaRegreso=item.FechaLlegada;

  //Se asignan temporalmente
  transporte.fechaIda=this.paramsBusqueda.fechaFinal;
  transporte.fechaRegreso=this.paramsBusqueda.fechaFinal;

  if(transporte.fechaIda)
    if(transporte.fechaIda.split('T').length >0)
       transporte.fechaIda=transporte.fechaIda.split('T')[0];
  if(transporte.fechaRegreso)
    if(transporte.fechaRegreso.split('T').length >0)
       transporte.fechaRegreso=transporte.fechaRegreso.split('T')[0];
  //transporte.nombre=item.;
  transporte.valor=item.Precio;
  //transporte.descripcion=item.;
  //transporte.tipo=item.;
  transporte.imagen=item.imagen;
  //transporte.accion=item.;
  transporte.numSillas=this.paramsBusqueda.cantidadPersonas;
  transporte.fechaRegresoDespegue="2019-04-19"; //TODO - poner fecha
  transporte.fechaRegresoAterrizaje="2019-04-19";//TODO - poner fecha

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
    if(this.infoTable[0])
    this.infoTable[0].select = true;
    return this.infoTable;
  }
  }
}
