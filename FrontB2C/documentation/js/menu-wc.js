'use strict';


customElements.define('compodoc-menu', class extends HTMLElement {
    constructor() {
        super();
        this.isNormalMode = this.getAttribute('mode') === 'normal';
    }

    connectedCallback() {
        this.render(this.isNormalMode);
    }

    render(isNormalMode) {
        let tp = lithtml.html(`
        <nav>
            <ul class="list">
                <li class="title">
                    <a href="index.html" data-type="index-link">turesbalon documentation</a>
                </li>

                <li class="divider"></li>
                ${ isNormalMode ? `<div id="book-search-input" role="search"><input type="text" placeholder="Type to search"></div>` : '' }
                <li class="chapter">
                    <a data-type="chapter-link" href="index.html"><span class="icon ion-ios-home"></span>Getting started</a>
                    <ul class="links">
                        <li class="link">
                            <a href="overview.html" data-type="chapter-link">
                                <span class="icon ion-ios-keypad"></span>Overview
                            </a>
                        </li>
                        <li class="link">
                            <a href="index.html" data-type="chapter-link">
                                <span class="icon ion-ios-paper"></span>README
                            </a>
                        </li>
                        <li class="link">
                            <a href="dependencies.html" data-type="chapter-link">
                                <span class="icon ion-ios-list"></span>Dependencies
                            </a>
                        </li>
                    </ul>
                </li>
                    <li class="chapter modules">
                        <a data-type="chapter-link" href="modules.html">
                            <div class="menu-toggler linked" data-toggle="collapse" ${ isNormalMode ?
                                'data-target="#modules-links"' : 'data-target="#xs-modules-links"' }>
                                <span class="icon ion-ios-archive"></span>
                                <span class="link-name">Modules</span>
                                <span class="icon ion-ios-arrow-down"></span>
                            </div>
                        </a>
                        <ul class="links collapse" ${ isNormalMode ? 'id="modules-links"' : 'id="xs-modules-links"' }>
                            <li class="link">
                                <a href="modules/AppModule.html" data-type="entity-link">AppModule</a>
                                    <li class="chapter inner">
                                        <div class="simple menu-toggler" data-toggle="collapse" ${ isNormalMode ?
                                            'data-target="#components-links-module-AppModule-4bbe063d91f205315e879943f58a8224"' : 'data-target="#xs-components-links-module-AppModule-4bbe063d91f205315e879943f58a8224"' }>
                                            <span class="icon ion-md-cog"></span>
                                            <span>Components</span>
                                            <span class="icon ion-ios-arrow-down"></span>
                                        </div>
                                        <ul class="links collapse" ${ isNormalMode ? 'id="components-links-module-AppModule-4bbe063d91f205315e879943f58a8224"' :
                                            'id="xs-components-links-module-AppModule-4bbe063d91f205315e879943f58a8224"' }>
                                            <li class="link">
                                                <a href="components/AppComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">AppComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/BuscadorComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">BuscadorComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/CampaingComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">CampaingComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/CarroComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">CarroComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/CrearOrdenComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">CrearOrdenComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/DatalleEventoComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">DatalleEventoComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/DatosUsuarioComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">DatosUsuarioComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/DetalleOrdenComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">DetalleOrdenComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/FechaBuscadorComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">FechaBuscadorComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/HotelesComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">HotelesComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/LoginComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">LoginComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/MessageComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">MessageComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/NavegadorComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">NavegadorComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/OrdenesComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">OrdenesComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/PanelBuscadorComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">PanelBuscadorComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/PaquetesComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">PaquetesComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/PasosCompraComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">PasosCompraComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/ProductoComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">ProductoComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/RegistroComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">RegistroComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/TableComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">TableComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/UserComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">UserComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/VuelosComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">VuelosComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/homeComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">homeComponent</a>
                                            </li>
                                        </ul>
                                    </li>
                                <li class="chapter inner">
                                    <div class="simple menu-toggler" data-toggle="collapse" ${ isNormalMode ?
                                        'data-target="#injectables-links-module-AppModule-4bbe063d91f205315e879943f58a8224"' : 'data-target="#xs-injectables-links-module-AppModule-4bbe063d91f205315e879943f58a8224"' }>
                                        <span class="icon ion-md-arrow-round-down"></span>
                                        <span>Injectables</span>
                                        <span class="icon ion-ios-arrow-down"></span>
                                    </div>
                                    <ul class="links collapse" ${ isNormalMode ? 'id="injectables-links-module-AppModule-4bbe063d91f205315e879943f58a8224"' :
                                        'id="xs-injectables-links-module-AppModule-4bbe063d91f205315e879943f58a8224"' }>
                                        <li class="link">
                                            <a href="injectables/AuthenticationService.html"
                                                data-type="entity-link" data-context="sub-entity" data-context-id="modules" }>AuthenticationService</a>
                                        </li>
                                        <li class="link">
                                            <a href="injectables/StorageConfigService.html"
                                                data-type="entity-link" data-context="sub-entity" data-context-id="modules" }>StorageConfigService</a>
                                        </li>
                                        <li class="link">
                                            <a href="injectables/StorageParamsCompraService.html"
                                                data-type="entity-link" data-context="sub-entity" data-context-id="modules" }>StorageParamsCompraService</a>
                                        </li>
                                        <li class="link">
                                            <a href="injectables/StorageParamsService.html"
                                                data-type="entity-link" data-context="sub-entity" data-context-id="modules" }>StorageParamsService</a>
                                        </li>
                                        <li class="link">
                                            <a href="injectables/StorageService.html"
                                                data-type="entity-link" data-context="sub-entity" data-context-id="modules" }>StorageService</a>
                                        </li>
                                    </ul>
                                </li>
                            </li>
                            <li class="link">
                                <a href="modules/AppRoutingModule.html" data-type="entity-link">AppRoutingModule</a>
                            </li>
                            <li class="link">
                                <a href="modules/MaterialModule.html" data-type="entity-link">MaterialModule</a>
                            </li>
                </ul>
                </li>
                    <li class="chapter">
                        <div class="simple menu-toggler" data-toggle="collapse" ${ isNormalMode ? 'data-target="#classes-links"' :
                            'data-target="#xs-classes-links"' }>
                            <span class="icon ion-ios-paper"></span>
                            <span>Classes</span>
                            <span class="icon ion-ios-arrow-down"></span>
                        </div>
                        <ul class="links collapse" ${ isNormalMode ? 'id="classes-links"' : 'id="xs-classes-links"' }>
                            <li class="link">
                                <a href="classes/Ano.html" data-type="entity-link">Ano</a>
                            </li>
                            <li class="link">
                                <a href="classes/buscadorPaginacion.html" data-type="entity-link">buscadorPaginacion</a>
                            </li>
                            <li class="link">
                                <a href="classes/DialogData.html" data-type="entity-link">DialogData</a>
                            </li>
                            <li class="link">
                                <a href="classes/Evento.html" data-type="entity-link">Evento</a>
                            </li>
                            <li class="link">
                                <a href="classes/FileFlatNode.html" data-type="entity-link">FileFlatNode</a>
                            </li>
                            <li class="link">
                                <a href="classes/franquicia.html" data-type="entity-link">franquicia</a>
                            </li>
                            <li class="link">
                                <a href="classes/GraphData.html" data-type="entity-link">GraphData</a>
                            </li>
                            <li class="link">
                                <a href="classes/hotel.html" data-type="entity-link">hotel</a>
                            </li>
                            <li class="link">
                                <a href="classes/InfoDateFilter.html" data-type="entity-link">InfoDateFilter</a>
                            </li>
                            <li class="link">
                                <a href="classes/Meses.html" data-type="entity-link">Meses</a>
                            </li>
                            <li class="link">
                                <a href="classes/orden.html" data-type="entity-link">orden</a>
                            </li>
                            <li class="link">
                                <a href="classes/orden-1.html" data-type="entity-link">orden</a>
                            </li>
                            <li class="link">
                                <a href="classes/PageDat.html" data-type="entity-link">PageDat</a>
                            </li>
                            <li class="link">
                                <a href="classes/Pais.html" data-type="entity-link">Pais</a>
                            </li>
                            <li class="link">
                                <a href="classes/paqueteInfo.html" data-type="entity-link">paqueteInfo</a>
                            </li>
                            <li class="link">
                                <a href="classes/ParameterInfo.html" data-type="entity-link">ParameterInfo</a>
                            </li>
                            <li class="link">
                                <a href="classes/parametrosBusqueda.html" data-type="entity-link">parametrosBusqueda</a>
                            </li>
                            <li class="link">
                                <a href="classes/Producto.html" data-type="entity-link">Producto</a>
                            </li>
                            <li class="link">
                                <a href="classes/Session.html" data-type="entity-link">Session</a>
                            </li>
                            <li class="link">
                                <a href="classes/tipoDocumento.html" data-type="entity-link">tipoDocumento</a>
                            </li>
                            <li class="link">
                                <a href="classes/transporte.html" data-type="entity-link">transporte</a>
                            </li>
                            <li class="link">
                                <a href="classes/User.html" data-type="entity-link">User</a>
                            </li>
                        </ul>
                    </li>
                        <li class="chapter">
                            <div class="simple menu-toggler" data-toggle="collapse" ${ isNormalMode ? 'data-target="#injectables-links"' :
                                'data-target="#xs-injectables-links"' }>
                                <span class="icon ion-md-arrow-round-down"></span>
                                <span>Injectables</span>
                                <span class="icon ion-ios-arrow-down"></span>
                            </div>
                            <ul class="links collapse" ${ isNormalMode ? 'id="injectables-links"' : 'id="xs-injectables-links"' }>
                                <li class="link">
                                    <a href="injectables/AuthenticationService.html" data-type="entity-link">AuthenticationService</a>
                                </li>
                                <li class="link">
                                    <a href="injectables/CarritoService.html" data-type="entity-link">CarritoService</a>
                                </li>
                                <li class="link">
                                    <a href="injectables/ProductsService.html" data-type="entity-link">ProductsService</a>
                                </li>
                                <li class="link">
                                    <a href="injectables/StorageConfigService.html" data-type="entity-link">StorageConfigService</a>
                                </li>
                                <li class="link">
                                    <a href="injectables/StorageParamsCompraService.html" data-type="entity-link">StorageParamsCompraService</a>
                                </li>
                                <li class="link">
                                    <a href="injectables/StorageParamsService.html" data-type="entity-link">StorageParamsService</a>
                                </li>
                                <li class="link">
                                    <a href="injectables/StorageService.html" data-type="entity-link">StorageService</a>
                                </li>
                                <li class="link">
                                    <a href="injectables/UserInfoService.html" data-type="entity-link">UserInfoService</a>
                                </li>
                            </ul>
                        </li>
                    <li class="chapter">
                        <div class="simple menu-toggler" data-toggle="collapse" ${ isNormalMode ? 'data-target="#guards-links"' :
                            'data-target="#xs-guards-links"' }>
                            <span class="icon ion-ios-lock"></span>
                            <span>Guards</span>
                            <span class="icon ion-ios-arrow-down"></span>
                        </div>
                        <ul class="links collapse" ${ isNormalMode ? 'id="guards-links"' : 'id="xs-guards-links"' }>
                            <li class="link">
                                <a href="guards/RemoveSession.html" data-type="entity-link">RemoveSession</a>
                            </li>
                        </ul>
                    </li>
                    <li class="chapter">
                        <div class="simple menu-toggler" data-toggle="collapse" ${ isNormalMode ? 'data-target="#interfaces-links"' :
                            'data-target="#xs-interfaces-links"' }>
                            <span class="icon ion-md-information-circle-outline"></span>
                            <span>Interfaces</span>
                            <span class="icon ion-ios-arrow-down"></span>
                        </div>
                        <ul class="links collapse" ${ isNormalMode ? ' id="interfaces-links"' : 'id="xs-interfaces-links"' }>
                            <li class="link">
                                <a href="interfaces/DataInfo.html" data-type="entity-link">DataInfo</a>
                            </li>
                        </ul>
                    </li>
                    <li class="chapter">
                        <div class="simple menu-toggler" data-toggle="collapse" ${ isNormalMode ? 'data-target="#miscellaneous-links"'
                            : 'data-target="#xs-miscellaneous-links"' }>
                            <span class="icon ion-ios-cube"></span>
                            <span>Miscellaneous</span>
                            <span class="icon ion-ios-arrow-down"></span>
                        </div>
                        <ul class="links collapse" ${ isNormalMode ? 'id="miscellaneous-links"' : 'id="xs-miscellaneous-links"' }>
                            <li class="link">
                                <a href="miscellaneous/functions.html" data-type="entity-link">Functions</a>
                            </li>
                            <li class="link">
                                <a href="miscellaneous/variables.html" data-type="entity-link">Variables</a>
                            </li>
                        </ul>
                    </li>
                    <li class="chapter">
                        <a data-type="chapter-link" href="coverage.html"><span class="icon ion-ios-stats"></span>Documentation coverage</a>
                    </li>
                    <li class="divider"></li>
                    <li class="copyright">
                        Documentation generated using <a href="https://compodoc.app/" target="_blank">
                            <img data-src="images/compodoc-vectorise.png" class="img-responsive" data-type="compodoc-logo">
                        </a>
                    </li>
            </ul>
        </nav>
        `);
        this.innerHTML = tp.strings;
    }
});