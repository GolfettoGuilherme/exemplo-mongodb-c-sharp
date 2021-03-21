using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExemplosMongoDb
{
    public class Livro
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Ano { get; set; }
        public int Paginas { get; set; }
        public List<string> Assunto { get; set; }
    }

    public class valoresLivro
    {

        public static Livro incluiValoresLivro(string Titulo, string Autor, int Ano, int Paginas, string assuntos)
        {
            Livro Livro = new Livro();
            Livro.Titulo = Titulo;
            Livro.Autor = Autor;
            Livro.Ano = Ano;
            Livro.Paginas = Paginas;
            string[] vetAssunto = assuntos.Split(',');
            List<string> vetAssunto2 = new List<string>();
            for (int i = 0; i <= vetAssunto.Length - 1; i++)
            {
                vetAssunto2.Add(vetAssunto[i].Trim());
            }
            Livro.Assunto = vetAssunto2;
            return Livro;
        }
    }
}
