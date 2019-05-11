import { Component, OnInit } from '@angular/core';

import {Router} from "@angular/router";
import {User } from '../../Models/User';
import { Session } from '../../Models/session';
import { AppComponent } from '../../app.component';
import { Observable } from 'rxjs';
import { StorageService } from '../../storage/storage.service';
import { AuthenticationService } from '../../service/authentication.service';

import {StorageConfigService}from '../../storage/storage-config.service'; 


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  username: string;
  password: string;
  processing: boolean;
  error:string;
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

  constructor(private servicio:AuthenticationService,
    private parent: AppComponent 
    ,private storageService: StorageService
    ,private router: Router,
   private storage:StorageConfigService) {
    this.username=null;
    this.password= null;
    this.processing = false;
    this.error = null;
  }

  ngOnInit(){
    this.verifiSesion();
  }

  onSubmit() {
    if(this.username && this.password) {
      this.processing=true;
      console.log(this.username, this.password);
      var sessionData= new Session();
      var config= this.storage.getConfigSession();

      //Valida si aplica mock
      if(config.login){
       // var use = user;
       // sessionData.user=use;
       // this.correctLogin(sessionData);
      }else
      this.servicio.signIn(new User(this.username, this.password,"","","","","","","","","","","","","","","","","")).subscribe(
        result => {
             console.log(result);
             this.processing = false;
            if(result.codigo=="0") {
              console.log("usuario logueado "+this.username);
              this.parent.openDialog( "","Usuario  "+this.username+" autenticado !!","Informativo");
              var userData= new User( 
                this.username,
                this.password,
                result.nombre,
                result.apellido,
                result.telefono,
                result.correo,
                result.ciudad,
                result.departamento,
                result.pais,
                this.password,
                result.checkTarjeta,
                result.direccion,
                result.tipoDocumento,
                result.documento,
                result.codigoTarjeta,
                result. fechaTarjeta,
                result.numeroTarjeta,
                result. franquiciaTarjeta,
                result. userid,
              )

              sessionData.user=userData;
              this.correctLogin(sessionData);
            } else {
              console.log(JSON.stringify(result, null, 4));
              this.parent.openDialog( "",result.mensaje,"Alerta");
            }
        },
        error => {
            console.log(error);
            this.processing = false;
            this.parent.openDialog( "","Servidor no disponible","Alerta");
            
        })
    } else {
      this.parent.openDialog( "","Debe completar todos los campos","Alerta");
    }
  }
 
  private correctLogin(data: Session){
    this.storageService.setCurrentSession(data);
    console.log("redireccion");
    //this.verifiSesion();
    this.router.navigate(['/']);
  }

  verifiSesion(){
   // this.home.validateSession();
  }

  discardNotification(){
    this.error = null;
  }


  
  validateForm(text){
      
    
    if("username"==text)
      if(this.username!=''){
          this.validate.username.css="is-valid";
          this.validate.username.message="Dato Valido";
          this.validate.username.valid=true;
        }else{
          this.validate.username.css="is-invalid";
          this.validate.username.message="Dato requerido";
          this.validate.username.valid=false;
      }
    if("password"==text)
      if(this.password!=''){
          this.validate.password.css="is-valid";
          this.validate.password.message="Dato Valido";
          this.validate.password.valid=true;
        }else{
          this.validate.password.css="is-invalid";
          this.validate.password.message="Dato requerido";
          this.validate.password.valid=false;
      }
     

  }
}

