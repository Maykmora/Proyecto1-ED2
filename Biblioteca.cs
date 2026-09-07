
namespace Proyecto1ED2
{
    public class Biblioteca
    {
        private ArbolBPlus arbol;
        private MinHeap heapMin;
        private MaxHeap heapMax;

        public Biblioteca()
        {
            arbol = new ArbolBPlus();
            heapMin = new MinHeap();
            heapMax = new MaxHeap();
        }

        public void RegistrarLibro(Libro libro)
        {
            arbol.Insertar(libro);
            heapMin.Insertar(libro);
            heapMax.Insertar(libro);
        }

        public Libro BuscarLibro(string codigo)
        {
            return arbol.Buscar(codigo);
        }

        public string PrestarLibro(string codigo)
        {
            Libro libro = arbol.Buscar(codigo);

            if (libro == null)
            {
                return "No existe un libro con ese codigo.";
            }

            if (libro.CopiasDisponibles <= 0)
            {
                return "No hay copias disponibles para prestar este libro.";
            }

            libro.CopiasDisponibles--;
            libro.VecesPrestado++;

            ReconstruirHeaps();

            return $"Prestamo registrado. Copias disponibles: {libro.CopiasDisponibles}";
        }

        public string DevolverLibro(string codigo)
        {
            Libro libro = arbol.Buscar(codigo);

            if (libro == null)
            {
                return "No existe un libro con ese codigo.";
            }

            libro.CopiasDisponibles++;

            ReconstruirHeaps();

            return $"Devolucion registrada. Copias disponibles: {libro.CopiasDisponibles}";
        }

        private void ReconstruirHeaps()
        {
            Libro[] libros = arbol.ObtenerTodos();

            heapMin = new MinHeap();
            heapMax = new MaxHeap();

            for (int i = 0; i < libros.Length; i++)
            {
                heapMin.Insertar(libros[i]);
                heapMax.Insertar(libros[i]);
            }
        }

        public MinHeap ObtenerHeapMin()
        {
            return heapMin;
        }

        public MaxHeap ObtenerHeapMax()
        {
            return heapMax;
        }

        public ArbolBPlus ObtenerArbol()
        {
            return arbol;
        }

            
        public string EliminarLibro(string codigo)
        {
            Libro libro = arbol.Buscar(codigo);

            if (libro == null)
            {
                return "No existe un libro con ese codigo.";
            }

            arbol.Eliminar(codigo);
            ReconstruirHeaps();

            return $"Libro '{libro.Titulo}' eliminado del catalogo.";
        }

        public string CargarDesdeArchivo(string rutaArchivo)
        {
            if (!System.IO.File.Exists(rutaArchivo))
            {
                return "El archivo no existe en la ruta indicada.";
            }

            string[] lineas = System.IO.File.ReadAllLines(rutaArchivo);

            int cargados = 0;
            int errores = 0;

            for (int i = 0; i < lineas.Length; i++)
            {
                string linea = lineas[i].Trim();

                if (linea.Length == 0)
                {
                    continue;
                }

                string[] partes = linea.Split(',');

                if (partes.Length != 5)
                {
                    Console.WriteLine($"Linea {i + 1} invalida (se esperaban 5 campos): {linea}");
                    errores++;
                    continue;
                }

                string codigo = partes[0].Trim();
                string titulo = partes[1].Trim();
                string autor = partes[2].Trim();
                string categoria = partes[3].Trim();

                int copias;
                bool copiasValidas = int.TryParse(partes[4].Trim(), out copias);

                if (!copiasValidas)
                {
                    Console.WriteLine($"Linea {i + 1} invalida (copias no es un numero): {linea}");
                    errores++;
                    continue;
                }

                if (arbol.Buscar(codigo) != null)
                {
                    Console.WriteLine($"Linea {i + 1} omitida (codigo duplicado): {codigo}");
                    errores++;
                    continue;
                }

                Libro libro = new Libro(codigo, titulo, autor, categoria, copias);
                RegistrarLibro(libro);
                cargados++;
            }

            return $"Carga finalizada. Libros cargados: {cargados}. Lineas con error: {errores}.";
        }

    
        public void MostrarCatalogoPorTitulo()
        {
            Libro[] libros = arbol.ObtenerTodos();
            OrdenarPorTitulo(libros);

            Console.WriteLine("=== Catalogo ordenado por titulo ===");
            for (int i = 0; i < libros.Length; i++)
            {
                Console.WriteLine($"{libros[i].Codigo} | {libros[i].Titulo} | {libros[i].Autor} | {libros[i].Categoria} | Copias: {libros[i].CopiasDisponibles}");
            }
        }

        private void OrdenarPorTitulo(Libro[] libros)
        {
            for (int i = 1; i < libros.Length; i++)
            {
                Libro actual = libros[i];
                int j = i - 1;

                while (j >= 0 && string.Compare(libros[j].Titulo, actual.Titulo) > 0)
                {
                    libros[j + 1] = libros[j];
                    j--;
                }

                libros[j + 1] = actual;
            }
        }

    }   
}