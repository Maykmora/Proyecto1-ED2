namespace Proyecto1ED2
{
    public class MaxHeap
    {
        private const int CAPACIDAD_INICIAL = 50;

        private Libro[] datos;
        private int cantidad;

        public MaxHeap()
        {
            datos = new Libro[CAPACIDAD_INICIAL];
            cantidad = 0;
        }
        public void Insertar(Libro libro)
        {
            if (cantidad == datos.Length)
            {
                RedimensionarArreglo();
            }

            datos[cantidad] = libro;
            FlotarHaciaArriba(cantidad);
            cantidad++;
        }

        private void RedimensionarArreglo()
        {
            Libro[] nuevo = new Libro[datos.Length * 2];
            for (int i = 0; i < datos.Length; i++)
            {
                nuevo[i] = datos[i];
            }
            datos = nuevo;
        }

        private void FlotarHaciaArriba(int indice)
        {
            while (indice > 0)
            {
                int padre = (indice - 1) / 2;

                if (datos[indice].VecesPrestado > datos[padre].VecesPrestado)
                {
                    Intercambiar(indice, padre);
                    indice = padre;
                }
                else
                {
                    break;
                }
            }
        }

        public Libro ObtenerMaximo()
        {
            if (cantidad == 0)
            {
                return null;
            }

            return datos[0];
        }


        public Libro Eliminar()
        {
            if (cantidad == 0)
            {
                return null;
            }

            Libro maximo = datos[0];

            cantidad--;
            datos[0] = datos[cantidad];
            datos[cantidad] = null;

            HundirHaciaAbajo(0);

            return maximo;
        }

        private void HundirHaciaAbajo(int indice)
        {
            while (true)
            {
                int izquierdo = indice * 2 + 1;
                int derecho = indice * 2 + 2;
                int mayor = indice;

                if (izquierdo < cantidad && datos[izquierdo].VecesPrestado > datos[mayor].VecesPrestado)
                {
                    mayor = izquierdo;
                }

                if (derecho < cantidad && datos[derecho].VecesPrestado > datos[mayor].VecesPrestado)
                {
                    mayor = derecho;
                }

                if (mayor == indice)
                {
                    break;
                }

                Intercambiar(indice, mayor);
                indice = mayor;
            }
        }

        private void Intercambiar(int i, int j)
        {
            Libro temporal = datos[i];
            datos[i] = datos[j];
            datos[j] = temporal;
        }

        public void Recorrer()
        {
            for (int i = 0; i < cantidad; i++)
            {
                Console.WriteLine($"{datos[i].Codigo} - {datos[i].Titulo} (Prestamos: {datos[i].VecesPrestado})");
            }
        }

        public void Imprimir()
        {
            Console.WriteLine("=== Max Heap (libros mas prestados) ===");
            Recorrer();
        }

        public void MostrarTopPrestados(int n)
        {
            MaxHeap temporal = new MaxHeap();
            for (int i = 0; i < cantidad; i++)
            {
                temporal.Insertar(datos[i]);
            }

            Console.WriteLine($"=== Top {n} libros mas prestados ===");
            int limite = n < temporal.cantidad ? n : temporal.cantidad;

            for (int i = 0; i < limite; i++)
            {
                Libro libro = temporal.Eliminar();
                Console.WriteLine($"{i + 1}. {libro.Codigo} - {libro.Titulo} (Prestamos: {libro.VecesPrestado})");
            }
        }
    }
}