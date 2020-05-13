using System;
using System.Collections.Generic;

namespace AgregarCarrito_openShop
{
    class GestorVentas
    {
        static Carrito Carrito = new Carrito();
        static List<FormasPago> FormasPagos = new List<FormasPago>();

        static void Main(string[] args)
        {
            FormasPagos.Add(new FormasPago("Tarjeta en 6 cuotas sin interés"));
            FormasPagos.Add(new FormasPago("Débito"));

            while (true)
            {
                var finalizado = Comprar();

                if (finalizado)
                {
                    break;
                }
            }

            AgregarPago();
            System.Console.WriteLine("Gracias por su compra, vuelva pronto");
        }

        static public bool Comprar()
        {
            RegistroProductos.MostrarProductos();

            System.Console.WriteLine();
            System.Console.WriteLine("Seleccione un producto");

            var opcionProducto = System.Console.ReadLine();
            var producto = RegistroProductos.Productos[int.Parse(opcionProducto) - 1];

            Carrito.Agregar(producto);

            System.Console.WriteLine();
            System.Console.WriteLine("Introduzca la cantidad de productos que desea comprar:");
            var opcionCantidad = System.Console.ReadLine();
            int cantidadElegida = (int.Parse(opcionCantidad));

            Carrito.MostrarCarrito(cantidadElegida);

            System.Console.WriteLine("");
            System.Console.WriteLine("Digite 1 para seguir comprando, 2 para abonar los productos del carrito");
            var opcionSeguir = System.Console.ReadLine();

            if (int.Parse(opcionSeguir) == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static public void AgregarPago()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Formas de pago:");
            int pos = 1;
            foreach (var pago in FormasPagos)
            {
                System.Console.WriteLine(pos + "- " + pago.Tipo);
                pos++;
            }

            System.Console.WriteLine();
            System.Console.WriteLine("Seleccione una forma de pago (Digite 1 o 2)");
            var seleccion2 = System.Console.ReadLine();
            var formasPagos = FormasPagos[int.Parse(seleccion2) - 1];
            System.Console.WriteLine("La forma de pago elegida fue: " + formasPagos.Tipo);
            System.Console.WriteLine("");

            // Aqui podriamos convertir el carrito en un objeto Venta y asignar la forma de pago
        }
    }

    class Cantidad
    {
        public int Unidad { get; set; }

        public Cantidad(int unidad)
        {
            Unidad = unidad;
        }
    }
    class FormasPago
    {
        public string Tipo { get; set; }
        public FormasPago(string tipo)
        {
            Tipo = tipo;
        }

    }
    class Carrito
    {
        private List<Producto> Productos = new List<Producto>();

        public void Agregar(Producto producto)
        {
            Productos.Add(producto);
        }

        public void MostrarCarrito(int cantidadElegida)
        {
            decimal sumaCarrito = 0;
            System.Console.WriteLine("");
            System.Console.WriteLine("Tienes en tu carrito: ");

            foreach (var productoEnCarrito in Productos)
            {
                System.Console.WriteLine(cantidadElegida + "x " + productoEnCarrito.Nombre + " " + productoEnCarrito.Marca + " $" + productoEnCarrito.Precio + " c/u");
                sumaCarrito = sumaCarrito + (productoEnCarrito.Precio * cantidadElegida);
                System.Console.WriteLine("Total: $" + sumaCarrito);
            }
        }
    }

    class Producto
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Marca { get; set; }

        public Producto(string nombre, decimal precio, string marca)
        {
            Nombre = nombre;
            Precio = precio;
            Marca = marca;
        }
    }
    class RegistroProductos
    {
        public static List<Producto> Productos = new List<Producto>();
        static RegistroProductos()
        {
            Productos.Add(new Producto("Cafetera", 3000, "Philips"));
            Productos.Add(new Producto("Celular", 249999.99m, "Apple"));
            Productos.Add(new Producto("Televisor", 22000, "Sony"));
            Productos.Add(new Producto("Ojotas", 700, "Havaianas"));
            Productos.Add(new Producto("Teclado", 6500.99m, "Razer"));
        }

        static public void MostrarProductos()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("OPEN SHOP");
            System.Console.WriteLine("Listado de productos:");
            int pos = 1;
            foreach (var producto in Productos)
            {
                System.Console.WriteLine(pos + "-" + producto.Nombre + " " + producto.Marca + " $" + producto.Precio);
                pos++;
            }
        }
    }
}
