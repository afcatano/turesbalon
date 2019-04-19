import { Component, OnInit,Input,Output,EventEmitter , Inject } from '@angular/core';
import { Router } from '@angular/router';
import {AppComponent} from '../../app.component';
import {StorageParamsCompraService} from '../../storage/storage-compra'
@Component({
  selector: 'app-buscador',
  templateUrl: './buscador.component.html',
  styleUrls: ['./buscador.component.css']
})
export class BuscadorComponent implements OnInit {
  states: string[] = [
    'Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California', 'Colorado', 'Connecticut', 'Delaware',
    'Florida', 'Georgia', 'Hawaii', 'Idaho', 'Illinois', 'Indiana', 'Iowa', 'Kansas', 'Kentucky',
    'Louisiana', 'Maine', 'Maryland', 'Massachusetts', 'Michigan', 'Minnesota', 'Mississippi',
    'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire', 'New Jersey', 'New Mexico',
    'New York', 'North Carolina', 'North Dakota', 'Ohio', 'Oklahoma', 'Oregon', 'Pennsylvania',
    'Rhode Island', 'South Carolina', 'South Dakota', 'Tennessee', 'Texas', 'Utah', 'Vermont',
    'Virginia', 'Washington', 'West Virginia', 'Wisconsin', 'Wyoming'
  ];


  params={
    categoria:"",
    evento:"",
    cantidad:"1",
    optionPaquete:"E",
    routers:[]
  }

  
  validate={
    categoria:{valid:true, message:"",css:""},
    evento:{valid:true, message:"",css:""}
  };

  constructor(private route: Router, private parent:AppComponent, private storageCompra:StorageParamsCompraService) { }

  ngOnInit() {
   if(!this.params.categoria)
   this.params.categoria="--selecciona--";
    
  }

  selectOpcion(option){
console.log(option);
    switch(option){

       case 'FULL':
                  this.params.optionPaquete='FULL';
                  this.params.routers=[{route:"hotel",optionActual:"E"},
                                       {route:"transporte",optionActual:"H"},
                                       {route:"carro",optionActual:"T"},
                                    ]
                  break;
       case 'ET':
                  this.params.optionPaquete='ET';
                  this.params.routers=[{route:"transporte",optionActual:"E"},
                                       {route:"carro",optionActual:"T"},
                                    ]
                  break;
       case 'EH':
                  this.params.optionPaquete='EH';
                  this.params.routers=[{route:"hotel",optionActual:"E"},
                                       {route:"carro",optionActual:"H"},
                                    ]
                  break;
       default:
                  this.params.optionPaquete='E';
                  this.params.routers=[{route:"carro",optionActual:"E"}
                                    ]
                  break;

    }
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
    if(this.params.categoria!='' && this.params.categoria!=null && this.params.categoria!="--selecciona--"){
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
      if(this.params.evento!='' && this.params.evento!=null){
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
    this.params.cantidad=text.toString().replace("-","");
    }

  consultar(){
    console.log(this.validateSumitForms());
    if(this.validateSumitForms())
     { 
       this.storageCompra.setParamsCompraSession(this.params);
       this.route.navigate(['paquetes', this.params.categoria,this.params.evento,this.params.cantidad, this.params.optionPaquete]);
     } else{
        this.parent.openDialog( "","Seleciona una categoria y/ó ingresa un evento","Alerta");

       }
  }

}
