﻿using Sistema.Datos;
using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Negocio
{
    public class NCategoria
    {
        public static DataTable Listar()
        {
            DCategoria Datos= new DCategoria();
            return Datos.Listar();
        }
        public static DataTable Buscar(string Valor)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Buscar(Valor);
        }
        public static string Insertar(string Nombre,string Descripcion)
        {
            DCategoria Datos = new DCategoria();
            string Existe = Datos.Existe(Nombre);
            if (Existe.Equals(1))
            {
                return "La categoria ya existe";
            }
            else
            {
            Categoria Obj = new Categoria();
            Obj.Nombre=Nombre;
            Obj.Descripcion = Descripcion;
            return Datos.Insertar(Obj);
            }
        }
        public static string Actualizar(int Id, string NombreAnterior, string Nombre, string Descripcion)
        {
            DCategoria Datos = new DCategoria();
            Categoria Obj = new Categoria();
            if (NombreAnterior.Equals(Nombre)){
                Obj.IdCategoria = Id;
                Obj.Nombre = Nombre;
                Obj.Descripcion = Descripcion;
                return Datos.Actualizar(Obj);
            }
            else
            {            
                string Existe = Datos.Existe(Nombre);
                if (Existe.Equals("1"))
                {
                    return "La categoria ya existe";
                }
                else
                {
                    Obj.IdCategoria = Id;
                    Obj.Nombre = Nombre;
                    Obj.Descripcion = Descripcion;
                    return Datos.Actualizar(Obj);
                }
            }
        }
        public static string Eliminar(int Id)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Eliminar(Id);

        }
        public static string Activar(int Id)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Activar(Id);
        }
        public static string Desactivar(int Id)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Desactivar(Id);
        }
        public static string Existe(int Id)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Desactivar(Id);
        }
    }
}
