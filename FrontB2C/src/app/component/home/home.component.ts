import { Component, OnInit } from '@angular/core';
import { MediaMatcher} from '@angular/cdk/layout';
import { ChangeDetectorRef, OnDestroy} from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import { AppComponent } from '../../app.component';
import { element } from 'protractor';
import { Router } from '@angular/router';

import { StorageService } from '../../storage/storage.service'
/*
import {StorageCotizacionService} from '../../storage/Storage-cotizacion.service';
import {StorageSelectedBotService} from "../../storage/storage.selectedbot";
import { ParameterInfo } from '../../ParameterInfo';

import { ParameterService } from '../../Services/parameter.service';
*/


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class homeComponent implements OnInit {

 sesionUser:boolean=false;

  //Variables del menu material
  mobileQuery: MediaQueryList;
  private _mobileQueryListener: () => void;
  //Variables
  navMenuHome = {"state":"/home","title":"Home","icon":"dashboard"}
  navMenuHotel = {"state":"/hotel","title":"Hoteles","icon":"hotel"}
  navMenuPaquetes = {"state":"/paquetes","title":"Paquetes","icon":"business_center"}
  navMenuTransporte = {"state":"/transporte","title":"Transporte","icon":"flight"}
  navMenuUsuario = []
  navMenuCerrarSesion = {"state":"/login","title":"Cerrar Sesión","icon":"exit_to_app"} 
  navMenuLogin = {"state":"/login","title":"Iniciar Sessión","icon":"lock"} 
  navMenuProductos = {"state":"/products","title":"Ver Productos","icon":"add_shopping_cart"} 
  
  navMenu = [this.navMenuHome,this.navMenuPaquetes, this. navMenuTransporte,this.navMenuHotel,this.navMenuCerrarSesion];//Variable que contiene la lista de menú
  navMenuChatBot = [];
  botSelected:any;
  tituloBot:string;
  saludo:string;

  verCarrito=false;
  listCarrito :any;
  totalCarrito=0;
  cantidadProductos=0;
  constructor(changeDetectorRef: ChangeDetectorRef, media: MediaMatcher,private parent: AppComponent,
    //private cotizacion: StorageCotizacionService ,
    private sesion:StorageService, 
    public router: Router ) {

    //this.botSelected=  this.selectBot.getSelectedBotSession();
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);

      //Obtiene la ultima actualización en el localstorage
     // var paramInfo = new ParameterInfo();
    /*  this.servicio.lastUpdateParams(paramInfo,result =>{
         this.getMenuChatBot();
     });*/
     this.cargarCotizacion();
  }

  cargarCotizacion(){
    //Capturar Carrito
    console.log("entra a cotizar");
   /* this.cotizacion.loadSessionData();
    this.listCarrito= this.cotizacion.getCotizacionSession();
    console.log(this.listCarrito);
    if(this.listCarrito){
      this.cantidadProductos=0;
      this.totalCarrito=0;
      this.verCarrito = true;
      this.listCarrito.forEach(element => {
        this.totalCarrito=  (element.precio * element.cantidad )+ this.totalCarrito;
        this.cantidadProductos=this.cantidadProductos+ (1 * element.cantidad );
        console.log(this.cantidadProductos);
      });
     }else{
      this.listCarrito=[];
     }*/
  }
    //Invoca servicio de consulta  del menu de cada chatbot
  getMenuChatBot(){
    //Invoca servicio de consulta del menu de cada chatbot
  /* this.servicio.getMenuChatBot(result =>{
     if(result.code==200) {
      result.data.forEach(element => {
        var menuItem= {"state":element.link,"title":element.titulo,"icon":element.icono};
        this.navMenuChatBot.push(menuItem);
      });

    this.navMenuUsuario = result.data;  
    }else{
      this.parent.openDialog( "","Servidor no disponible","Alerta");
    }
  });*/

  this.navMenuChatBot.push( this.navMenuProductos);
    this.navMenuChatBot.push(this.navMenuLogin);
}

  //funcion que actualiza el menu con las opciones que tiene habilidada cada chatbot
  onMenuChatBotClick(chatBotSelected) {
    this.navMenu = []
  /*  this.navMenu.push(this.navMenuHome);
    this.navMenuUsuario.forEach(chatBotMenuItem => {
      if (chatBotMenuItem.titulo == chatBotSelected[0].title) {
        this.botSelected=chatBotMenuItem; //Carga el bot seleccionado
        this.tituloBot = this.botSelected.titulo;
        this.selectBot.setSelectedBotSession(this.botSelected);
        chatBotMenuItem.ChatBot.forEach(element => {
          var menuItem= {"state":element.link,"title":element.titulo,"icon":element.icono};
          this.navMenu.push(menuItem);
        })
      }
    })

    
    this.navMenu.push(this.navMenuLogin);
    this.navMenu.push( this.navMenuProductos);
      this.navMenu.push(this.navMenuLogin);
      */
  }

  ngOnInit(): void {
    this.cargarCotizacion();
    console.log("Inicia Home");
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.validateSession();
    
    /*
    this.navMenu = []
    this.navMenu.push(this.navMenuHome)
    this.navMenu.push( this.navMenuProductos);
      this.navMenu.push(this.navMenuLogin);
    let bot = this.selectBot.getSelectedBotSession()
    if(bot)
      if(bot!=null){
        this.tituloBot = bot.titulo
        console.log(bot)
        bot.ChatBot.forEach(element =>{
          let menuItem = {"state":element.link,"title":element.titulo,"icon":element.icono};
          this.navMenu.push(menuItem)
        })
        this.navMenu.push(this.navMenuCerrarSesion)
      }
      this.navMenu.push(this.navMenuLogin);
    this.navMenu.push( this.navMenuProductos);
      this.navMenu.push(this.navMenuLogin);*/
  }


  validateSession() {
    if(this.sesion.loadSessionData()){
      this.sesionUser=true;
      console.log("Usuario  registrado");
      this.saludo = `Bienvenido ${this.sesion.loadSessionData().user.username}`
    }else{
      this.sesionUser=false;
      this.saludo="";
      console.log("Usuario no registrado");
    }
  }
  
  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

  shouldRun = [/(^|\.)plnkr\.co$/, /(^|\.)stackblitz\.io$/].some(h => h.test(window.location.host));

}

