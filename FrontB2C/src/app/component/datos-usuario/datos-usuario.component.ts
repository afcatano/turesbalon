


import { Component, OnInit, Inject } from '@angular/core';
import {PageDat } from '../../Models/pageData';
import {User } from '../../Models/User';
import { UserInfoService} from '../../service/user-info.service';
import { AppComponent } from '../../app.component';
import {Router} from "@angular/router";
import { StorageService } from '../../storage/storage.service';
import {Session } from '../../Models/session';

@Component({
  selector: 'app-datos-usuario',
  templateUrl: './datos-usuario.component.html',
  styleUrls: ['./datos-usuario.component.css']
})
export class DatosUsuarioComponent implements OnInit {
  pages:PageDat;
  processing: boolean;
  userInfo :User;
  constructor( private userService: UserInfoService, private parent : AppComponent, private storageService: StorageService
    ,private router: Router
  ,private sesion:StorageService) {

   }

  ngOnInit() {
    this.pages=new PageDat();
    this.pages.title="Mis Datos";
    this.pages.action="update";
    this.pages.button="Actualizar";

    //this.userInfo = new User( "andres","andres","andres","andres","andres","andres","andres","andres","","","","","","","","","","");

    this.userInfo = this.sesion.loadSessionData().user;
 }

  action(data:User){
     console.log("Eschua evento");
     this.processing=true;
     //console.log(this.username, this.password);
     this.userService.updateUser(data).subscribe(
       result => {
            this.processing = false;
           if(result.codigo=="0") {
             console.log(result);
             console.log("Se  actualizado los datos !!");
             this.parent.openDialog( "",result.mensaje+"!!","Informativo");
             var sessionData= new Session();
             sessionData.user=data;
             this.processing=false;
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
   }

 private correctLogin(data: Session){
    this.storageService.setCurrentSession(data);
  }
}
