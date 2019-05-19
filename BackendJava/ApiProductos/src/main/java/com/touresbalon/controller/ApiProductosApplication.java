package com.touresbalon.controller;

import java.io.Console;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.data.web.SpringDataWebProperties.Pageable;
import org.springframework.cache.annotation.CacheEvict;
import org.springframework.cache.annotation.EnableCaching;
import org.springframework.context.annotation.Configuration;
import org.springframework.data.domain.PageRequest;
import org.springframework.scheduling.annotation.EnableScheduling;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;


@SpringBootApplication
@EnableCaching
@Configuration
@EnableScheduling
@RestController
public class ApiProductosApplication {
	
	@Autowired
	repository repository;
	
	@Autowired
	pageRepository page;
	
	@Autowired
	pageCampañaRepository pageCampaing;
	
	
	public static void main(String[] args) {
		SpringApplication.run(ApiProductosApplication.class, args);
	}
	
	@RequestMapping(value="/campaing", method=RequestMethod.POST)
	public ResponseCampañasDto campañas(@RequestBody RequestCampañasDTO request){
		
	    try {
			
    	    System.out.println("Entra camapañas");
    	    PageRequest pagerequest= PageRequest.of(request.pagina, request.tamanoPagina);
			List<Campañas> res = pageCampaing.findCampanasActive(pagerequest);
			int cantidad = pageCampaing.findCampanasActiveCount();
			
			System.out.println("Sale");
			System.out.println(cantidad);
			ResponseCampañasDto resDto  = new  ResponseCampañasDto();
			resDto.codigo="0";
			resDto.mensaje="";
			resDto.cantidadRegistros=cantidad;
			resDto.paginaActual=request.pagina;
			resDto.tamanoPagina=request.tamanoPagina;
			int tamano=request.tamanoPagina;
			resDto.totalPaginas=Math.round(cantidad/tamano);
			resDto.data=res;
			return  resDto;
        
         }catch(Exception e){
        	 ResponseCampañasDto resDto  = new  ResponseCampañasDto();
        	 resDto.codigo="01";
     		 resDto.mensaje="Error en la ejecución";
     		 System.out.print(e);
        	 return  resDto;
        }  
}
	
	@CacheEvict(allEntries = true, cacheNames = { "Campaña","CountCampaña" })
	@Scheduled(fixedDelay = 180000)
	@RequestMapping(value="/limpiarCacheCampaña", method=RequestMethod.GET)
	public void limpiarCacheCampañas(){
		System.out.println(
			      "Fixed delay task - " + System.currentTimeMillis() / 1000);
	}
	//*****************************************PRODUCTOS****************************///
	
	
	@CacheEvict(allEntries = true, cacheNames = { "Producto","CountProducto" })
	@Scheduled(fixedDelay = 180000)
	@RequestMapping(value="/limpiarCache", method=RequestMethod.GET)
	public void limpiarCache(){
		System.out.println(
			      "Fixed delay task - " + System.currentTimeMillis() / 1000);
	}
	
	@RequestMapping(value="/producto/{number}/{size}", method=RequestMethod.GET)
	public Iterable<Producto> tripByPage(@PathVariable("number")int pageNumber, @PathVariable("size")int pageSize){
	      
		  @SuppressWarnings("deprecation")
		  PageRequest pageRequest = new PageRequest(pageNumber,pageSize);
		  Iterable<Producto> res = page.findAll(pageRequest);
	      return  res;
	   }
	
	

	//API con paginación que Busca  de elementos dado un elemnto y una rango de fecha
	@RequestMapping(value="/producto", method=RequestMethod.POST)
	public ResponseDto likeByPageComodin(@RequestBody RequestProductoDTO producto){
	      try {
				
	    	    System.out.println(producto);
	    	    System.out.println("Entra");
	    	    System.out.println(producto.nombre);
				System.out.println(producto.descripcion);
				System.out.println(producto.codigo);
	    	    PageRequest pagerequest= PageRequest.of(producto.pagina, producto.tamanoPagina);
				List<Producto> res = page.findByLikeEspectaculo( producto.fechaInicial,  producto.fechaFinal,  producto.nombre ,pagerequest);
				int cantidad = page.findByLikeEspectaculoCount( producto.fechaInicial,  producto.fechaFinal,  producto.nombre );
				
				System.out.println("Sale");
				
				System.out.println(cantidad);
				ResponseDto resDto  = new  ResponseDto();
				resDto.codigo="0";
				resDto.mensaje="";
				resDto.cantidadRegistros=cantidad;
				resDto.paginaActual=producto.pagina;
				resDto.tamanoPagina=producto.tamanoPagina;
				int tamano=producto.tamanoPagina;
				resDto.totalPaginas=Math.round(cantidad/tamano);
				resDto.eventos=res;
				return  resDto;
	        
	         }catch(Exception e){
	        	 ResponseDto resDto  = new  ResponseDto();
	        	 resDto.codigo="01";
	     		 resDto.mensaje="Error en la ejecución";
	     		 System.out.print(e);
	        	 return  resDto;
	        }  
	}
	
	//API con paginación que Busca  de elementos dado un elemnto y una rango de fecha
	@RequestMapping(value="/producto/{number}/{size}/{dateIni}/{dateFin}/{name}", method=RequestMethod.GET)
	public ResponseDto likeByPage(@PathVariable("number")Integer pageNumber, @PathVariable("size")int pageSize,
			@PathVariable("dateIni")String dateIni,@PathVariable("dateFin")String dateFin,
			@PathVariable("name")String name){
      try {
			PageRequest pa= PageRequest.of(pageNumber, pageSize);
			List<Producto> res = page.findByLikeEspectaculoFull( dateIni,  dateFin,  name ,pa);
			int cantidad = page.findByLikeEspectaculoFullCount( dateIni,  dateFin,  name );
			
			
			System.out.println(name);
			
			System.out.println(cantidad);
			System.out.println(pa.getSort());
			ResponseDto resDto  = new  ResponseDto();
			resDto.codigo="0";
			resDto.mensaje="";
			resDto.cantidadRegistros=cantidad;
			resDto.paginaActual=pageNumber;
			resDto.tamanoPagina=pageSize;
			resDto.totalPaginas=Math.round(cantidad/pageSize);
			resDto.eventos=res;
			return  resDto;
        
         }catch(Exception e){
        	 ResponseDto resDto  = new  ResponseDto();
        	 resDto.codigo="01";
     		 resDto.mensaje="Error en la ejecución";
        	 return  resDto;
        }  
	   }
}
