import { Component, OnInit,Input,Output,EventEmitter , Inject } from '@angular/core';
import {User } from '../../Models/User';
import  {Paises} from '../../mock/paises';
import  {anos} from '../../mock/anos';
import  {meses} from '../../mock/mes';
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

  userDisabled:boolean=false;
  register:User;
  dataMeses = meses;
  dataAnos = anos;
  dataPaises = Paises;
  dataFranquicias= franquicias;
  dataTiposDocumento = TiposDocumento;
  terminosCondicionesValue:boolean;
  terminosCondiciones:boolean;
  tipoDocumentoSeleccionado:any;
  terminosCondicionesValid:boolean=true;
  dataFechaTarjeta={dataAnos:"", dataMeses:""};
  
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

    this.register = new User( "","","","","","","","","","","","","","","","","","", "","");
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
                      this.userdata. franquiciaTarjeta,
                      this.userdata.userid,
                      "")
                      this.register.confirmarContrasena= this.register.password;
                      this.userDisabled = true;
                     break;
       case  "create":
       
                      this.terminosCondiciones= true;
                      this.register = new User( "","","","","","","","","","","","","","","","","","","","");
                      break;
        default:
              break;
      }
    
       console.log(this.page);
  }

  
  onSubmit() {
    var validate=false;
    var msg="Debe completar todos los campos";
    
    this.register.fechaTarjeta = this.dataFechaTarjeta.dataMeses + this.dataFechaTarjeta.dataAnos;
   console.log(this.register.fechaTarjeta);
   console.log("tarjeta");
    if(this.terminosCondiciones){
       if(!this.terminosCondicionesValue){
           msg="Debe aceptar los terminos y condiciones";
           this.terminosCondicionesValid=false;
         }else{
          validate=this.validateSumitForms();
         }
        }else{
          validate=this.validateSumitForms();
        }

        
   if(validate) {
      console.log("Ejecuta accion.");
      if(!this.register.checkTarjeta)
      this.register.checkTarjeta=false;

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
       this.register. franquiciaTarjeta,
       this.register.userid,
       ""
      ));
    } else {
      this.parent.openDialog( "",msg,"Alerta");
    }
  }
 
  capturar() {
    // Pasamos el valor seleccionado a la variable verSeleccion
   console.log(this.register.tipoDocumento);
}


