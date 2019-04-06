import {Component, OnInit} from '@angular/core';
import {Session } from '../../Models/session';
import { StorageService } from '../../storage/storage.service';
import {User } from '../../Models/User';
import {UserInfoService} from '../../service/user-info.service'
import {AppComponent} from '../../app.component';
import {ordenes } from '../../mock/oredenes';
@Component({
  selector: 'app-ordenes',
  templateUrl: './ordenes.component.html',
  styleUrls: ['./ordenes.component.css']
})
export class OrdenesComponent implements OnInit {
  userInfo :User;
  ordenesCount:number;
  dataTitle = {
     fechas: 'fecha',valor: 'valor',evento: 'evento',transporte: 'transporte', hotel: "hotel", estado: 'estado', accion: 'accion'
  }
  infoTable: any[];
  processing:boolean;
  constructor(private sesion:StorageService, private userService: UserInfoService, private parent: AppComponent) { 

  }

  ngOnInit() {
    this.userInfo =this.sesion.loadSessionData().user;
    
    this.action();
}

  action(){
    console.log("Eschua evento");
    this.processing=true;
    //console.log(this.username, this.password);
    this.userService.odersUser(this.userInfo.username).subscribe(
      result => {
            console.log(result);
            this.infoTable = result;
            this.ordenesCount=this.infoTable.length; 
      },
      error => {
          console.log(error);
          this.processing = false;
          this.parent.openDialog( "","Servidor no disponible","Alerta");
          this.infoTable = ordenes;
      })
     
  }

  private correctLogin(data: User){
    var sessionData= new Session();
    sessionData.user=data;
   // this.storageService.setCurrentSession(sessionData);
    //this.router.navigate(['/']);
  }
}
