import { Component, OnInit,ViewChild ,Input,Output } from '@angular/core';
import { MatPaginator } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import {User } from '../../Models/User';
import {paqueteInfo} from '../../Models/paqueteInfo';
import {ProductsService} from '../../service/products.service';
import { buscadorPaginacion} from '../../Models/busquedaPaginacion';
import {CarritoService} from '../../service/carrito.service';
import { Subscription } from 'rxjs';
import {MatDialog} from '@angular/material';
import {DatalleEventoComponent} from '../datalle-evento/datalle-evento.component';
import { Router } from '@angular/router';
import {StorageParamsCompraService} from '../../storage/storage-compra'
import {Evento} from '../../Models/evento';
import {Producto} from '../../Models/producto';
import {parametrosBusqueda} from '../../Models/parametrosBusqueda';
import {AppComponent} from '../../app.component';
@Component({
  selector: 'app-paquetes',
  templateUrl: './paquetes.component.html',
  styleUrls: ['./paquetes.component.css']
})
export class PaquetesComponent implements OnInit {

  @Input() page: paqueteInfo;


  @ViewChild(MatPaginator) paginator: MatPaginator;
 
  paramsBusqueda:parametrosBusqueda;
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
    pageSize: 21,
    accion:""
  }
  dataCount: number=0;//tamaño para el paginador
  dataEventos:Evento[];
  
  constructor(private serviceProduct :ProductsService,
    private parent: AppComponent,
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

    //event
    //this.params
    this.getDataEventos();
  }

  //Metodo que recibe la información del componente buscador
  action(data:parametrosBusqueda){
     console.log("Llega evento");
     console.log(data);
     this.paramsBusqueda=data;

     this.params.categoria=  data.categoria;
     this.params.evento= data.nombre;
     //this.params.categoria=data.descripcion;// TODO
    // this.params.categoria=data.codigo;//tTODO
    // this.params.categoria=data.comodin;// this.paquete.comodin, TODO
    // this.params.categoria=data.fechaFinal;
    // this.params.categoria=data.fechaInicial;
     this.params.cantidad=data.cantidadPersonas;
    // this.params.categoria=data.destino;// this.paquete.destino, TODO
    // this.params.categoria=data.origen;// this.paquete.origen, TODO
    // this.params.categoria=data.cantidadItems;//this.paquete.cantidadItems TODO
     this.getDataEventos();
  }

  //Metodo que trae los eventos
  getDataEventos() {
    this.progressBar=true;
    let parametros = this.params;
    console.log("getDataSource()", parametros);
    var data=new buscadorPaginacion();
    data.pagina=this.params.page;
    data.tamanoPagina=this.params.pageSize;
    data.nombre=this.params.evento;
    data.fechaInicial="2017-09-09";//TODO
    data.fechaFinal="2022-09-09";//TODO
    data.codigo =this.params.categoria; //TODO - Categoria
    this.serviceProduct.getEventos(data,result =>{
         
      if(result.codigo=='0'){
         this.dataCount=result.cantidadRegistros;// ==0?1:result.cantidadRegistros;//TODO
         this.params.page = result.paginaActual;
         this.params.pageSize = result.tamanoPagina;
         console.log(this.dataEventos);
         if(result.eventos){
              this.dataEventos=result.eventos;
              
          }else{
    
          }
          
        }else{
          this.parent.openDialog( "",result.mensaje,"Alerta");

        }
        this.progressBar=false;
    });
    
  }

  //Metodo que agrega el evento al carro de compras
  addEvento(item) {
    console.log(item);
    var route="carro";
    var producto = new Producto();
    producto.Evento=new Evento(); 
    producto.Evento.CodigoEvento=item.id
    producto.Evento.NombreEvento=item.nombreEvento ;
    producto.Evento.ValorEvento=item.valorEvento;
    producto.Evento.DescEvento=item.descEvento ;
    producto.Evento.ciudad=item.ciudad ;
    producto.Evento.imagen=item.imagen ;
    producto.Evento.FechaEvento="2019-09-09";//item.fechaEvento ;TODO - Arreglar el formato de fecha
    producto.Evento.inicioEvento=item.horaInicio ;
    producto.Evento.finEvento=item.horaFin ;
    producto.Evento.cantidadDisponibildad=item.cantidad ;
    producto.Evento.cantidadPersonas=this.params.cantidad;

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

    //Metodo que abre el popup del detalle del evento
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
