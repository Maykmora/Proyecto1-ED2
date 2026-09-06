using System;
public class Libro
{
    public string Codigo { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Categoria { get; set; }
    public int CopiasDisponibles { get; set; }
    public int VecesPrestado { get; set; }

    public Libro(string codigo, string titulo, string autor, string categoria, int copiasDisponibles)
    {
        Codigo = codigo;
        Titulo = titulo;
        Autor = autor;
        Categoria = categoria;
        CopiasDisponibles = copiasDisponibles;
        VecesPrestado = 0;
    }
}
