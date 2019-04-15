import { Component, OnInit, Inject } from '@angular/core';
import {PageDat } from '../../Models/pageData';
import {User } from '../../Models/User';
import { UserInfoService} from '../../service/user-info.service';
import { AppComponent } from '../../app.component';
import {Router} from "@angular/router";
import { StorageService } from '../../storage/storage.service';
import {Session } from '../../Models/session';
import { homeComponent } from '../home/home.component';
@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {
  pages:PageDat;
  processing: boolean;
  constructor( private userService: UserInfoService, private parent : AppComponent, private storageService: StorageService,private home:homeComponent
    ,private router: Router) {

   }

  ngOnInit() {
    this.pages=new PageDat();
    this.pages.title="Datos registro";
    this.pages.action="create";
    this.pages.button="Registrar";
    

  }

  action(data:User){
     console.log("Eschua evento");
     //this.processing=true;
     //console.log(this.username, this.password);
     this.userService.registerUser(data).subscribe(
       result => {
             console.log(result);
             if(result.codigo=="0") {
             console.log("Usuario registrado "+data.username);
             this.parent.openDialog( "","Usuario Registrado!!","Informativo");
             data.userid=result.userid;
             this.correctLogin(data);
            } else {
              console.log(JSON.stringify(result, null, 4));
              this.parent.openDialog( "",result.mensaje,"Alerta");
            }
            this.processing=false;
       },
       error => {
           console.log(error);
           this.processing = false;
           this.parent.openDialog( "","Servidor no disponible","Alerta");
           
       })

   }

   verifiSesion(){
    this.home.validateSession();
  }

 private correctLogin(data: User){
    var sessionData= new Session();
    sessionData.user=data;
    this.storageService.setCurrentSession(sessionData);
    this.home.validateSession();
    this.router.navigate(['/']);
  }
}
