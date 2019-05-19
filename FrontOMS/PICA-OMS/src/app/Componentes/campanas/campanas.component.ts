import { Component, OnInit } from '@angular/core';
import {buscadorPaginacion} from '../../Models/busquedaPaginacion';
import {Evento} from '../../Models/evento';
import {campana} from '../../Models/Campana';
import { AppComponent } from '../../app.component';
import {CampanasService} from '../../service/campanas.service';
import { DatalleCampanaComponent} from '../datalle-campana/datalle-campana.component';
import {MatDialog} from '@angular/material';
@Component({
  selector: 'app-campanas',
  templateUrl: './campanas.component.html',
  styleUrls: ['./campanas.component.css']
})
export class CampanasComponent implements OnInit {

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
  dataCampaing:Evento[];
  constructor(private parent: AppComponent ,private serviceCamping :CampanasService, private dialog: MatDialog) { }

  ngOnInit() {
    this.onSubmit();
  }


  
  //Metodo que trae las campañas
  onSubmit() {
    this.progressBar=true;
    let parametros = this.params;
    console.log("getDataSource()", parametros);
    var data=new buscadorPaginacion();
    data.pagina=0;
    data.tamanoPagina=20;
    this.serviceCamping.getCampaing(data,result =>{
         
      if(result.codigo=='0'){
         this.dataCount=result.cantidadRegistros;// ==0?1:result.cantidadRegistros;//TODO
         this.params.page = result.paginaActual;
         this.params.pageSize = result.tamanoPagina;
         if(result.data){
          this.dataCampaing=result.data;
          this.progressBar=false;
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
     console.log(item);
   const dialogRef = this.dialog.open(DatalleCampanaComponent, {
      width: '70%',
      data: item
    });

    dialogRef.afterClosed().subscribe(result => {
      this.parent.openDialog( "",result,"Alerta");
      console.log('The dialog was closed');
    });
  }

   //Metodo que abre el popup del detalle del evento
   crearCampana( ): void {
    var item = new campana();
    console.log(item);
    console.log("item");

   const dialogRef = this.dialog.open(DatalleCampanaComponent, {
     width: '70%',
     data: item
   });

   dialogRef.afterClosed().subscribe(result => {
     this.parent.openDialog( "",result,"Alerta");
     console.log('The dialog was closed');
   });
 }
}
