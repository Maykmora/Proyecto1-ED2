using System;
using Proyecto1ED2;

class Program
{
    static void Main(string[] args)
    {
        ArbolBPlus arbol = new ArbolBPlus();
        MaxHeap maxHeap = new MaxHeap();
        MinHeap minHeap = new MinHeap();

        while (true)
        {
            Console.WriteLine("\n=== MENU BIBLIOTECA ===");
            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Buscar libro por código");
            Console.WriteLine("3. Mostrar catálogo (B+)");
            Console.WriteLine("4. Top libros más prestados (MaxHeap)");
            Console.WriteLine("5. Libro con menos copias (MinHeap)");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Código: ");
                    string codigo = Console.ReadLine();
                    Console.Write("Título: ");
                    string titulo = Console.ReadLine();
                    Console.Write("Autor: ");
                    string autor = Console.ReadLine();
                    Console.Write("Categoría: ");
                    string categoria = Console.ReadLine();
                    Console.Write("Copias disponibles: ");
                    int copias = int.Parse(Console.ReadLine());

                    Libro libro = new Libro(codigo, titulo, autor, categoria, copias);

                    arbol.Insertar(libro);
                    maxHeap.Insertar(libro);
                    minHeap.Insertar(libro);
                    Console.WriteLine("Libro registrado.");
                    break;

                case "2":
                    Console.Write("Ingrese código: ");
                    string buscarCodigo = Console.ReadLine();
                    var encontrado = arbol.Buscar(buscarCodigo);
                    if (encontrado != null)
                    {
                        Console.WriteLine($"Libro encontrado:");
                        Console.WriteLine($"Titulo: {encontrado.Titulo}| {encontrado.Autor}");
                        Console.WriteLine($"Categoria: {encontrado.Categoria}");
                        Console.WriteLine($"Copias disponibles: {encontrado.CopiasDisponibles}");
                    }
                    else
                        Console.WriteLine("No se encontró el libro.");
                    break;

                case "3":
                    arbol.Imprimir();
                    break;

                case "4":
                    maxHeap.MostrarTopPrestados(5);
                    break;

                case "5":
                    var minimo = minHeap.ObtenerMinimo();
                    if (minimo != null)
                        Console.WriteLine($"Libro con menos copias: {minimo.Titulo} ({minimo.CopiasDisponibles} copias)");
                    else
                        Console.WriteLine("No hay libros registrados.");
                    break;

                case "6":
                    return;
            }
        }
    }
}
