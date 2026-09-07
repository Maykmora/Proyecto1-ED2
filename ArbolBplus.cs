namespace Proyecto1ED2
{
    public class NodoBPlus
    {
        public const int ORDEN = 4;

        public string[] Claves;
        public Libro[] Libros;
        public NodoBPlus[] Hijos;
        public int NumClaves;
        public bool EsHoja;
        public NodoBPlus Siguiente;

        public NodoBPlus(bool esHoja)
        {
            EsHoja = esHoja;
            Claves = new string[ORDEN];
            NumClaves = 0;
            Siguiente = null;

            if (esHoja)
            {
                Libros = new Libro[ORDEN];
            }
            else
            {
                Hijos = new NodoBPlus[ORDEN + 1];
            }
        }
    }

    public class ArbolBPlus
    {
        private NodoBPlus raiz;

        public ArbolBPlus()
        {
            raiz = new NodoBPlus(true);
        }

        public Libro Buscar(string codigo)
        {
            NodoBPlus hoja = BuscarHoja(codigo);

            for (int i = 0; i < hoja.NumClaves; i++)
            {
                if (hoja.Claves[i] == codigo)
                {
                    return hoja.Libros[i];
                }
            }
            return null;
        }

        private NodoBPlus BuscarHoja(string codigo)
        {
            NodoBPlus actual = raiz;

            while (!actual.EsHoja)
            {
                int i = 0;
                while (i < actual.NumClaves && string.Compare(codigo, actual.Claves[i]) >= 0)
                {
                    i++;
                }
                actual = actual.Hijos[i];
            }
            return actual;
        }

        public void Insertar(Libro libro)
        {
            var resultado = InsertarEnNodo(raiz, libro);

            if (resultado.nuevoNodo != null)
            {
                NodoBPlus nuevaRaiz = new NodoBPlus(false);
                nuevaRaiz.Claves[0] = resultado.claveSubida;
                nuevaRaiz.Hijos[0] = raiz;
                nuevaRaiz.Hijos[1] = resultado.nuevoNodo;
                nuevaRaiz.NumClaves = 1;
                raiz = nuevaRaiz;
            }
        }

        private (NodoBPlus? nuevoNodo, string? claveSubida) InsertarEnNodo(NodoBPlus nodo, Libro libro)
        {
            if (nodo.EsHoja)
            {
                InsertarEnHoja(nodo, libro);

                if (nodo.NumClaves == NodoBPlus.ORDEN)
                {
                    return DividirHoja(nodo);
                }
                return (null, null);
            }
            else
            {
                int i = 0;
                while (i < nodo.NumClaves && string.Compare(libro.Codigo, nodo.Claves[i]) >= 0)
                {
                    i++;
                }

                var resultadoHijo = InsertarEnNodo(nodo.Hijos[i], libro);

                if (resultadoHijo.nuevoNodo != null)
                {
                    InsertarEnInterno(nodo, resultadoHijo.claveSubida!, resultadoHijo.nuevoNodo);

                    if (nodo.NumClaves == NodoBPlus.ORDEN)
                    {
                        return DividirInterno(nodo);
                    }
                }
                return (null, null);
            }
        }

        private void InsertarEnHoja(NodoBPlus hoja, Libro libro)
        {
            int i = hoja.NumClaves - 1;

            while (i >= 0 && string.Compare(hoja.Claves[i], libro.Codigo) > 0)
            {
                hoja.Claves[i + 1] = hoja.Claves[i];
                hoja.Libros[i + 1] = hoja.Libros[i];
                i--;
            }

            hoja.Claves[i + 1] = libro.Codigo;
            hoja.Libros[i + 1] = libro;
            hoja.NumClaves++;
        }

        private (NodoBPlus nuevoNodo, string claveSubida) DividirHoja(NodoBPlus hoja)
        {
            NodoBPlus nuevaHoja = new NodoBPlus(true);
            int mitad = hoja.NumClaves / 2;

            int j = 0;
            for (int i = mitad; i < hoja.NumClaves; i++)
            {
                nuevaHoja.Claves[j] = hoja.Claves[i];
                nuevaHoja.Libros[j] = hoja.Libros[i];
                j++;
            }
            nuevaHoja.NumClaves = j;
            hoja.NumClaves = mitad;

            nuevaHoja.Siguiente = hoja.Siguiente;
            hoja.Siguiente = nuevaHoja;

            return (nuevaHoja, nuevaHoja.Claves[0]);
        }

        private void InsertarEnInterno(NodoBPlus nodo, string claveSubida, NodoBPlus nuevoHijo)
        {
            int i = nodo.NumClaves - 1;

            while (i >= 0 && string.Compare(nodo.Claves[i], claveSubida) > 0)
            {
                nodo.Claves[i + 1] = nodo.Claves[i];
                nodo.Hijos[i + 2] = nodo.Hijos[i + 1];
                i--;
            }

            nodo.Claves[i + 1] = claveSubida;
            nodo.Hijos[i + 2] = nuevoHijo;
            nodo.NumClaves++;
        }

        private (NodoBPlus nuevoNodo, string claveSubida) DividirInterno(NodoBPlus nodo)
        {
            NodoBPlus nuevoNodo = new NodoBPlus(false);
            int mitad = nodo.NumClaves / 2;
            string claveSubida = nodo.Claves[mitad];

            int j = 0;
            for (int i = mitad + 1; i < nodo.NumClaves; i++)
            {
                nuevoNodo.Claves[j] = nodo.Claves[i];
                j++;
            }

            j = 0;
            for (int i = mitad + 1; i <= nodo.NumClaves; i++)
            {
                nuevoNodo.Hijos[j] = nodo.Hijos[i];
                j++;
            }

            nuevoNodo.NumClaves = nodo.NumClaves - mitad - 1;
            nodo.NumClaves = mitad;

            return (nuevoNodo, claveSubida);
        }

        public bool Eliminar(string codigo)
        {
            NodoBPlus hoja = BuscarHoja(codigo);

            int pos = -1;
            for (int i = 0; i < hoja.NumClaves; i++)
            {
                if (hoja.Claves[i] == codigo)
                {
                    pos = i;
                    break;
                }
            }

            if (pos == -1) return false;

            for (int i = pos; i < hoja.NumClaves - 1; i++)
            {
                hoja.Claves[i] = hoja.Claves[i + 1];
                hoja.Libros[i] = hoja.Libros[i + 1];
            }

            hoja.NumClaves--;
            return true;
        }

        public void Recorrer()
        {
            NodoBPlus actual = raiz;

            while (!actual.EsHoja)
            {
                actual = actual.Hijos[0];
            }

            while (actual != null)
            {
                for (int i = 0; i < actual.NumClaves; i++)
                {
                    Console.WriteLine($"{actual.Claves[i]} - {actual.Libros[i].Titulo}");
                }
                actual = actual.Siguiente;
            }
        }

        public void Imprimir()
        {
            Console.WriteLine("=== Catalogo (Arbol B+) ===");
            Recorrer();
        }
    }
}
