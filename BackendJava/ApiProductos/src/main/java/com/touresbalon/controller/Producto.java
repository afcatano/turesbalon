package com.touresbalon.controller;


import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;

import org.springframework.cache.annotation.Cacheable;

@Entity
@Table(name = "Producto")
@Cacheable("Producto")
public class Producto {
	 
	@Id
	@Column(name = "id")
	Integer Id;
	 
	@Column(name = "espectaculo")
	String nombre;
	
	
	@Column(name = "codigo")
	String codigo;
	
	@Column(name = "descripcion")
	String descripcion;
	
	@Column(name = "ciudad")
	String ciudad;
	
	@Column(name = "fecha")
	Date fecha;
	
	@Column(name = "horainicio")
	String horaInicio;
	
	@Column(name = "horafin")
	String horaFin;
	
	@Column(name = "precio")
	Long precio;
	
	@Column(name = "cantidad")
	Long cantidad;
	

	@Column(name = "imagen")
	String imagen;
	
	@Column(name = "usuario")
	String usuario;

	public Integer getId() {
		return Id;
	}

	public void setId(Integer id) {
		Id = id;
	}

	public String getNombre() {
		return nombre;
	}

	public void setNombre(String nombre) {
		this.nombre = nombre;
	}

	public String getCodigo() {
		return codigo;
	}

	public void setCodigo(String codigo) {
		this.codigo = codigo;
	}

	public String getDescripcion() {
		return descripcion;
	}

	public void setDescripcion(String descripcion) {
		this.descripcion = descripcion;
	}

	public String getCiudad() {
		return ciudad;
	}

	public void setCiudad(String ciudad) {
		this.ciudad = ciudad;
	}

	public Date getFecha() {
		return fecha;
	}

	public void setFecha(Date fecha) {
		this.fecha = fecha;
	}

	public String getHoraInicio() {
		return horaInicio;
	}

	public void setHoraInicio(String horaInicio) {
		this.horaInicio = horaInicio;
	}

	public String getHoraFin() {
		return horaFin;
	}

	public void setHoraFin(String horaFin) {
		this.horaFin = horaFin;
	}

	public Long getPrecio() {
		return precio;
	}

	public void setPrecio(Long precio) {
		this.precio = precio;
	}

	public Long getCantidad() {
		return cantidad;
	}

	public void setCantidad(Long cantidad) {
		this.cantidad = cantidad;
	}

	public String getImagen() {
		return imagen;
	}

	public void setImagen(String imagen) {
		this.imagen = imagen;
	}

	public String getUsuario() {
		return usuario;
	}

	public void setUsuario(String usuario) {
		this.usuario = usuario;
	}

	

	
}
