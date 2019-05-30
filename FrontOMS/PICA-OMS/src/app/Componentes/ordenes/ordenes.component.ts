import { Component, OnInit} from '@angular/core';
import {Orders} from '../../Models/Orders';
import { AppComponent } from '../../app.component';
import {OrdenesService} from '../../service/ordenes.service';
import {mes, Mes} from '../../mock/mes';
import { flattenStyles } from '@angular/platform-browser/src/dom/dom_renderer';
import {MatDatepickerInputEvent} from '@angular/material/datepicker';

@Component({
  selector: 'app-ordenes',
  templateUrl: './ordenes.component.html',
  styleUrls: ['./ordenes.component.css']
})
export class OrdenesComponent implements OnInit {
  dataOrders:Orders[];
  datames= {mes:""};
  dataMeses = Mes;

  colTotalFacturado = false;
  
  progressBar= false;
  params = {
    Pagina: 1,//Variable para almacenar la pagina actual
    pageSize: 25,  // Variable para almacenar la cantidad de resultados por pagina
    TotalRegistros: 0,
    CodigoOrden: "",
    CodigoEvento:"",
    TipoConsulta: "ESTANDAR",
    ConDetalle: true,
    Anio: "",
    FechaIni:"",
    FechaFin:"",
    checkRanking:false
  }
  public dataCount: number=0;//tamaño para el paginador

  constructor(private parent: AppComponent ,private serviceOrders :OrdenesService) { }

  ngOnInit() {
    this.colTotalFacturado = false;
    this.datames.mes = "0";
    this.params.TipoConsulta= "ESTANDAR";
    this.params.FechaIni = "";
    this.params.FechaFin = "";
  }

  //metodo que consulta Ordenes
  onConsultar(pagina:number){
    if(pagina==1)
    {
      this.params.Pagina = pagina;
      this.dataCount = 0;
    }
    this.progressBar=true;
    let parametros = this.params;
    console.log("onConsultar()", parametros);
    var dataRequest = new Orders();
    dataRequest.Pagina =this.params.Pagina;
    dataRequest.RegsPorPagina =this.params.pageSize;
    dataRequest.TotalRegistros = this.dataCount;
    dataRequest.TipoConsulta = this.params.TipoConsulta;
    dataRequest.ConDetalle = this.params.ConDetalle;

    if (this.params.CodigoOrden != "")
    {
      dataRequest.CodigoOrden = this.params.CodigoOrden;
      this.colTotalFacturado = false;
    }
    if (this.params.CodigoEvento != "")
    {
      dataRequest.CodigoEvento = this.params.CodigoEvento;
      this.colTotalFacturado = false;
    }     
    if (this.datames.mes != "0" && this.params.Anio !="")
    {
      dataRequest.FechaInicio = this.params.Anio +"-"+ this.datames.mes + "-01";
      dataRequest.TipoConsulta = "ORDCERTOTFACMES";
      dataRequest.EstadoOrden = "PA";
      this.colTotalFacturado = true;
    }
    if (this.params.checkRanking == true)
    {
      dataRequest.TipoConsulta = "ORDMASTIEABI";
      this.colTotalFacturado = false;
    }
    if (this.params.FechaIni.trim() != "")
    {
      dataRequest.TipoConsulta = "ORDCERMASFAC";
      dataRequest.FechaInicio = this.params.FechaIni;
      this.colTotalFacturado = false;
    }
    if (this.params.FechaFin.trim() != "")
    {
      dataRequest.FechaFin = this.params.FechaFin;
      this.colTotalFacturado = false;
    }
      
    this.serviceOrders.getOrdenes(dataRequest,result =>{
         
      if(result.codigo=='0'){
         this.dataCount=result.cantidadRegistros;// ==0?1:result.cantidadRegistros;//TODO
         this.params.Pagina = result.paginaActual;
         //this.params.pageSize = result.tamanoPagina;
         this.params.TotalRegistros = result.cantidadRegistros;
      
         console.log(this.dataOrders);
         if(result.Orders){
              this.dataOrders=result.Orders;
              for (let i = 0; i < this.dataOrders.length; i++) {
                if(this.dataOrders[i].EstadoOrden == 'A')
                {
                 this.dataOrders[i].NombreEstadoOrden = 'Activo'
                }
                if(this.dataOrders[i].EstadoOrden == 'C')
                {
                 this.dataOrders[i].NombreEstadoOrden = 'Cancelada'
                }
                if(this.dataOrders[i].EstadoOrden == 'PE')
                {
                 this.dataOrders[i].NombreEstadoOrden = 'Pendiente'
                }
                if(this.dataOrders[i].EstadoOrden == 'PA')
                {
                 this.dataOrders[i].NombreEstadoOrden = 'Pagada'
                }
                if(this.dataOrders[i].EstadoOrden == 'I')
                {
                 this.dataOrders[i].NombreEstadoOrden = 'Inactiva'
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
  this.onConsultar(this.params.Pagina);
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
/*
chosenYearHandler(normalizedYear: Moment) {
  const ctrlValue = this.date.value;
  ctrlValue.year(normalizedYear.year());
  this.date.setValue(ctrlValue);
}

chosenMonthHandler(normalizedMonth: Moment, datepicker: MatDatepicker<Moment>) {
  const ctrlValue = this.date.value;
  ctrlValue.month(normalizedMonth.month());
  this.date.setValue(ctrlValue);
  datepicker.close();
}
*/
}
