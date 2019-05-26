import { Component, OnInit } from '@angular/core';
import {Hoteles } from '../../mock/hoteles';
import {DataCodigoHotel } from '../../mock/proveedores';
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
import {parametrosBusqueda} from '../../Models/parametrosBusqueda';
import {RulesService} from '../../service/rules.service';
import { Router } from '@angular/router';
import { hotel } from '../../Models/hotel';
import {HotelsService} from '../../service/hotels.service';
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
  infoTableLength:number=0;
  processing:boolean;
  progressBar=false
  optionActual="H";
  params:any;
  paramsBusqueda:parametrosBusqueda;
  hoteles: String
  session:any;
  isHilton=false;
  isDann=false;
  isNacional:string;
  constructor(private sesion:StorageService, private productService: ProductsService,private router:Router,private rulesService: RulesService,
     private parent: AppComponent, private dialog: MatDialog,private hotelsService: HotelsService,
     private carritoService:CarritoService, private storageCompra:StorageParamsCompraService) { 

  }

  ngOnInit() {
    console.log("Inicia hotel");
    this.session = this.storageCompra.getParamsCompraSession();
    this.params={};
    this.params.opionPaquete=this.session.optionPaquete
    this.params.nombrePaso= this.optionActual;//esta en el paso de consultar eventos
    //this.action();
    this.isNacional=this.session.orden.Evento.esInternacional;
    var param={
      tipoEvento: this.session.orden.Evento.esInternacional,
    }
    //Consulta el metor de reglas para traer los proveedores de hoteles a consultar
    this.rulesService.hotel(param).subscribe(request=>{
      if(request.resultado)
         this.hoteles=request.resultado;
         
        console.log("los codigos de hotel a consultar son:"+this.hoteles );

        var splitDataHotel= this.hoteles.split(',');
        splitDataHotel.forEach(element => {
          if( element=='H')
            this.isHilton=true;
          if( element=='D')
            this.isDann=true; 
        });
      
        
    })
  }

  action(item:parametrosBusqueda){
    console.log("Eschua evento");
    this.processing=true;
    //console.log(this.username, this.password);
    this.paramsBusqueda=item;
    var splitHoteles=this.hoteles.split(',');
    this.infoTable=[];

    splitHoteles.forEach(itemFor=>{
        var params ={
            Proveedor: itemFor, //TODO - Validar si hay mas de uno
            Pais:"colombia", //TODO.- pais esta quemado
            Ciudad: item.destino,//"BOG",
            FechaEntrada: item.fechaInicial,
            FechaSalida: item.fechaFinal,
            CodigoProm: "",
            TipoHabitacion: "Individual", //TODO - Tipo habitación esta quemado
            IdHotel:"DCH-1003", //TODO - el hotel esta quemado.
            NumeroHabitaciones: item.cantidadPersonas
      }
      this.hotelsService.hotel(params).subscribe(
        result => {
              console.log(result);
              if(result.Hoteles){
                var contadorImagen=1;
                var arrayHotel=[];
                result.Hoteles.forEach(element => {
                    var infHotel=new  hotel()

                    if(element.Habitaciones.length>0){
                      

                         element.Habitaciones.forEach(elementHabita => {
                              infHotel.codigo=212112;//TODO - Codigo de reserva de hotel
                              infHotel.nombre=element.NombreHotel;
                              infHotel.valor=elementHabita.Precio;
                              //infHotel.descripcion=element.Habitaciones.Habitacion.Descripcion;
                              infHotel.direccion=element.Direccion;
                              infHotel.numeroHabitacion=elementHabita.Numero;
                              infHotel.ciudad=item.destino;
                              infHotel.pais="Estados unidos";//TODO - falta el pais
                              infHotel.imagen="/../../../assets/hoteles/hotel"+contadorImagen+".jpg";
                              //infHotel.coordenadas=result.Hoteles;
                              infHotel.fechaEntrada=item.fechaInicial;
                              infHotel.fechaSalida=item.fechaFinal;
                              infHotel.tipoHotel=elementHabita.Tipo ? elementHabita.Tipo:"Estandar";
                              infHotel.proveedor=itemFor;
                              infHotel.accion=this.optionActual;
                              infHotel.cantidad=item.cantidadPersonas;

                              if(contadorImagen==4)
                                contadorImagen=0;
                                contadorImagen= contadorImagen+1;
                                arrayHotel.push(infHotel);
                          });
                 
                    }else{
                      infHotel.codigo=element.IdHotel;
                      infHotel.nombre=element.Habitaciones.Habitacion.Nombre;
                      infHotel.valor=element.Habitaciones.Habitacion.Precio;
                      infHotel.descripcion=element.Habitaciones.Habitacion.Descripcion;
                      infHotel.direccion="avenue 124 dolor four" ;//TODO - no tiene direccion
                      infHotel.numeroHabitacion=element.Habitaciones.Habitacion.Numero;
                      infHotel.ciudad=item.destino;
                      infHotel.pais="Colombia";//TODO - falta el pais
                      infHotel.imagen="/../../../assets/hoteles/hotel"+contadorImagen+".jpg";
                      //infHotel.coordenadas=result.Hoteles;
                      infHotel.fechaEntrada=item.fechaInicial;
                      infHotel.fechaSalida=item.fechaFinal;
                      infHotel.tipoHotel="Estandar";//TODO - no tiene tipo
                      infHotel.proveedor=itemFor;
                      infHotel.accion=this.optionActual;
                      infHotel.cantidad=item.cantidadPersonas;
                      if(contadorImagen==4)
                      contadorImagen=0;
                      contadorImagen= contadorImagen+1;
                      arrayHotel.push(infHotel);
                    }
                    
                });
                //Verifica que no este vacio para saber si lo asigna o lo une
                if(this.infoTable.length > 0){
                  arrayHotel.forEach(element => {
                    this.infoTable.push(element);
                  });
                }
                else
                  this.infoTable=arrayHotel;
                
              }
              this.ordenesCount=this.infoTable.length; 
              this.infoTableLength=this.infoTable.length;
              this.processing = false;
        },
        error => {
            console.log(error);
            this.processing = false;
            //this.parent.openDialog( "","Servidor no disponible","Alerta");
            this.infoTable = Hoteles;
        })
    });
     
  }

  private correctLogin(data: User){
    var sessionData= new Session();
    sessionData.user=data;
   // this.storageService.setCurrentSession(sessionData);
    //this.router.navigate(['/']);
  }

  addProducto(item) {
  var producto = new Producto();
  console.log("Agrega Hotel al carrito");
  console.log(item);
    var route="carro";
    var producto = new Producto();
   // item.cantidadPersonas=this.params.can
    
    item.cantidadPersonas=this.paramsBusqueda.cantidadPersonas;
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


  get sortData() {
    if(this.infoTable){
    var data =this.infoTable.sort((a, b) => {
      return <any>a.valor - <any>b.valor;
    });
    if(this.infoTable[1])
    this.infoTable[1].select = false;
    if(this.infoTable[0])
    this.infoTable[0].select = true;
    return this.infoTable;
  }
  }
}