validateSumitForms(){
  if(
    this.validateNombre()&&
    this.validateApellido()&&
    this.validateUserName()&&
    this.validatePassword()&&
    this.validateConfirmaPass()&&
    this.validateTelefono()&&
    this.validateCorreo() &&
    this.validateCiudad()&&
    this.validateDocumento()&&
    this.validatePais()){

    
      return true;
    }else{
      return false;
    }
}

  validateTerminos(){
    if(this.terminosCondicionesValue)
    return this.terminosCondicionesValid=true;
    else
    return this.terminosCondicionesValid=false;
  }

  validateNombre(){
    if(this.register.nombre!=''){
      this.validate.nombre.css="is-valid";
      this.validate.nombre.message="Dato Valido";
      return this.validate.nombre.valid=true;
    }else{
      this.validate.nombre.css="is-invalid";
      this.validate.nombre.message="Dato requerido";
      return this.validate.nombre.valid=false;
  }
  }


  validateApellido(){
    if(this.register.nombre!=''){
      this.validate.nombre.css="is-valid";
      this.validate.nombre.message="Dato Valido";
      return  this.validate.nombre.valid=true;
    }else{
      this.validate.nombre.css="is-invalid";
      this.validate.nombre.message="Dato requerido";
      return this.validate.nombre.valid=false;
  }
  }


  validateUserName(){
    if(this.register.username!=''){
      this.validate.username.css="is-valid";
      this.validate.username.message="Dato Valido";
      return this.validate.username.valid=true;
    }else{
      this.validate.username.css="is-invalid";
      this.validate.username.message="Dato requerido";
      return this.validate.username.valid=false;
  }
  }

  validatePassword(){
    if(this.register.password!=''){
      this.validate.password.css="is-valid";
      this.validate.password.message="Dato Valido";
      //this.register.confirmarContrasena="";
     if(this.register.confirmarContrasena)
      this.validateConfirmaPass();
      return this.validate.password.valid=true;
      
    }else{
      this.validate.password.css="is-invalid";
      this.validate.password.message="Dato requerido";
      return this.validate.password.valid=false;
  }
  }


  validateConfirmaPass(){
    if(this.register.confirmarContrasena==this.register.password){
      this.validate.confirmarContrasena.css="is-valid";
      this.validate.confirmarContrasena.message="Dato Valido";
      return this.validate.confirmarContrasena.valid=true;
      console.log("igual");
    }else{
      this.validate.confirmarContrasena.css="is-invalid";
      this.validate.confirmarContrasena.message="Las contraseñas no coiciden";
      return this.validate.confirmarContrasena.valid=false;
      console.log("no igual");
  }
  }

  validateTelefono(){
   if(this.register.telefono>0){
      this.validate.telefono.css="is-valid";
      this.validate.telefono.message="Dato Valido";
      return  this.validate.telefono.valid=true;
    }else{
      this.validate.telefono.css="is-invalid";
      this.validate.telefono.message="Dato requerido";
      return this.validate.telefono.valid=false;
  }
  }

  validateCorreo(){
    if(this.register.correo!=''){
      if(this.validarEmail(this.register.correo))
      {this.validate.correo.css="is-valid";
      this.validate.correo.message="Dato Valido";
      return  this.validate.correo.valid=true;
    }else{
      this.validate.correo.css="is-invalid";
      this.validate.correo.message="Correo no valido.";
      return  this.validate.correo.valid=false;
    }
    }else{
      this.validate.correo.css="is-invalid";
      this.validate.correo.message="Dato requerido";
      return  this.validate.correo.valid=false;
  }
  }

   validarEmail(email) {
    if (/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.([a-zA-Z]{2,4})+$/.test(email)){
      return (true)
    } else {
      return (false);
    }
  }

  validateCiudad(){
   if(this.register.ciudad!=''){
      this.validate.ciudad.css="is-valid";
      this.validate.ciudad.message="Dato Valido";
      return this.validate.ciudad.valid=true;
    }else{
      this.validate.ciudad.css="is-invalid";
      this.validate.ciudad.message="Dato requerido";
      return this.validate.ciudad.valid=false;
  }
  }

  validateDocumento(){
    if(this.register.documento>0){
      this.validate.documento.css="is-valid";
      this.validate.documento.message="Dato Valido";
      return this.validate.documento.valid=true;
    }else{
      this.validate.documento.css="is-invalid";
      this.validate.documento.message="Dato requerido";
      return this.validate.documento.valid=false;
  }
  }

  validatePais(){
    if(this.register.pais!=''){
      this.validate.pais.css="is-valid";
      this.validate.pais.message="Dato Valido";
      return this.validate.pais.valid=true;
    }else{
      this.validate.pais.css="is-invalid";
      this.validate.pais.message="Dato requerido";
      return this.validate.pais.valid=false;
  }
  }

  validateForm(text){
    
    if("terminosCondiciones")
    this.validateTerminos();
    if("nombre"==text)
    this.validateNombre();
    if("apellido"==text)
    this.validateApellido();
    if("username"==text)
     this.validateUserName();
    if("password"==text)
    this.validatePassword();
     if("confirmarContrasena"==text)
     this.validateConfirmaPass();
    if("telefono"==text)
    this.validateTelefono();
    if("correo"==text)
    this.validateCorreo()
     if("ciudad"==text)
     this.validateCiudad();
     if("documento"==text)
       this.validateDocumento();
      if("pais"==text)
      this.validatePais();
    

     /* ciudad:{valid:true, message:"",css:""},
    departamento:{valid:true, message:"",css:""},
    pais:{valid:true, message:"",css:""},
    confirmarContrasena:{valid:true, message:"",css:""},
    checkTarjeta:{valid:true, message:"",css:""}*/


  }
}
