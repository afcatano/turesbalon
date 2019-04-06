import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MaterialModule} from './material.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { homeComponent } from './component/home/home.component';
import { BuscadorComponent } from './component/buscador/buscador.component';
import { ProductoComponent } from './component/producto/producto.component';
import { PaquetesComponent } from './component/paquetes/paquetes.component';
import { HotelesComponent } from './component/hoteles/hoteles.component';
import { VuelosComponent } from './component/vuelos/vuelos.component';
import { LoginComponent } from './component/login/login.component';
import { RegistroComponent } from './component/registro/registro.component';
import { CarroComponent } from './component/carro/carro.component';
import { MessageComponent} from './component/message/message.component';
import { TableComponent} from './component/table/table.component';

//servicios
import {StorageService} from "./storage/storage.service";
import {StorageParamsService} from "./storage/storage-params.service";
import {RemoveSession} from "./service/remove-session.service";
import { AuthenticationService } from './service/authentication.service';
import { DatosUsuarioComponent } from './component/datos-usuario/datos-usuario.component';
import { OrdenesComponent } from './component/ordenes/ordenes.component';
import { UserComponent } from './component/user/user.component';

@NgModule({
  declarations: [
    AppComponent,
    homeComponent,
    BuscadorComponent,
    ProductoComponent,
    PaquetesComponent,
    HotelesComponent,
    VuelosComponent,
    LoginComponent,
    RegistroComponent,
    CarroComponent,
    MessageComponent,
    DatosUsuarioComponent,
    OrdenesComponent,
    UserComponent,
    TableComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
     //Angular material iniciio
     MaterialModule, 
     BrowserAnimationsModule,
     //Angular material fin
     FormsModule,
     HttpClientModule
  ],
  entryComponents: [ MessageComponent],
  providers: [StorageService, StorageParamsService, RemoveSession, AuthenticationService
],
  bootstrap: [AppComponent]
})
export class AppModule { }
