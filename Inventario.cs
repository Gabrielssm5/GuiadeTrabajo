using System;
using System.Collections; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiónInventario
{
    public class Inventario
    {
        private List<Producto> productos;

        public Inventario()
        { 
            productos = new List<Producto>();
        }

        public void AgregarProducto(Producto producto)
        {
            productos.Add(producto);
        }

        public IEnumerable<Producto> FiltrarYOrdenarProductos(decimal precioMinimo)
        {
            //Filtrar y ordenar productos con LINQ y expresiones lambda
            return productos
             .Where(p => p.Precio > precioMinimo)//Filtra productos con precio mayor al minimo especificado
                 .OrderBy(p => p.Precio); //ordena los productos de menor a mayor precio
        }


        //metodo para actualizar el precio de un producto
        public bool ActualizarPrecioProductos (string nombre, decimal nuevoPrecio)
        {
            var producto = productos.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (producto != null)
            {
                producto.Precio = nuevoPrecio;
                return true;
            }
            return false;
        }
        //metodo para eliminar un producto ya existente
        public bool EliminarProducto(string nombre)
        {
            var producto = productos.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (producto != null)
            {
                productos.Remove(producto);
                return true;
            }
            return false;
        }
        //metodo que cuenta y agrupa segun rangos de precios predeterminados por el usuario
        public Dictionary<string, int> ContarYAgruparProductosPorPrecio()
        {
            var grupos = new Dictionary<string, int>
        {
            { "Menores a 100", productos.Count(p => p.Precio < 100) },
            { "Entre 100 y 500", productos.Count(p => p.Precio >= 100 && p.Precio <= 500) },
            { "Mayores a 500", productos.Count(p => p.Precio > 500) }
        };

            return grupos;
        }

    }
}
