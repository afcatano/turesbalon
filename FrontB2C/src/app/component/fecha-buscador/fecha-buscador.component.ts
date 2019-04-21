import { Component, OnInit ,Output,Input,EventEmitter} from '@angular/core';
import {paqueteInfo} from '../../Models/paqueteInfo';
import { Observable } from 'rxjs';
import {AppComponent} from '../../app.component';

import {FormControl} from '@angular/forms';
import {DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE} from '@angular/material/core';

import {MatDatepickerInputEvent} from '@angular/material/datepicker';

import {parametrosBusqueda} from '../../Models/parametrosBusqueda';


@Component({
  selector: 'app-fecha-buscador',
  templateUrl: './fecha-buscador.component.html',
  styleUrls: ['./fecha-buscador.component.css']
})
export class FechaBuscadorComponent implements OnInit {

  //destino que envia la consulta realizada
  @Output() action = new EventEmitter<parametrosBusqueda>();
  
  @Input() params: any;

  mensajeError="";
  v_initialDate = new FormControl(new Date());
  v_endDate = new FormControl(new Date());
  serializedDate = new FormControl((new Date()).toISOString());
  
  public initialDate;//Valor donde se almacena el rango de fecha inicial 
  public endDate;//Valor donde se almacena el rango de fecha final
  public rangeDate:boolean=false; //Bandera para activar o inactivar el rango de fecha

  //Constantes
  v_minInitialDate = new Date(2018, 4, 1);
  v_maxDateFist = new Date(2030, 4, 1);
  v_minEndDate = new Date(2018, 4, 1);
  v_maxEndDate = new Date(2030, 0, 1);
  day:number=24*60*60*1000;
  maxRange:number=31 //Maximo del rango;

  //Variables
  minInitialDate = this.v_minInitialDate;
  maxDateFist = this.v_maxDateFist;
  minEndDate = this.v_minEndDate;
  maxEndDate = this.v_maxEndDate;

  param={ categoria:"",nombre:"", fechaInicial:"", fechaFinal:"", cantidadPersonas:1 ,accion:"", nombrePaso:"", descripcion: "",
   codigo: "",
   comodin: "",
   destino: "",
   origen: "",
   cantidadItems: 1
}
  constructor(private parent: AppComponent) { }

  validate={
    categoria:{valid:true, message:"",css:""},
    destino:{valid:true, message:"",css:""},
    origen:{valid:true, message:"",css:""}
  };

  ngOnInit() {
    this.param.categoria=this.params.categoria;
    this.param.cantidadPersonas=this.params.cantidad?parseInt(this.params.cantidad):1;
    this.param.nombre=this.params.destino;
    this.param.accion=this.params.accion;
    this.param.nombrePaso=this.params.nombrePaso;
    console.log(this.param);
   if(!this.param.categoria)
      this.param.categoria="--selecciona--";
    
    if(this.param.accion=="buscar")
        this.onSubmit();
    
  }

  validateSumitForms(){
    if(
      (this.param.nombrePaso=='T'? this.validateOrigen(): true)&&
      this.validateDestino()&&
      this.validateDate()&&
      this.validateCantidad()){
           return true;
      }else{
        return false;
      }
  }
  
  validateCategoria(){
    if(this.param.categoria!='' && this.param.categoria!=null && this.param.categoria!="--selecciona--"){
      this.validate.categoria.css="";
      this.validate.categoria.message="";
      return this.validate.categoria.valid=true;
    }else{
      this.validate.categoria.css="is-invalid";
      this.validate.categoria.message="Dato requerido";
      return this.validate.categoria.valid=false;
  }
}

    validateCantidad(){
       if(this.param.cantidadPersonas > 0){
          return true;
        }else{
            this.mensajeError="La cantidad de personas debe ser mayor a 0.";
            return false;
        }
      
    }
  
    validateDestino(){
      if(this.param.destino!='' && this.param.destino!=null){
        return this.validate.destino.valid=true;
      }else{
        this.mensajeError="Debe seleccionar un destino.";
        return this.validate.destino.valid=false;
    }
    }

    validateDate(){
      if(this.param.fechaInicial!='' && this.param.fechaInicial!=null && this.param.fechaFinal!='' && this.param.fechaFinal!=null){
         return true;
      }else{
        this.mensajeError="Debe seleccionar un rango de fechas.";
        return false;
    }
    }

    validateOrigen(){
      if(this.param.origen!='' && this.param.origen!=null){
        return true;
      }else{
        this.mensajeError="Debe seleccionar un origen.";
        return false;
    }
    }
    replaceNegative(text){
      this.param.cantidadPersonas=text.toString().replace("-","");
      }

  onSubmit() {
    console.log("Event emitter");
    if(this.validateSumitForms()){
    var paramsBusqueda = new parametrosBusqueda();
    paramsBusqueda.categoria= this.param.categoria,
    paramsBusqueda.nombre=  this.param.nombre,
    paramsBusqueda.descripcion= this.param.descripcion,
    paramsBusqueda.codigo=  this.param.codigo,
    paramsBusqueda.comodin=  this.param.comodin,
    paramsBusqueda.fechaFinal=  this.param.fechaFinal,
    paramsBusqueda.fechaInicial =  this.param.fechaInicial,
    paramsBusqueda.cantidadPersonas =  this.param.cantidadPersonas,
    paramsBusqueda.destino =  this.param.destino,
    paramsBusqueda.origen=   this.param.origen,
    paramsBusqueda.cantidadItems=  this.param.cantidadItems

    this.action.emit(paramsBusqueda);
    }else{
      this.parent.openDialog( "",this.mensajeError,"Alerta");

     }
   }

   
  //destino que captura el input de los rangos de fecha
  addEvent(type: string, event: MatDatepickerInputEvent<Date>, dateInput: string) {
    var fecha = event.value;
    console.log(fecha);
    if(fecha && fecha!=null){
      if("endDate"==dateInput)
      {
        this.endDate= fecha;//.getFullYear() +"-"+fecha.getMonth()+"-"+fecha.getDate();
        console.log("endDate");
        console.log(fecha.getFullYear()+"-"+fecha.getMonth()+"-"+ fecha.getDate());
        this.param.fechaFinal=fecha.getFullYear()+"-"+fecha.getMonth()+"-"+ fecha.getDate();
      }
      else
      {
        this.initialDate= fecha;//.getFullYear() +"-"+fecha.getMonth()+"-"+fecha.getDate();
        var vfinishDay = new Date (fecha.getTime() + (this.day*this.maxRange));
        this.minEndDate = new Date(fecha.getFullYear(), fecha.getMonth(), fecha.getDate());
        this.maxEndDate = new Date(vfinishDay.getFullYear(), vfinishDay.getMonth(), vfinishDay.getDate());
        console.log(fecha.getFullYear()+"-"+fecha.getMonth()+"-"+ fecha.getDate());
        console.log("initialDate");
        this.param.fechaInicial=fecha.getFullYear()+"-"+fecha.getMonth()+"-"+ fecha.getDate();
      }
    }else{
      if("endDate"==dateInput)
       {
          this.endDate= undefined;
           console.log("entra");
        }
      else
      {
        this.initialDate= undefined;
         console.log("sale");
      }
    }
   }
}
