import { NgModule } from '@angular/core';
import { RouterModule, Routes, Route } from '@angular/router';
import {TestComponent} from './test/test.component';
import { HomeComponent } from './Componentes/home/home.component';
import { ClientesComponent } from './Componentes/clientes/clientes.component';
import { CampanasComponent } from './Componentes/campanas/campanas.component';
import { ProductosComponent } from './Componentes/productos/productos.component';
import { OrdenesComponent } from './Componentes/ordenes/ordenes.component';
import { LoginComponent } from './Componentes/login/login.component';
import { ValidateSession } from './service/validatesession.service';
import { RemoveSession } from './service/remove-session.service';
//Constante que almacena las rutas de la app
const routes: Route[] = [
 { path: '', redirectTo: '/', pathMatch: 'full', canActivate: [ValidateSession]},
  //{ path: "", component: HomeComponent},
  {path: "login", component: LoginComponent},
  {path: "Campañas", component: CampanasComponent, canActivate: [ValidateSession]},
  {path: "Productos", component: ProductosComponent, canActivate: [ValidateSession]},
  {path: "Clientes", component: ClientesComponent, canActivate: [ValidateSession]},
  {path: "Ordenes", component: OrdenesComponent, canActivate: [ValidateSession]},
  { path: "logout", component: LoginComponent, canActivate: [RemoveSession] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }



