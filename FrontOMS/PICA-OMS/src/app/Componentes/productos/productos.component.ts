import { Component, OnInit } from '@angular/core';
import {buscadorPaginacion} from '../../Models/busquedaPaginacion';
import {Evento} from '../../Models/evento';
import { AppComponent } from '../../app.component';
import {ProductosService} from '../../service/productos.service';

@Component({
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css']
})
export class ProductosComponent implements OnInit {

  progressBar= false;
  params = {
    tmIni: null,
    tmFin: null,
    evento: null,
    cantidad: 0,
    page: 0,//Variable para almacenar la pagina actual
    pageSize: 21,  // Variable para almacenar la cantidad de resultados por pagina
    categoria: null,
    operador: null,
    optionPaquete:null,
    nombrePaso:null,
    opionPaquete:null,
    routers:[],
 
    accion:""
  }
  dataCount: number=0;//tamaño para el paginador
  dataEventos:Evento[];
  constructor(private parent: AppComponent ,private serviceProduct :ProductosService) { }

  ngOnInit() {
  }


  
  //Metodo que trae los eventos
  onSubmit() {
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
    this.serviceProduct.getProductos(data,result =>{
         
      if(result.codigo=='0'){
         this.dataCount=result.cantidadRegistros;// ==0?1:result.cantidadRegistros;//TODO
         this.params.page = result.paginaActual;
         this.params.pageSize = result.tamanoPagina;
         console.log(this.dataEventos);
         if(result.eventos){
              this.dataEventos=result.eventos;
        }
        }else{
          if(result.mensaje)
            this.parent.openDialog( "",result.mensaje,"Alerta");
          else
            this.parent.openDialog( "","Servidor no disponible","Alerta");
       }
        this.progressBar=false;
    });
    
  }

   //Metodo que se ejecuta cuando cambia la pagina
   onPaginateChange(event) {
    this.params.page = event.pageIndex;
    this.params.pageSize = event.pageSize;
   //event
    //this.params
    this.onSubmit();
  }

   //Metodo que abre el popup del detalle del evento
   openDetail( item): void {
   /* var orden = new Producto();
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
    });*/
  }
}