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

  //Evento que envia la consulta realizada
  @Output() action = new EventEmitter<parametrosBusqueda>();
  
  @Input() params: any;

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

  param={ categoria:"",nombre:"", fechaInicial:"", fechaFinal:"", cantidadPersonas:"1" ,accion:"", nombrePaso:"", descripcion: "",
   codigo: "",
   comodin: "",
   destino: "",
   origen: "",
   cantidadItems: 1
}
  constructor(private parent: AppComponent) { }

  validate={
    categoria:{valid:true, message:"",css:""},
    evento:{valid:true, message:"",css:""}
  };

  ngOnInit() {
    this.param.categoria=this.params.categoria;
    this.param.cantidadPersonas=this.params.cantidad?this.params.cantidad:1;
    this.param.nombre=this.params.evento;
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
      this.validateCategoria()||
      this.validateEvento()){
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
  
    validateEvento(){
      if(this.param.nombre!='' && this.param.nombre!=null){
        this.validate.evento.css="";
        this.validate.evento.message="";
        return this.validate.evento.valid=true;
      }else{
        this.validate.evento.css="is-invalid";
        this.validate.evento.message="Dato requerido";
        return this.validate.evento.valid=false;
    }
    }
    replaceNegative(text){
      this.param.cantidadPersonas=text.toString().replace("-","");
      }

  onSubmit() {
    console.log("Event emitter");
    if(this.validateSumitForms()){
    var param = new parametrosBusqueda();
    param.categoria= this.params.categoria,
    param.nombre=  this.params.nombre,
    param.descripcion= this.params.descripcion,
    param.codigo=  this.params.codigo,
    param.comodin=  this.params.comodin,
    param.fechaFinal=  this.params.fechaFinal,
    param.fechaInicial =  this.params.fechaInicial,
    param.cantidadPersonas =  this.params.cantidadPersonas,
    param.destino =  this.params.destino,
    param.origen=   this.params.origen,
    param.cantidadItems=  this.params.cantidadItems

    this.action.emit(param);
    }else{
      this.parent.openDialog( "","Seleciona una rando de fecha y destino","Alerta");

     }
   }

   
  //Evento que captura el input de los rangos de fecha
  addEvent(type: string, event: MatDatepickerInputEvent<Date>, dateInput: string) {
    var fecha = event.value;
    console.log(fecha);
    if(fecha && fecha!=null){
      if("endDate"==dateInput)
      {
        this.endDate= fecha;//.getFullYear() +"-"+fecha.getMonth()+"-"+fecha.getDate();
        console.log("endDate");
        console.log(fecha.getFullYear()+"-"+fecha.getMonth()+"-"+ fecha.getDate());
        
      }
      else
      {
        this.initialDate= fecha;//.getFullYear() +"-"+fecha.getMonth()+"-"+fecha.getDate();
        var vfinishDay = new Date (fecha.getTime() + (this.day*this.maxRange));
        this.minEndDate = new Date(fecha.getFullYear(), fecha.getMonth(), fecha.getDate());
        this.maxEndDate = new Date(vfinishDay.getFullYear(), vfinishDay.getMonth(), vfinishDay.getDate());
        console.log(fecha.getFullYear()+"-"+fecha.getMonth()+"-"+ fecha.getDate());
        console.log("initialDate");
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
