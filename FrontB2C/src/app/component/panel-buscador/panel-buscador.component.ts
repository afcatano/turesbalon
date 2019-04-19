import { Component, OnInit ,Output,Input,EventEmitter} from '@angular/core';
import {paqueteInfo} from '../../Models/paqueteInfo';
import { Observable } from 'rxjs';

import {AppComponent} from '../../app.component';
@Component({
  selector: 'app-panel-buscador',
  templateUrl: './panel-buscador.component.html',
  styleUrls: ['./panel-buscador.component.css']
})
export class PanelBuscadorComponent implements OnInit {

  //Evento que envia la consulta realizada
  @Output() action = new EventEmitter<paqueteInfo>();
  
  @Input() params: any;

  paquete={ categoria:"",evento:"", fechaInicial:"", fechaFinal:"", cantidad:"1" ,accion:""}
  constructor(private parent: AppComponent) { }

  validate={
    categoria:{valid:true, message:"",css:""},
    evento:{valid:true, message:"",css:""}
  };

  ngOnInit() {
    this.paquete.categoria=this.params.categoria;
    this.paquete.cantidad=this.params.cantidad?this.params.cantidad:1;
    this.paquete.evento=this.params.evento;
    this.paquete.accion=this.params.accion;
    console.log(this.paquete);
   if(!this.paquete.categoria)
      this.paquete.categoria="--selecciona--";
    
    if(this.paquete.accion=="buscar")
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
    if(this.paquete.categoria!='' && this.paquete.categoria!=null && this.paquete.categoria!="--selecciona--"){
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
      if(this.paquete.evento!='' && this.paquete.evento!=null){
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
      this.paquete.cantidad=text.toString().replace("-","");
      }
  onSubmit() {
    console.log("Event emitter");
    if(this.validateSumitForms()){
    var paquete = new paqueteInfo(this.paquete.categoria, this.paquete.evento,this.paquete.fechaInicial,this.paquete.fechaFinal, this.paquete.cantidad);
    this.action.emit(paquete);
    }else{
      this.parent.openDialog( "","Seleciona una categoria y/ó ingresa un evento","Alerta");

     }
   }
}