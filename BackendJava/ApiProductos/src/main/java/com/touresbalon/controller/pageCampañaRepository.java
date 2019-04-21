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
public interface pageCampañaRepository extends JpaRepository<Campañas, Integer>, PagingAndSortingRepository<Campañas, Integer> {

    
	@Query("FROM Campañas")
	public List<Producto> getAll();
	

	@Cacheable("Campaña")
	@Query(value = "SELECT * FROM campañas  WHERE fechafin >= SYSDATETIME() and cantidad > 0 ", 
			  nativeQuery = true)
	List<Campañas> findCampanasActive( PageRequest pageRequest);
	
	@Cacheable("CountCampaña")
	@Query(value = "SELECT count(1) FROM campañas  WHERE fechafin >= SYSDATETIME() and cantidad > 0 ", 
			  nativeQuery = true)
	int findCampanasActiveCount();
	
	
	
	
}