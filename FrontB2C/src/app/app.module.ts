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
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { ReactiveFormsModule } from '@angular/forms';

//servicios
import {StorageService} from "./storage/storage.service";
import {StorageConfigService} from "./storage/storage-config.service";
import {StorageParamsService} from "./storage/storage-params.service";
import {StorageParamsCompraService} from "./storage/storage-compra";
import {RemoveSession} from "./service/remove-session.service";
import { AuthenticationService } from './service/authentication.service';
import { DatosUsuarioComponent } from './component/datos-usuario/datos-usuario.component';
import { OrdenesComponent } from './component/ordenes/ordenes.component';
import { UserComponent } from './component/user/user.component';
import { PanelBuscadorComponent } from './component/panel-buscador/panel-buscador.component';
import { DetalleOrdenComponent } from './component/detalle-orden/detalle-orden.component';
import { DatalleEventoComponent } from './component/datalle-evento/datalle-evento.component';
import { PasosCompraComponent } from './component/pasos-compra/pasos-compra.component';
import { NavegadorComponent } from './component/navegador/navegador.component';
import { FechaBuscadorComponent } from './component/fecha-buscador/fecha-buscador.component';
import { CrearOrdenComponent } from './component/crear-orden/crear-orden.component';

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
    TableComponent,
    PanelBuscadorComponent,
    DetalleOrdenComponent,
    DatalleEventoComponent,
    PasosCompraComponent,
    NavegadorComponent,
    FechaBuscadorComponent,
    CrearOrdenComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
     //Angular material iniciio
     MaterialModule, 
     BrowserAnimationsModule,
     //Angular material fin
     FormsModule,
     HttpClientModule,
     environment.production ? ServiceWorkerModule.register('ngsw-worker.js') : [],
     ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),  
  ],
  entryComponents: [ MessageComponent,DetalleOrdenComponent, DatalleEventoComponent],
  providers: [StorageService, StorageParamsService, RemoveSession, AuthenticationService,StorageConfigService,StorageParamsCompraService
],
  bootstrap: [AppComponent]
})
export class AppModule { }
