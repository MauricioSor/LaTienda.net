using Sistema.Datos;
using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Negocio
{
    public class NArticulo
    {
        public static DataTable Listar()
        {
            DArticulo Datos = new DArticulo();
            return Datos.Listar();
        }
        public static DataTable Buscar(string Valor)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Buscar(Valor);
        }
        public static string Insertar(int IdCategoria,string Nombre, string Codigo, decimal PrecioDeVenta,int Stock,string Imagen, string Descripcion)
        {
            DArticulo Datos = new DArticulo();
            string Existe = Datos.Existe(Nombre);
            if (Existe.Equals(1))
            {
                return "La Articulo ya existe";
            }
            else
            {
                Articulo Obj = new Articulo();
                Obj.IdCategoria = IdCategoria;
                Obj.Codigo = Codigo;
                Obj.Nombre = Nombre;
                Obj.Stock= Stock;
                Obj.PrecioVenta = PrecioDeVenta;
                Obj.Imagen = Imagen;
                Obj.Descripcion = Descripcion;
                return Datos.Insertar(Obj);
            }
        }
        public static string Actualizar(int Id,int IdCategoria, string NombreAnterior,string Codigo,decimal PrecioDeVenta,string Imagen,int Stock, string Nombre, string Descripcion)
        {
            DArticulo Datos = new DArticulo();
            Articulo Obj = new Articulo();
            if (NombreAnterior.Equals(Nombre))
            {
                Obj.IdArticulo = Id;
                Obj.IdCategoria = IdCategoria;
                Obj.Codigo = Codigo;
                Obj.Nombre = Nombre;
                Obj.Stock = Stock;
                Obj.PrecioVenta = PrecioDeVenta;
                Obj.Descripcion=Descripcion;
                Obj.Imagen = Imagen;
                return Datos.Actualizar(Obj);
            }
            else
            {
                string Existe = Datos.Existe(Nombre);
                if (Existe.Equals("1"))
                {
                    return "La Articulo ya existe";
                }
                else
                {
                    Obj.IdArticulo = Id;
                    Obj.IdCategoria = IdCategoria;
                    Obj.Codigo = Codigo;
                    Obj.Nombre = Nombre;
                    Obj.Stock = Stock;
                    Obj.PrecioVenta = PrecioDeVenta;
                    Obj.Descripcion = Descripcion;
                    Obj.Imagen = Imagen;
                    return Datos.Actualizar(Obj);
                }
            }
        }
        public static string Eliminar(int Id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Eliminar(Id);

        }
        public static string Activar(int Id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Activar(Id);
        }
        public static string Desactivar(int Id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Desactivar(Id);
        }
        public static string Existe(int Id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Desactivar(Id);
        }

    }
}
