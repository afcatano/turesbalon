import { NgModule } from '@angular/core';
import { RouterModule, Routes, Route } from '@angular/router';
import {TestComponent} from './test/test.component';
import { HomeComponent } from './Componentes/home/home.component';
import { ClientesComponent } from './Componentes/clientes/clientes.component';
import { CampanasComponent } from './Componentes/campanas/campanas.component';
import { ProductosComponent } from './Componentes/productos/productos.component';
import { OrdenesComponent } from './Componentes/ordenes/ordenes.component';

//Constante que almacena las rutas de la app
const routes: Route[] = [
 { path: '', redirectTo: '/', pathMatch: 'full' },
  //{ path: "", component: HomeComponent},
  {path: "Campañas", component: CampanasComponent},
  {path: "Productos", component: ProductosComponent},
  {path: "Clientes", component: ClientesComponent},
  {path: "Ordenes", component: OrdenesComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }



