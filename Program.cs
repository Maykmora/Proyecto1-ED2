using Proyecto1ED2;

class Program
{
    static void Main(string[] args)
    {
        ArbolBPlus arbol = new ArbolBPlus();
        arbol.Insertar(new Libro("001", "Cien años de soledad", "Gabriel García Márquez", "Novela", 3));
        arbol.Imprimir();
    }
}

//esto es una prueba de funcionamiento con archivos separados