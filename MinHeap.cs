namespace Proyecto1ED2
{
    public class MinHeap
    {
        private const int CAPACIDAD_INICIAL = 50;

        private Libro[] datos;
        private int cantidad;

        public MinHeap()
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

                if (datos[indice].CopiasDisponibles < datos[padre].CopiasDisponibles)
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


        public Libro ObtenerMinimo()
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

            Libro minimo = datos[0];

            cantidad--;
            datos[0] = datos[cantidad];
            datos[cantidad] = null;

            HundirHaciaAbajo(0);

            return minimo;
        }
        private void HundirHaciaAbajo(int indice)
        {
            while (true)
            {
                int izquierdo = indice * 2 + 1;
                int derecho = indice * 2 + 2;
                int menor = indice;

                if (izquierdo < cantidad && datos[izquierdo].CopiasDisponibles < datos[menor].CopiasDisponibles)
                {
                    menor = izquierdo;
                }

                if (derecho < cantidad && datos[derecho].CopiasDisponibles < datos[menor].CopiasDisponibles)
                {
                    menor = derecho;
                }

                if (menor == indice)
                {
                    break;
                }

                Intercambiar(indice, menor);
                indice = menor;
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
                Console.WriteLine($"{datos[i].Codigo} - {datos[i].Titulo} (Copias: {datos[i].CopiasDisponibles})");
            }
        }

        public void Imprimir()
        {
            Console.WriteLine("=== Min Heap (prioridad de reabastecimiento) ===");
            Recorrer();
        }
    }

}