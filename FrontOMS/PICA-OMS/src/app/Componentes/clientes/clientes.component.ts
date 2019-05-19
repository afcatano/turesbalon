import { Component, OnInit} from '@angular/core';
import {TiposDocumento} from '../../mock/tipoDocumentos';
import {Categorias} from '../../mock/categorias';
import {RequestJsonClientes} from '../../Models/RequestJsonClientes';
import {Cliente} from '../../Models/Customer';
import { AppComponent } from '../../app.component';
import {ClientesService} from '../../service/clientes.service';
import {MAT_DATE_LOCALE} from '@angular/material';

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
  private dataCount: number=0;//tamaño para el paginador

  constructor(private parent: AppComponent ,private serviceCustomer :ClientesService) { } 
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

onDateIni(event){
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


onDateFin(event){
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
}
