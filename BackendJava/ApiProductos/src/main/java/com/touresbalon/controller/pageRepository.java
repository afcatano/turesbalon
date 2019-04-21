package com.touresbalon.controller;

import java.util.List;

import org.springframework.boot.autoconfigure.data.web.SpringDataWebProperties.Pageable;
import org.springframework.cache.annotation.CacheConfig;
import org.springframework.cache.annotation.CacheEvict;
import org.springframework.cache.annotation.Cacheable;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.PagingAndSortingRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Repository;

@Repository
public interface pageRepository extends JpaRepository<Producto, Integer>, PagingAndSortingRepository<Producto, Integer> {

    
	@Query("FROM Producto")
	public List<Producto> getAll();
	
	
	
	@Query(value = "SELECT * FROM Producto u WHERE u.espectaculo='uefa' ", 
			  nativeQuery = true)
	List<Producto> findUserByStatusAndNameNamedParamsNative( PageRequest pageRequest);
	
	
	@Query(value = "SELECT * FROM Producto e WHERE e.espectaculo = ?1 ",
			nativeQuery = true)
	List<Producto> findByEspectaculo(  String status , PageRequest pageRequest);
	
	
	
	@Cacheable("Producto")
	@Query(value = "select DISTINCT id,espectaculo,descripcion ,ciudad,fecha ,horainicio,horafin ,precio,cantidad,imagen ,usuario,codigo from Producto where fecha between ?1 and ?2 and ( codigo like ?3 " + 
			" or espectaculo like ?3 or descripcion like ?3 )",
			nativeQuery = true)
	List<Producto> findByLikeEspectaculo( String dateIni, String dateFin, String name , PageRequest pageRequest);
	
	@Cacheable("CountProducto")
	@Query(value = "select DISTINCT  count(1) from Producto where fecha between ?1 and ?2 and ( codigo like ?3 " + 
			" or espectaculo like ?3 or descripcion like ?3 )",
			nativeQuery = true)
	int findByLikeEspectaculoCount( String dateIni, String dateFin, String name );
	
	
	//***************************************************************************///
	@Query(value = "SELECT * FROM Producto e WHERE e.fecha between ?1 and ?2 and e.espectaculo like %?3% ",
			nativeQuery = true)
	List<Producto> findByLikeEspectaculoFull( String dateIni, String dateFin, String name , PageRequest pageRequest);
	
	@Query(value = "SELECT count(*) FROM Producto e WHERE e.fecha between ?1 and ?2 and e.espectaculo like %?3% ",
			nativeQuery = true)
	int findByLikeEspectaculoFullCount( String dateIni, String dateFin, String name );
	
	
	@Query(value = "SELECT * FROM Producto e WHERE e.espectaculo like %?1 ",
			nativeQuery = true)
	List<Producto> findByLikeEspectaculoDesp(  String status , PageRequest pageRequest);
	
	@Query(value = "SELECT * FROM Producto e WHERE e.fecha between ?1 and ?2 and e.espectaculo like ?3% ",
			nativeQuery = true)
	List<Producto> findByLikeEspectaculoAnte(  String status , PageRequest pageRequest);
	
}