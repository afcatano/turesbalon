import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {MaterialModule} from './material.module';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import {TestComponent} from './test/test.component';
import { HomeComponent } from './Componentes/home/home.component';
import { ClientesComponent } from './Componentes/clientes/clientes.component';
import { CampanasComponent } from './Componentes/campanas/campanas.component';
import { ProductosComponent } from './Componentes/productos/productos.component';
import { OrdenesComponent } from './Componentes/ordenes/ordenes.component';

@NgModule({
  declarations: [
    AppComponent,
    TestComponent,
    HomeComponent,
    ClientesComponent,
    CampanasComponent,
    ProductosComponent,
    OrdenesComponent
  ],
  imports: [
    BrowserModule,
    MaterialModule,
    BrowserAnimationsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
