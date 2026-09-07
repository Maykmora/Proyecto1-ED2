using System;
using Proyecto1ED2;

public class Program
{
    public static void Main(string[] args)
    {
        Biblioteca biblioteca = new Biblioteca();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("===== CATALOGO DE BIBLIOTECA =====");
            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Buscar libro");
            Console.WriteLine("3. Eliminar libro");
            Console.WriteLine("4. Mostrar catalogo por codigo");
            Console.WriteLine("5. Mostrar catalogo por titulo");
            Console.WriteLine("6. Registrar prestamo");
            Console.WriteLine("7. Registrar devolucion");
            Console.WriteLine("8. Mostrar libros mas prestados");
            Console.WriteLine("9. Mostrar informacion de las estructuras");
            Console.WriteLine("10. Cargar libros desde archivo");
            Console.WriteLine("11. Salir");
            Console.Write("Elige una opcion: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Codigo: ");
                    string codigoNuevo = Console.ReadLine();
                    Console.Write("Titulo: ");
                    string titulo = Console.ReadLine();
                    Console.Write("Autor: ");
                    string autor = Console.ReadLine();
                    Console.Write("Categoria: ");
                    string categoria = Console.ReadLine();
                    Console.Write("Copias disponibles: ");
                    int copias = int.Parse(Console.ReadLine());

                    Libro libroNuevo = new Libro(codigoNuevo, titulo, autor, categoria, copias);
                    biblioteca.RegistrarLibro(libroNuevo);
                    Console.WriteLine("Libro registrado correctamente.");
                    break;

                case "2":
                    Console.Write("Codigo a buscar: ");
                    string codigoBuscar = Console.ReadLine();
                    Libro encontrado = biblioteca.BuscarLibro(codigoBuscar);

                    if (encontrado == null)
                    {
                        Console.WriteLine("No se encontro un libro con ese codigo.");
                    }
                    else
                    {   
                        Console.WriteLine();
                        Console.WriteLine("Libro encontrado:");
                        Console.WriteLine($"Codigo: {encontrado.Codigo}");
                        Console.WriteLine($"Titulo: {encontrado.Titulo}");
                        Console.WriteLine($"Autor: {encontrado.Autor}");
                        Console.WriteLine($"Categoria: {encontrado.Categoria}");
                        Console.WriteLine($"Copias disponibles: {encontrado.CopiasDisponibles}");
                        Console.WriteLine($"Veces prestado: {encontrado.VecesPrestado}");
                    }
                    break;

                case "3":
                    Console.Write("Codigo a eliminar: ");
                    string codigoEliminar = Console.ReadLine();
                    Console.WriteLine(biblioteca.EliminarLibro(codigoEliminar));
                    break;

                case "4":
                    biblioteca.ObtenerArbol().Imprimir();
                    break;

                case "5":
                    biblioteca.MostrarCatalogoPorTitulo();
                    break;

                case "6":
                    Console.Write("Codigo a prestar: ");
                    string codigoPrestar = Console.ReadLine();
                    Console.WriteLine(biblioteca.PrestarLibro(codigoPrestar));
                    break;

                case "7":
                    Console.Write("Codigo a devolver: ");
                    string codigoDevolver = Console.ReadLine();
                    Console.WriteLine(biblioteca.DevolverLibro(codigoDevolver));
                    break;

                case "8":
                    Console.Write("¿Cuantos libros del top deseas ver?: ");
                    int cantidadTop = int.Parse(Console.ReadLine());
                    biblioteca.ObtenerHeapMax().MostrarTopPrestados(cantidadTop);
                    break;

                case "9":
                    biblioteca.ObtenerArbol().Imprimir();
                    biblioteca.ObtenerHeapMin().Imprimir();
                    biblioteca.ObtenerHeapMax().Imprimir();
                    break;

                case "10":
                    Console.Write("Ruta del archivo (.csv o .txt): ");
                    string ruta = Console.ReadLine();
                    Console.WriteLine(biblioteca.CargarDesdeArchivo(ruta));
                    break;

                case "11":
                    Console.WriteLine("Saliendo del sistema...");
                    return;

                default:
                    Console.WriteLine("Opcion invalida, intenta de nuevo.");
                    break;
            }
        }
    }
}
