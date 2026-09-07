
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
    
    }   
}