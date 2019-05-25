import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {MaterialModule} from './material.module';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import {TestComponent} from './test/test.component';
import { HomeComponent } from './Componentes/home/home.component';
import { ClientesComponent } from './Componentes/clientes/clientes.component';
import { CampanasComponent } from './Componentes/campanas/campanas.component';
import { ProductosComponent } from './Componentes/productos/productos.component';
import { OrdenesComponent } from './Componentes/ordenes/ordenes.component';
import { LoginComponent } from './Componentes/login/login.component';
import { MessageComponent } from './Componentes/message/message.component';

//servicios
import {StorageService} from "./storage/storage.service";
import {StorageConfigService} from "./storage/storage-config.service";
import {StorageParamsService} from "./storage/storage-params.service";
import {RemoveSession} from "./service/remove-session.service";
import {ValidateSession} from "./service/validatesession.service";
import { AuthenticationService } from './service/authentication.service';
import { DatalleCampanaComponent } from './Componentes/datalle-campana/datalle-campana.component';
import { DatalleProductoComponent } from './Componentes/datalle-producto/datalle-producto.component';
import { DetalleClienteComponent } from './Componentes/detalle-cliente/detalle-cliente.component';
import { PasarelapagoComponent } from './Componentes/pasarelapago/pasarelapago.component';

@NgModule({
  declarations: [
    AppComponent,
    TestComponent,
    HomeComponent,
    ClientesComponent,
    CampanasComponent,
    ProductosComponent,
    OrdenesComponent,
    LoginComponent,
    MessageComponent,
    DatalleCampanaComponent,
    DatalleProductoComponent,
    DetalleClienteComponent,
    PasarelapagoComponent
  ],
  imports: [
    BrowserModule,
    MaterialModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FormsModule,
     HttpClientModule  
  ],
  entryComponents: [ MessageComponent, DatalleCampanaComponent,DatalleProductoComponent, DetalleClienteComponent],
  providers: [StorageService, StorageParamsService,ValidateSession, RemoveSession, AuthenticationService,StorageConfigService
],
  bootstrap: [AppComponent]
})
export class AppModule { }
