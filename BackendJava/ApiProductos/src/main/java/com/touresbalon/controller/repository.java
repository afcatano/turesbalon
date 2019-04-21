package com.touresbalon.controller;

import java.util.List;
 
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;
 
@Repository
public interface repository extends CrudRepository<Producto, Long> {
 
    @Query("FROM Producto")
    public List<Producto> getAll();
}



