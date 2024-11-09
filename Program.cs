using System; 
namespace GestiónInventario
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventario inventario = new Inventario();
            Console.WriteLine("Bienvenido al sistema de gestión de inventario");

            //Ingreso de productos por el usario.
            Console.WriteLine("¿Cuántos productos desea ingresar?");
            int cantidad;
            while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad <= 0)
            {
                Console.WriteLine("Por favor ingrese un numero positivo");
            }

            //Se usa el for para pedir exactamente la cantidad de productos que desea ingresar el usario.
            for (int i = 0; i < cantidad; i++)
            {
                Console.WriteLine($"\nProducto {i + 1} ");

                //Console.WriteLine("Nombre: ");
                //string nombre = Console.ReadLine();

                //Console.WriteLine("Precio: ");
                //decimal precio = decimal.Parse(Console.ReadLine());

                //validaciones...
                string nombre;
                do
                {
                    Console.WriteLine("Nombre: ");
                    nombre = Console.ReadLine();
                    //función vacía o blanca, será invalidado
                    if (string.IsNullOrWhiteSpace(nombre))
                    {
                        Console.WriteLine("El nombre del producto no puede estar vacío.");
                    }
                } while (string.IsNullOrWhiteSpace(nombre));

                decimal precio;
                do
                {
                    //validación para precio positivo
                    Console.WriteLine("Precio: ");
                    if (!decimal.TryParse(Console.ReadLine(), out precio) || precio <= 0)
                    {
                        Console.WriteLine("Por favor, ingrese un precio válido (número positivo).");
                    }
                } while (precio <= 0);

                Producto producto = new Producto(nombre, precio);
                inventario.AgregarProducto(producto);
            }

            //Ingresa el precio minimo para el filtro
            Console.WriteLine("\nIngrese el precio minimo para filtrar los productos: ");
            decimal precioMinimo = decimal.Parse(Console.ReadLine());

            //filtar y mostrar productos
            var productosFiltrados = inventario.FiltrarYOrdenarProductos(precioMinimo);

            Console.WriteLine("\nProductos filtrados y ordenados: ");
            foreach (var producto in productosFiltrados)
            {
                producto.MostrarDatos(); 
            }

            //Actualizar el precio de un producto
            Console.WriteLine("\n¿Desea actualizar el precio de algún producto? (s/n)");
            string respuesta = Console.ReadLine();
            if (respuesta.Equals("s", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Ingrese el nombre del producto que desea actualizar: ");
                string nombreProducto = Console.ReadLine();

                Console.WriteLine("Ingrese el nuevo precio: ");
                decimal nuevoPrecio = decimal.Parse(Console.ReadLine());

                //condicion de busqueda
                bool actualizado = inventario.ActualizarPrecioProductos(nombreProducto, nuevoPrecio);
                if (actualizado)
                {
                    Console.WriteLine("El precio ha sido actualizado");
                }else
                {
                    Console.WriteLine("Producto no encontrado");
                }

                //eliminar un producto, actualización de eliminacion 
                Console.WriteLine("\n¿Desea eliminar algún producto? (s/n)");
                respuesta = Console.ReadLine();
                if (respuesta.Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Ingrese el nombre del producto que desea eliminar:");
                    string nombreProductos = Console.ReadLine();

                    bool eliminado = inventario.EliminarProducto(nombreProducto);
                    if (eliminado)
                    {
                        Console.WriteLine("El producto ha sido eliminado.");
                    }
                    else
                    {
                        Console.WriteLine("Producto no encontrado.");
                    }

                    // Contar y agrupar productos por precio
                    var grupos = inventario.ContarYAgruparProductosPorPrecio();
                    Console.WriteLine("\nConteo de productos por rango de precios:");
                    foreach (var grupo in grupos)
                    {
                        Console.WriteLine($"{grupo.Key}: {grupo.Value}");
                    }
                }
            }
        }
    }
}