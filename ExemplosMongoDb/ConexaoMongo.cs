using System;
using MongoDB.Driver;

namespace ExemplosMongoDb
{
    public class ConexaoMongo
    {
        public const string STRING_DE_CONEXAO = "mongodb://localhost:27017";
        public const string NOME_DA_BASE = "Biblioteca";
        public const string NOME_DA_COLECAO = "Livros";

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _BaseDados;

        public ConexaoMongo()
        {
            _client = new MongoClient(STRING_DE_CONEXAO);
            _BaseDados = _client.GetDatabase(NOME_DA_BASE);
        }

        public IMongoClient Cliente
        {
            get { return _client; }
        }

        public IMongoCollection<Livro> Livros
        {
            get { return _BaseDados.GetCollection<Livro>(NOME_DA_COLECAO); }
        }
    }
}
