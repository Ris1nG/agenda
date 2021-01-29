namespace Agenda.Entities {
    class Endereco {
        public int Id { get; set; }
        
        // Variavel para garantir que cada endereco tenha um ID diferente.
        private static int _controleIds = 0;
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }


        public Endereco(string logradouro, int numero, string complemento, string bairro, string cidade, string estado, string cep) {
            Id = _controleIds++;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
        }
        public override string ToString() {
            return "ID: " + Id
                + "\nLogradouro: "
                + Logradouro
                + "\nNumero: "
                + Numero
                + "\nComplemento: "
                + Complemento
                + "\nBairro: "
                + Bairro
                + "\nCidade: "
                + Cidade
                + "\nEstado: "
                + Estado
                + "\nCEP: "
                + Cep;
        }
    }
}
