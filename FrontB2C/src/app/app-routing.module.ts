import { NgModule } from '@angular/core';
import { RouterModule, Routes, Route } from '@angular/router';

import { homeComponent } from './component/home/home.component';
import { BuscadorComponent } from './component/buscador/buscador.component';
import { LoginComponent } from './component/login/login.component';
import { RegistroComponent } from './component/registro/registro.component';
import { RemoveSession } from './service/remove-session.service';
import { OrdenesComponent } from './component/ordenes/ordenes.component';
import { DatosUsuarioComponent } from './component/datos-usuario/datos-usuario.component';
import { HotelesComponent } from './component/hoteles/hoteles.component';
import { PaquetesComponent } from './component/paquetes/paquetes.component';
import { VuelosComponent } from './component/vuelos/vuelos.component';
import { CarroComponent } from './component/carro/carro.component';
import { CrearOrdenComponent } from './component/crear-orden/crear-orden.component';
import { PasarelaComponent } from './component/pasarela/pasarela.component';
//Constante que almacena las rutas de la app
const routes: Route[] = [
  { path: '', redirectTo: '/', pathMatch: 'full' },
  { path: "", component: BuscadorComponent},
  {path: "home", component: BuscadorComponent},
  {path: "registro", component: RegistroComponent},
  {path: "datosusuario", component: DatosUsuarioComponent},
  {path: "ordenesusuario", component: OrdenesComponent},
  {path: "paquetes", component: PaquetesComponent},
  {path: "paquetes/:categoria/:evento/:cantidad/:option", component: PaquetesComponent},
  {path: "carro", component: CarroComponent},
  {path: "transporte", component: VuelosComponent},
  { path: "login", component: LoginComponent},
  { path: "hotel", component: HotelesComponent},
  { path: "crearOrden", component: CrearOrdenComponent},
  { path: "pasarela", component: PasarelaComponent},
  { path: "logout", component: LoginComponent, canActivate: [RemoveSession] },
  //, canActivate: [RemoveSession] },
 /* children: [
    { path: 'products', component: CatalogoComponent },
    { path: 'carrito', component: CarritoComponent },
    { path: 'overview', component: OverviewComponent },
  ]},
 // { path: "", component: LoginComponent, canActivate: [UserLocalSession] },
  { path: "login", component: LoginComponent, canActivate: [RemoveSession] },
  {
    path: "home", component: HomeComponent, canActivate: [ValidateSession],
    children: [{ path: 'qualification', component: QualificationComponent },
    { path: 'cotizacion', component: CotizacionesComponent },
   { path: 'tracing', component: TracingComponent  },
    { path: 'qualification/ratingdetail', component: RatingDetailComponent },
    { path: 'behavior', component:BehaviorComponent },
    { path: 'report', component:ReportComponent },
    { path:'visual',component:WatsonVisualComponent }
  ]
  },*/
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }



