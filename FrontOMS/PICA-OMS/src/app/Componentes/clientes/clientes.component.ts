import { Component, OnInit} from '@angular/core';
import {TiposDocumento} from '../../mock/tipoDocumentos';
import {Categorias} from '../../mock/categorias';
import {RequestJsonClientes} from '../../Models/RequestJsonClientes';
import {Cliente} from '../../Models/Customer';
import { AppComponent } from '../../app.component';
import {ClientesService} from '../../service/clientes.service';
import {MAT_DATE_LOCALE} from '@angular/material';
import { DetalleClienteComponent} from  '../detalle-cliente/detalle-cliente.component';
import {MatDialog} from '@angular/material';
import {MatDatepickerInputEvent} from '@angular/material/datepicker';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.css'],
  providers: [{ provide: MAT_DATE_LOCALE, useValue: 'es-ES' }]
})
export class ClientesComponent implements OnInit {
  data = {tipoDocumento:""};
  datacat= {categoria:""};
  dataClientes:Cliente[];

  dataTiposDocumento = TiposDocumento;
  dataCategorias = Categorias; 
  colTotalFacturado = false;
  
  progressBar= false;
  params = {
    Pagina: 1,//Variable para almacenar la pagina actual
    pageSize: 25,  // Variable para almacenar la cantidad de resultados por pagina
    TotalRegistros: 0,
    tipoevento: "",
    tipoDocumento: "0",
    documento: 0,
    FechaIni:"",
    FechaFin:""
  }
  public dataCount: number=0;//tamaño para el paginador

  constructor(private parent: AppComponent ,private serviceCustomer :ClientesService, private dialog: MatDialog) { } 
  ngOnInit() {
    this.data.tipoDocumento = "0";
    this.datacat.categoria = "0";
    this.params.FechaIni = "";
    this.params.FechaFin = "";
    this.colTotalFacturado = false;
  }
  
  //metodo que consulta clientes
  onConsultar(){
      this.progressBar=true;
      let parametros = this.params;
      console.log("onConsultar()", parametros);
      var dataRequest = new RequestJsonClientes();
      dataRequest.Pagina =this.params.Pagina;
      dataRequest.RegistroXPagina =this.params.pageSize;
      dataRequest.TotalRegistros = this.dataCount;
      if (this.data.tipoDocumento != "0")
      {
        dataRequest.tipoDocumento = this.data.tipoDocumento;
        this.colTotalFacturado = false;
      }
      if (this.params.documento != 0)
      {
        dataRequest.documento = this.params.documento;
        this.colTotalFacturado = false;
      }
      if (this.datacat.categoria != "0")
      {
        dataRequest.estadoCliente = this.datacat.categoria;
        this.colTotalFacturado = false;
      }
      if (this.params.tipoevento.trim() != "")
      {
        dataRequest.TipoEvento = this.params.tipoevento;
        this.colTotalFacturado = false;
      }
      if (this.params.FechaIni.trim() != "")
      {
        dataRequest.FechaIniFact = this.params.FechaIni;
        this.colTotalFacturado = true;
      }
      if (this.params.FechaFin.trim() != "")
      {
        dataRequest.FechaFinFact = this.params.FechaFin;
        this.colTotalFacturado = true;
      }
      //data.nombre=this.params.cliente;n
      //data.fechaInicial="2017-09-09";
      //data.fechaFinal="2022-09-09";//TODO
      //data.codigo =this.params.categoria; //TODO - Categoria
      this.serviceCustomer.getClientes(dataRequest,result =>{
           
        if(result.codigo=='0'){
           this.dataCount=result.cantidadRegistros;// ==0?1:result.cantidadRegistros;//TODO
           this.params.Pagina = result.paginaActual;
           this.params.pageSize = result.tamanoPagina;
           this.params.TotalRegistros = result.cantidadRegistros;
           
           console.log(this.dataClientes);
           if(result.clientes){
                this.dataClientes=result.clientes;
                for (let i = 0; i < this.dataClientes.length; i++) {
                  if(this.dataClientes[i].estadoCliente == 'PLATA')
                  {
                   this.dataClientes[i].IDEstadoCliente = 1
                  }
                  if(this.dataClientes[i].estadoCliente == 'PLATINO')
                  {
                   this.dataClientes[i].IDEstadoCliente = 2
                  }
                  if(this.dataClientes[i].estadoCliente == 'DORADO')
                  {
                   this.dataClientes[i].IDEstadoCliente = 3
                  }
                  if(this.dataClientes[i].tipoDocumento == 'CC')
                  {
                   this.dataClientes[i].IDtipoDocumento = 1
                  }
                  if(this.dataClientes[i].tipoDocumento == 'TI')
                  {
                   this.dataClientes[i].IDtipoDocumento = 2
                  }
                  if(this.dataClientes[i].tipoDocumento == 'PE')
                  {
                   this.dataClientes[i].IDtipoDocumento = 3
                  }
                  if(this.dataClientes[i].tipoDocumento == 'NIT')
                  {
                   this.dataClientes[i].IDtipoDocumento = 4
                  }
               }
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
    this.params.Pagina = event.pageIndex + 1;
    this.params.pageSize = event.pageSize;
   //event
    //this.params
    this.onConsultar();
  }

onDateIni(event: MatDatepickerInputEvent<Date>){
  if (event.value != null)
  {
  let tmpMonth = event.value.getUTCMonth() + 1;
  let tmpFechIni = event.value.getFullYear() + "-" + ("'00" + tmpMonth).substr(-2) + "-" + ("'00" + event.value.getDate()).substr(-2);
  this.params.FechaIni = tmpFechIni;
  }
  else
  {
    this.params.FechaIni = ""
  }
}


onDateFin(event: MatDatepickerInputEvent<Date>){
  if (event.value != null)
  {
  let tmpMonth = event.value.getUTCMonth() + 1;
  let tmpFechFin = event.value.getFullYear() + "-" + ("'00" + tmpMonth).substr(-2) + "-" + ("'00" + event.value.getDate()).substr(-2);
  this.params.FechaFin = tmpFechFin;
}
else
{
  this.params.FechaFin = ""
}
}

validateTotalFacturado(){
  if(this.colTotalFacturado)
    return this.colTotalFacturado=true;
    else
    return this.colTotalFacturado=false;
}

   //Metodo que abre el popup del detalle del evento
   openDetail( item): void {
    console.log(item);
  const dialogRef = this.dialog.open(DetalleClienteComponent, {
     width: '70%',
     data: item
   });

   dialogRef.afterClosed().subscribe(result => {
    if (result != undefined || result != null)
    {
    this.parent.openDialog( "",result,"Alerta");
    }
    console.log('The dialog was closed');
  });
  }

  
   //Metodo que abre el popup del detalle del evento
   crearCliente( ): void {
    var item = new Cliente();
    console.log(item);
    console.log("item");

   const dialogRef = this.dialog.open(DetalleClienteComponent, {
     width: '70%',
     data: item
   });

   dialogRef.afterClosed().subscribe(result => {
     if (result != undefined || result != null)
     {
      this.parent.openDialog( "",result,"Alerta");
    }
     console.log('The dialog was closed');
   });
 }
}