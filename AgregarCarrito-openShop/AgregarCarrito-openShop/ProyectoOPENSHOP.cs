using System;
using System.Collections.Generic;

namespace AgregarCarrito_openShop
{
    class GestorVentas
    {
        static Carrito Carrito = new Carrito();
        static List<FormasPago> FormasPagos = new List<FormasPago>();
        static List<Venta> Ventas = new List<Venta>();

        static void Main(string[] args)
        {
            FormasPagos.Add(new FormasPago("Tarjeta en 6 cuotas sin interés"));
            FormasPagos.Add(new FormasPago("Débito"));

            while (true)
            {
                var finalizado = ProcesoDeVenta();

                if (finalizado) break;

            }

            System.Console.WriteLine("\nGracias por su compra, vuelva pronto");
        }

        static public bool Comprar()
        {
            RegistroProductos.MostrarProductos();

            System.Console.WriteLine("\nSeleccione un producto");

            while (true)
            {
                var opcionElegidaProducto = System.Console.ReadLine();
                if (int.TryParse(opcionElegidaProducto, out var value))
                {
                    if (value >= 1 && value <= RegistroProductos.Productos.Count)
                    {
                        var producto = RegistroProductos.Productos[int.Parse(opcionElegidaProducto) - 1];
                        
                        System.Console.WriteLine("\nIntroduzca la cantidad de productos que desea comprar:");
                        var opcionElegidaCantidad = System.Console.ReadLine();
                        int cantidadElegida = (int.Parse(opcionElegidaCantidad));

                        Carrito.Agregar(producto, cantidadElegida);
                        Carrito.MostrarCarrito();
                        break;
                    }
                    else System.Console.WriteLine("VALOR INGRESADO INCORRECTO, Ingrese un valor mayor a 1 y menor a " + RegistroProductos.Productos.Count);
                    
                }
            }

            System.Console.WriteLine("\n¿Que desea hacer? \n1- Seguir comprando \n2- Abonar los productos del carrito");
            
            while (true)
            {
                var opcionElegidaSeguir = System.Console.ReadLine();
                Console.Clear();
                if (int.TryParse(opcionElegidaSeguir, out var value))
                {
                    if (value >= 1 && value <= 2)
                    {
                        if (value == 1) return false;
                        
                        else return true;
                        
                    }
                    else System.Console.WriteLine("\nVALOR INGRESADO INCORRECTO, ingrese 1 o 2");

                }
            }
        }

        

        static public FormasPago AgregarPago()
        {
            System.Console.WriteLine("\n-Formas de pago:");
            int pos = 1;
            foreach (var pago in FormasPagos)
            {
                System.Console.WriteLine(pos + "- " + pago.Tipo);
                pos++;
            }

            System.Console.WriteLine("\nSeleccione una forma de pago:");

            FormasPago formasPagos;

            while (true)
            {
                var opcionElegidaPago = System.Console.ReadLine();
                if (int.TryParse(opcionElegidaPago, out var value))
                {
                    if (value >= 1 && value <= 2)
                    {
                        formasPagos = FormasPagos[value - 1];
                        break;
                    }

                    else System.Console.WriteLine("\nVALOR INGRESADO INCORRECTO, Ingrese 1 o 2");
                }
            }

            System.Console.WriteLine("\nLa forma de pago elegida fue: " + formasPagos.Tipo);
        
            return formasPagos;
        }

        static public bool ProcesoDeVenta()
        {
            Console.WriteLine("\nBienvenidos al menú de Open Shop, seleccione lo que desea hacer");
            Console.WriteLine("\n1- Comprar productos \n2- Preparar un pedido");

            while (true)
            {
                var opcionElegidaMenu = (Console.ReadLine());
                Console.Clear();
                if (int.TryParse(opcionElegidaMenu, out var value))
                {
                    if (value >= 1 && value <= 2)
                    {
                        if (value == 1)
                        {
                            while (true)
                            {
                                var finalizado = Comprar();

                                if (finalizado)
                                {
                                    break;
                                }
                            }

                            var pago = AgregarPago();
                            var productos = Carrito.ProductosDelCarrito();
                            var venta = new Venta(productos, pago);

                            Ventas.Add(venta);
                            break;
                        }

                        else
                        {
                            int contadorPedidos = 1;
                            Console.WriteLine("OPEN SHOP - Lista de pedidos");
                            
                            if (Ventas.Count > 0)
                            {
                                foreach (var venta in Ventas)
                                {
                                    Console.WriteLine("\n-Pedido N°:"  +contadorPedidos);
                                    Console.WriteLine("-Fecha:" + venta.Fecha.ToShortDateString());
                                    venta.Carrito.MostrarCarrito();
                                    venta.FormasPagos.MostrarFormasPagos();
                                    contadorPedidos++;
                                    Console.WriteLine("_________________________________________________");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No hay pedidos registrados");
                            }

                            break;
                        }

                    }
                    else System.Console.WriteLine("\nVALOR INGRESADO INCORRECTO, ingrese 1 o 2");
                }
            }

            Console.WriteLine("\nDesea seguir navegando en Open Shop?   \n1- Sí \n2- No");
            var opcionElegidaSeguir = int.Parse(Console.ReadLine());
            Console.Clear();
            if (opcionElegidaSeguir == 1) return false;
 
            else return true;
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
            Productos.Add(new Producto("Notebook", 300000, "Predator"));
        }

        static public void MostrarProductos()
        {
            System.Console.WriteLine("\nOPEN SHOP - Listado de productos:");
            
            int pos = 1;
            foreach (var producto in Productos)
            {
                System.Console.WriteLine(pos + "-" + producto.Nombre + " " + producto.Marca + " $" + producto.Precio);
                pos++;
            }
        }
    }
    class Carrito
    {
        private List<ProductoEnCarrito> Productos = new List<ProductoEnCarrito>();

        public void Agregar(Producto producto, int cantidad)
        {
            var prodEnCarrito = new ProductoEnCarrito();
            prodEnCarrito.Producto = producto;
            prodEnCarrito.Cantidad = cantidad;

            Productos.Add(prodEnCarrito);
        }

        public List<ProductoEnCarrito> ProductosDelCarrito()
        {
            return Productos;
        }

        public void MostrarCarrito()
        {
            System.Console.WriteLine("\nActualmente tienes en el carrito: ");

            decimal totalCarrito = 0;
            foreach (var productoEnCarrito in Productos)
            {
                var cantidad = productoEnCarrito.Cantidad;
                var precio = productoEnCarrito.Producto.Precio;
                var nombre = productoEnCarrito.Producto.Nombre;
                System.Console.WriteLine(cantidad + "x " + nombre + " $" + cantidad * precio);

                totalCarrito = totalCarrito + cantidad * precio;
            }

            System.Console.WriteLine("Total: $" + totalCarrito);
        }
    }
    class ProductoEnCarrito
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
    }

    class Venta
    {
        public FormasPago FormasPagos { get; set; }
        public Carrito Carrito { get; set; }
        public DateTime Fecha { get; set; }

        public Venta(List<ProductoEnCarrito> Productos, FormasPago formasPago)
        {
            Carrito = new Carrito();
            Fecha = DateTime.Now;
            FormasPagos = formasPago;

            foreach (var producto in Productos)
            {
                Carrito.Agregar(producto.Producto, producto.Cantidad);
            }
        }
    }

    class FormasPago
    {
        public string Tipo { get; set; }
        public FormasPago(string tipo)
        {
            Tipo = tipo;
        }

        public void MostrarFormasPagos()
        {
            Console.WriteLine("\nForma de pago: " + Tipo);
        }
    }
}

