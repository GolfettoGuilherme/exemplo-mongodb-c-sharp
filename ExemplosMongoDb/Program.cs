using System;
using MongoDB.Bson; //Manipulação de documentos Jsom
using MongoDB.Driver; //Acessa e coneta com o servidor
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ExemplosMongoDb
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await ExcluirDocumentos();

            Console.ReadKey();
        }

        static async Task AdicionarLivro()
        {
            var Livros = new List<Livro>();
            Livros.Add(valoresLivro.incluiValoresLivro("A Dança com os Dragões", "George R R Martin", 2011, 934, "Fantasia, Ação"));
            Livros.Add(valoresLivro.incluiValoresLivro("A Tormenta das Espadas", "George R R Martin", 2006, 1276, "Fantasia, Ação"));
            Livros.Add(valoresLivro.incluiValoresLivro("Memórias Póstumas de Brás Cubas", "Machado de Assis", 1915, 267, "Literatura Brasileira"));
            Livros.Add(valoresLivro.incluiValoresLivro("Star Trek Portal do Tempo", "Crispin A C", 2002, 321, "Fantasia, Ação"));
            Livros.Add(valoresLivro.incluiValoresLivro("Star Trek Enigmas", "Dedopolus Tim", 2006, 195, "Ficção Científica, Ação"));
            Livros.Add(valoresLivro.incluiValoresLivro("Emília no Pais da Gramática", "Monteiro Lobato", 1936, 230, "Infantil, Literatura Brasileira, Didático"));
            Livros.Add(valoresLivro.incluiValoresLivro("Chapelzinho Amarelo", "Chico Buarque", 2008, 123, "Infantil, Literatura Brasileira"));
            Livros.Add(valoresLivro.incluiValoresLivro("20000 Léguas Submarinas", "Julio Verne", 1894, 256, "Ficção Científica, Ação"));
            Livros.Add(valoresLivro.incluiValoresLivro("Primeiros Passos na Matemática", "Mantin Ibanez", 2014, 190, "Didático, Infantil"));
            Livros.Add(valoresLivro.incluiValoresLivro("Saúde e Sabor", "Yeomans Matthew", 2012, 245, "Culinária, Didático"));
            Livros.Add(valoresLivro.incluiValoresLivro("Goldfinger", "Iam Fleming", 1956, 267, "Espionagem, Ação"));
            Livros.Add(valoresLivro.incluiValoresLivro("Da Rússia com Amor", "Iam Fleming", 1966, 245, "Espionagem, Ação"));
            Livros.Add(valoresLivro.incluiValoresLivro("O Senhor dos Aneis", "J R R Token", 1948, 1956, "Fantasia, Ação"));


            var conexao = new ConexaoMongo();

            await conexao.Livros.InsertManyAsync(Livros);

            Console.WriteLine("Documento incluido");
        }

        static async Task ListandoLivros()
        {
            var conexao = new ConexaoMongo();
            Console.WriteLine("Listando");

            var lista = await conexao.Livros.Find(new BsonDocument()).ToListAsync();

            foreach(var doc in lista)
            {
                Console.WriteLine(doc.ToJson<Livro>());
            }

            Console.WriteLine("Fim da lista");
        }

        static async Task ListandoLivrosComFiltro()
        {
            var conexao = new ConexaoMongo();
            Console.WriteLine("Listando");

            var construtor = Builders<Livro>.Filter;
            //eq = equals
            //gte = greater or equal
            //anyEq = buscar em array
            var condicao = construtor.AnyEq(x => x.Assunto, "Ficção Científica");

            var lista = await conexao.Livros.Find(condicao).SortBy(x => x.Titulo).Limit(1).ToListAsync();

            foreach (var doc in lista)
            {
                Console.WriteLine(doc.ToJson<Livro>());
            }

            Console.WriteLine("Fim da lista");
        }

        static async Task AlterandoDocumento()
        {
            var conexao = new ConexaoMongo();
            var construtor = Builders<Livro>.Filter;
            var condicao = construtor.Eq(x => x.Titulo, "Guerra dos Tronos");

            var lista = await conexao.Livros.Find(condicao).ToListAsync();

            foreach (var doc in lista)
            {
                Console.WriteLine(doc.ToJson<Livro>());
                doc.Ano = 2000;
                doc.Paginas = 900;
                await conexao.Livros.ReplaceOneAsync(condicao, doc);
            }

            Console.WriteLine("Fim da lista");
        }

        static async Task AlterandoDocumentoClasse()
        {
            var conexao = new ConexaoMongo();
            var construtor = Builders<Livro>.Filter;
            var condicao = construtor.Eq(x => x.Autor, "Machado de Assis"); // busca

            var lista = await conexao.Livros.Find(condicao).ToListAsync();

            var construtorAlteracao = Builders<Livro>.Update; //permite fazer alteacoes em grupo até
            var condicaoalteracao = construtorAlteracao.Set(x => x.Autor, "M. Assists"); //alteracao
            await conexao.Livros.UpdateManyAsync(condicao, condicaoalteracao);

            Console.WriteLine("Alterado");
        }

        static async Task ExcluirDocumentos()
        {
            var conexao = new ConexaoMongo();

            var construtor = Builders<Livro>.Filter;
            var condicao = construtor.Eq(x => x.Autor, "M. Assists"); // busca

            await conexao.Livros.DeleteManyAsync(condicao);

            Console.WriteLine("Excluidos");
        }
    }
}
