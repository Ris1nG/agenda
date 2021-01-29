using System.Text;
using System.Collections.Generic;


namespace Agenda.Entities {
    class Registro {

        private List<Telefone> _telefones = new List<Telefone>();
        private List<Endereco> _enderecos = new List<Endereco>();

        public Pessoa Pessoa { get; set; }
        public string Nome { get; set; }
        public string GetNumeroDeIdentificacao() {
            return Pessoa.GetNumero();
        }
        public void AdicionaTelefone(string telefone) {
            Telefone tel = new Telefone(telefone);
            _telefones.Add(tel);
        }
        public void RemoveTelefone(string numASerRemovido) {
            Telefone telASerRemovido = _telefones.Find(x => x.Numero == numASerRemovido);
            if (telASerRemovido != null) {
                _telefones.Remove(telASerRemovido);
            }
        }
        public string[] obtemTelefones() {
            string[] telObtem = new string[_telefones.Count];
            int i = 0;
            foreach(Telefone tel in _telefones) {
                telObtem[i++] = tel.Numero;
            }
            return telObtem;
        }
        public void AdicionaEndereco(string logradouro, int numero, string complemento, string bairro, string cidade, string estado, string cep) {
            Endereco endereco = new Endereco(logradouro, numero, complemento, bairro, cidade, estado, cep);
            _enderecos.Add(endereco);
        }
        public void RemoveEndereco(int id) {
            Endereco endASerRemovido = _enderecos.Find(x => x.Id == id);
            if (endASerRemovido != null) {
                _enderecos.Remove(endASerRemovido);
            }
        }

        public EnderecoContainer[] obtemEnderecos() {
            EnderecoContainer[] endContainers = new EnderecoContainer[_enderecos.Count];
            int i = 0;

            foreach (Endereco end in _enderecos) {
                EnderecoContainer temp = new EnderecoContainer {
                    Numero = end.Numero,
                    Bairro = end.Bairro,
                    Cep = end.Cep,
                    Cidade = end.Cidade,
                    Complemento = end.Complemento,
                    Estado = end.Estado,
                    Id = end.Id,
                    Longradouro = end.Logradouro
                };

                endContainers[i++] = temp;
            }

            return endContainers;
        }
        /*
         * Override no ToString() somente para imprimir de forma mais organizada as informacoes
         */
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Nome do contato: " + Nome);
            sb.AppendLine("CPF/CNPJ: " + Pessoa.GetNumero());
            sb.AppendLine();
            sb.AppendLine("TELEFONE(S):");
            foreach (Telefone tel in _telefones) {
                sb.AppendLine(tel.ToString());
            }
            sb.AppendLine();
            sb.AppendLine("ENDERECO(S):");
            foreach (Endereco end in _enderecos) {
                sb.AppendLine(end.ToString());
                sb.AppendLine();
            }
            sb.AppendLine("--------------------");
            return sb.ToString();
        }
        /*
         * CompareTo para imprimir os contatos de uma forma ordenada para que pareca uma Agenda
         */
        internal int CompareTo(Registro value) {
            return Nome.CompareTo(value.Nome);
        }
    }
}
