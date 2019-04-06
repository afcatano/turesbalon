import { Component, OnInit,Input,Output,EventEmitter , Inject } from '@angular/core';
import {User } from '../../Models/User';
import  {Paises} from '../../mock/paises';
import {franquicias} from '../../mock/franquicia';
import  {TiposDocumento} from '../../mock/tipoDocumentos';
import { AppComponent } from '../../app.component';
import {Session } from '../../Models/session';
import { PageDat} from '../../Models/pageData';
import { Observable } from 'rxjs';

import {Router} from "@angular/router";
import { StorageService } from '../../storage/storage.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  @Input() page: PageDat;

  @Input() userdata: User;
  
  //Evento de accion sobre formulario
  @Output() action = new EventEmitter<User>()

  register:User;
  dataPaises = Paises;
  dataFranquicias= franquicias;
  dataTiposDocumento = TiposDocumento;
  terminosCondicionesValue:boolean;
  terminosCondiciones:boolean;
  tipoDocumentoSeleccionado:any;
  terminosCondicionesValid:boolean=true;
  
  validate={
    nombre:{valid:true, message:"",css:""},
    apellido:{valid:true, message:"",css:""},
    username:{valid:true, message:"",css:""},
    password:{valid:true, message:"",css:""},
    telefono:{valid:true, message:"",css:""},
    correo:{valid:true, message:"",css:""},
    ciudad:{valid:true, message:"",css:""},
    departamento:{valid:true, message:"",css:""},
    pais:{valid:true, message:"",css:""},
    confirmarContrasena:{valid:true, message:"",css:""},
    checkTarjeta:{valid:true, message:"",css:""},
    direccion:{valid:true, message:"",css:""},
    tipoDocumento:{valid:true, message:"",css:""},
    documento:{valid:true, message:"",css:""},
    codigoTarjeta:{valid:true, message:"",css:""},
     fechaTarjeta:{valid:true, message:"",css:""},
     numeroTarjeta:{valid:true, message:"",css:""},
     franquiciaTarjeta:{valid:true, message:"",css:""},
  };
 
  constructor( private storageService: StorageService, private parent : AppComponent 
    ,private router: Router) { 
     console.log (this.dataFranquicias);
  }


  ngOnInit() {

    this.register = new User( "","","","","","","","","","","","","","","","","","");
    console.log(this.register.nombre);
    switch(this.page.action){
      case  "update":
                    this.terminosCondiciones= false;
                    this.register = new User( 
                      this.userdata.username,
                      this.userdata.password,
                      this.userdata.nombre,
                      this.userdata.apellido,
                      this.userdata.telefono,
                      this.userdata.correo,
                      this.userdata.ciudad,
                      this.userdata.departamento,
                      this.userdata.pais,
                      this.userdata.confirmarContrasena,
                      this.userdata.checkTarjeta,
                      this.userdata.direccion,
                      this.userdata.tipoDocumento,
                      this.userdata.documento,
                      this.userdata.codigoTarjeta,
                      this.userdata. fechaTarjeta,
                      this.userdata.numeroTarjeta,
                      this.userdata. franquiciaTarjeta)
                     break;
       case  "create":
                      this.terminosCondiciones= true;
                      this.register = new User( "","","","","","","","","","","","","","","","","","");
                      break;
        default:
              break;
      }
    
       console.log(this.page);
  }

  
  onSubmit() {
    var validate=false;
    var msg="Debe completar todos los campos";
    if(this.terminosCondiciones){
       if(!this.terminosCondicionesValue){
           msg="Debe aceptar los terminos y condiciones";
           this.terminosCondicionesValid=false;
         }else{
          validate=true;
         }
        }else{
          validate=true;
        }

    if(validate) {
      console.log("Ejecuta accion.");
      this.action.emit(new User( 
       this.register.username,
       this.register.password,
       this.register.nombre,
       this.register.apellido,
       this.register.telefono,
       this.register.correo,
       this.register.ciudad,
       this.register.departamento,
       this.register.pais,
       this.register.confirmarContrasena,
       this.register.checkTarjeta,
       this.register.direccion,
       this.register.tipoDocumento,
       this.register.documento,
       this.register.codigoTarjeta,
       this.register. fechaTarjeta,
       this.register.numeroTarjeta,
       this.register. franquiciaTarjeta));
    } else {
      this.parent.openDialog( "",msg,"Alerta");
    }
  }
 
  capturar() {
    // Pasamos el valor seleccionado a la variable verSeleccion
   console.log(this.register.tipoDocumento);
}

  validateForm(text){
    
    if("terminosCondiciones")
       if(this.terminosCondicionesValue)
           this.terminosCondicionesValid=true;
           else
           this.terminosCondicionesValid=false;
    if("nombre"==text)
      if(this.register.nombre!=''){
          this.validate.nombre.css="is-valid";
          this.validate.nombre.message="Dato Valido";
          this.validate.nombre.valid=true;
        }else{
          this.validate.nombre.css="is-invalid";
          this.validate.nombre.message="Dato requerido";
          this.validate.nombre.valid=false;
      }
    if("apellido"==text)
      if(this.register.apellido!=''){
          this.validate.apellido.css="is-valid";
          this.validate.apellido.message="Dato Valido";
          this.validate.apellido.valid=true;
        }else{
          this.validate.apellido.css="is-invalid";
          this.validate.apellido.message="Dato requerido";
          this.validate.apellido.valid=false;
      }
    if("username"==text)
      if(this.register.username!=''){
          this.validate.username.css="is-valid";
          this.validate.username.message="Dato Valido";
          this.validate.username.valid=true;
        }else{
          this.validate.username.css="is-invalid";
          this.validate.username.message="Dato requerido";
          this.validate.username.valid=false;
      }
    if("password"==text)
      if(this.register.password!=''){
          this.validate.password.css="is-valid";
          this.validate.password.message="Dato Valido";
          this.validate.password.valid=true;
          this.register.confirmarContrasena="";
        }else{
          this.validate.password.css="is-invalid";
          this.validate.password.message="Dato requerido";
          this.validate.password.valid=false;
      }
     if("confirmarContrasena"==text)
      if(this.register.confirmarContrasena==this.register.password){
          this.validate.confirmarContrasena.css="is-valid";
          this.validate.confirmarContrasena.message="Dato Valido";
          this.validate.confirmarContrasena.valid=true;
          console.log("igual");
        }else{
          this.validate.confirmarContrasena.css="is-invalid";
          this.validate.confirmarContrasena.message="Las contraseñas no coiciden";
          this.validate.confirmarContrasena.valid=false;
          console.log("no igual");
      }

    if("telefono"==text)
      if(this.register.telefono>0){
          this.validate.telefono.css="is-valid";
          this.validate.telefono.message="Dato Valido";
          this.validate.telefono.valid=true;
        }else{
          this.validate.telefono.css="is-invalid";
          this.validate.telefono.message="Dato requerido";
          this.validate.telefono.valid=false;
      }
    if("correo"==text)
      if(this.register.correo!=''){
          this.validate.correo.css="is-valid";
          this.validate.correo.message="Dato Valido";
          this.validate.correo.valid=true;
        }else{
          this.validate.correo.css="is-invalid";
          this.validate.correo.message="Dato requerido";
          this.validate.correo.valid=false;
      }

      if("ciudad"==text)
      if(this.register.ciudad!=''){
          this.validate.ciudad.css="is-valid";
          this.validate.ciudad.message="Dato Valido";
          this.validate.ciudad.valid=true;
        }else{
          this.validate.ciudad.css="is-invalid";
          this.validate.ciudad.message="Dato requerido";
          this.validate.ciudad.valid=false;
      }

    if("documento"==text)
      if(this.register.documento>0){
          this.validate.documento.css="is-valid";
          this.validate.documento.message="Dato Valido";
          this.validate.documento.valid=true;
        }else{
          this.validate.documento.css="is-invalid";
          this.validate.documento.message="Dato requerido";
          this.validate.documento.valid=false;
      }

    

     /* ciudad:{valid:true, message:"",css:""},
    departamento:{valid:true, message:"",css:""},
    pais:{valid:true, message:"",css:""},
    confirmarContrasena:{valid:true, message:"",css:""},
    checkTarjeta:{valid:true, message:"",css:""}*/


  }
}
