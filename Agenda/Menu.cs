using System;
using System.Linq;
using System.Collections.Generic;
using Agenda.Exceptions;
using Agenda.Entities;

namespace Agenda {
    class Menu {
        public static void menuPrincipal() {
            Console.Clear();
            Console.WriteLine("Seja Bem-Vindo a sua Agenda de Contatos!");
            Console.WriteLine();
            Console.WriteLine("1. Adicionar um contato");
            Console.WriteLine("2. Adicionar informacoes a um contato");
            Console.WriteLine("3. Remover um informacoes de um contato");
            Console.WriteLine("4. Modificar um contato");
            Console.WriteLine("5. Imprimir todos os contatos");
            Console.WriteLine("Se deseja sair do programa digite 0");
        }
        /*
         * Menu adiciona o contato inserindo todas as informacoes necessarias para um contato! 
         */
        public static void menuAdcContato(Dictionary<string, Registro> contatos) {
            try {

                Registro contato = new Registro();
                Console.Clear();
                Console.WriteLine("ADICIONANDO UM CONTATO");
                Console.Write("Digite o nome do contato: ");

                string nome = Console.ReadLine();
                contato.Nome = nome;

                Console.Write("Digite qual eh o tipo de pessoa, se eh Juridica ou Fisica (J ou F): ");
                char opcao = char.Parse(Console.ReadLine());
                if (opcao == 'f' || opcao == 'F') {
                    while (true) {
                        try {
                            Console.Write("Digite o CPF maximo 9 caracteres: ");
                            string numeroDeIdentificacao = Console.ReadLine();
                            contato.Pessoa = new PessoaFisica(numeroDeIdentificacao);
                            break;
                        }
                        catch (AgendaException e) {
                            Console.WriteLine(e.Message);
                            Console.ReadLine();
                        }
                    }
                }
                else if (opcao == 'j' || opcao == 'J') {
                    while (true) {
                        try {
                            Console.Write("Digite o CNPJ maximo 14 caracteres: ");
                            string numeroDeIdentificacao = Console.ReadLine();
                            contato.Pessoa = new PessoaJuridica(numeroDeIdentificacao);
                            break;
                        }
                        catch (AgendaException e) {
                            Console.WriteLine(e.Message);
                            Console.ReadLine();
                        }
                    }
                }
                subMenuAdcTelefones(contato);
                subMenuAdcEndereco(contato);
                contatos.Add(contato.Pessoa.GetNumero(), contato);
            }

            catch (AgendaException e) {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
            catch (FormatException e) {
                Console.WriteLine("Erro de digitacao: " + e.Message);
                Console.ReadLine();
            }
            catch (OverflowException e) {
                Console.WriteLine(e.Message);
            }
            catch (Exception e) {
                Console.WriteLine("Algum erro inesperado aconteceu: " + e.Message);
                Console.ReadLine();
            }
        }
        /*
         * Menu para adicionar alguma informacao adicional a algum contato.
         */
        public static void menuAdcInfoAdicional(Dictionary<string, Registro> contatos) {
            if (contatos.Count == 0) {
                Console.WriteLine("Lista de contatos esta VAZIA!");
                Console.ReadLine();
                return;
            }

            while (true) {
                try {
                    Console.Clear();
                    Console.WriteLine("1. Deseja adicionar algum telefone a mais?");
                    Console.WriteLine("2. Deseja adicionar algum endereco a mais?");
                    Console.WriteLine("Se deseja sair do programa digite 0");

                    int op = int.Parse(Console.ReadLine());

                    switch (op) {
                        case 1:
                            menuTelefoneAdicional(contatos);
                            break;
                        case 2:
                            menuEnderecoAdicional(contatos);
                            break;
                        default:
                            break;
                    }
                    break;
                }
                catch (FormatException e) {
                    Console.WriteLine("Digitou a opcao errada!" + e.Message);
                    Console.ReadLine();
                }
                catch (Exception e) {
                    Console.WriteLine("Algum erro inesperado aconteceu: " + e.Message);
                    Console.ReadLine();
                }
            }
        }
        /*
         * Menu para o usuario realizar a adicao de mais Telefones caso queria
         */
        public static void menuTelefoneAdicional(Dictionary<string, Registro> contatos) {

            imprimeContatos(contatos);
            Console.WriteLine();

            Console.Write("Digite o CPF ou CNPJ da pessoa que deseja adicionar um telefone: ");
            string numDeIdentificacao = Console.ReadLine();

            foreach (KeyValuePair<string, Registro> contato in contatos) {
                if (contato.Key.Equals(numDeIdentificacao)) {
                    subMenuAdcTelefones(contato.Value);
                }
            }
        }
        /*
         * Menu para o usuario realizar a adicao de mais enderecos caso queria
         */
        public static void menuEnderecoAdicional(Dictionary<string, Registro> contatos) {

            imprimeContatos(contatos);
            Console.WriteLine();

            Console.Write("Digite o CPF ou CNPJ da pessoa que deseja adicionar um telefone: ");
            string numDeIdentificacao = Console.ReadLine();

            foreach (KeyValuePair<string, Registro> contato in contatos) {
                if (contato.Key.Equals(numDeIdentificacao)) {
                    subMenuAdcEndereco(contato.Value);
                }
            }
        }
        /*
         * Menu para realizar a remocao de um contato, de um telefone ou de um endereco de algum contato.
         */
        public static void menuRemoveContato(Dictionary<string, Registro> contatos) {
            if (contatos.Count == 0) {
                Console.WriteLine("Lista de contatos esta VAZIA!");
                Console.ReadLine();
                return;
            }

            while (true) {
                try {
                    Console.Clear();
                    Console.WriteLine("1. Deseja remover um contato por completo?");
                    Console.WriteLine("2. Deseja remover um telefone de um contato?");
                    Console.WriteLine("3. Deseja remover um endereco de um contato?");
                    Console.WriteLine("Se deseja sair do programa digite 0");

                    int op = int.Parse(Console.ReadLine());

                    switch (op) {
                        case 1:
                            removeContatoCompleto(contatos);
                            break;
                        case 2:
                            removeContatoTelefone(contatos);
                            break;
                        case 3:
                            removeContatoEndereco(contatos);
                            break;
                        default:
                            break;
                    }
                    break;
                }
                catch (FormatException e) {
                    Console.WriteLine("Digitou a opcao errada!" + e.Message);
                    Console.ReadLine();
                }
                catch (Exception e) {
                    Console.WriteLine("Algum erro inesperado aconteceu: " + e.Message);
                    Console.ReadLine();
                }
            }
        }
        /*
         * removeContatoCompleto() remove o contato por completo usando o CPF ou CNPJ
         * se nao existir o contato ele volta para o menu de menuRemoveContato()
         */
        public static void removeContatoCompleto(Dictionary<string, Registro> contatos) {

            imprimeContatos(contatos);
            Console.WriteLine();

            Console.Write("Digite o CPF ou CNPJ da pessoa que deseja remover: ");
            string numeroDeIdentificacao = Console.ReadLine();
            try {
                if(contatos.Remove(numeroDeIdentificacao)) {
                    Console.WriteLine("Contato Removido com sucesso!");
                    Console.ReadLine();
                }
                else {
                    Console.WriteLine("NAO FOI POSSIVEL REMOVER!");
                    Console.ReadLine();
                }
            }
            catch (ArgumentNullException e) {
                Console.WriteLine("Contato nao existente!" + e.Message);
                Console.ReadLine();
            }
        }
        /*
        * Remove somente o telefone do contato, ele passa por todos os contatos procurando o telefone q o usuario digitou.
        * se tivermos numeros iguais ele remove os 2 que tiverem.
        */
        public static void removeContatoTelefone(Dictionary<string, Registro> contatos) {

            imprimeContatos(contatos);

            Console.Write("Digite o Telefone que deseja remover (somente numeros sem caracteres): ");
            string telefone = Console.ReadLine();

            foreach (Registro contato in contatos.Values) {
                contato.RemoveTelefone(telefone);
            }
        }
        /*
         * Remove o endereco do contato que ele digitou o ID, como todo o id de endereco eh diferente 
         * entao ele procura por todos os contatos o ID q ele quer remover
         */
        public static void removeContatoEndereco(Dictionary<string, Registro> contatos) {

            imprimeContatos(contatos);

            try {
                Console.Write("Digite o Id do endereco que deseja remover: ");
                int id = int.Parse(Console.ReadLine());
                foreach (Registro contato in contatos.Values) {
                    contato.RemoveEndereco(id);
                }
            }
            catch (FormatException e) {
                Console.WriteLine("Digitou errado!" + e.Message);
            }
        }
        /*
         * Menu para modificar o endereco ou o telefone de algum contato.
         */
        public static void menuMdfcContato(Dictionary<string, Registro> contatos) {
            if (contatos.Count == 0) {
                Console.WriteLine("Lista de contatos esta VAZIA!");
                Console.ReadLine();
                return;
            }

            while (true) {
                try {
                    Console.Clear();
                    Console.WriteLine("1. Deseja modificar algum telefone?");
                    Console.WriteLine("2. Deseja modificar algum endereco?");
                    Console.WriteLine("Se deseja sair do programa digite 0");

                    int op = int.Parse(Console.ReadLine());

                    switch (op) {
                        case 1:
                            menuMdfcTelefone(contatos);
                            break;
                        case 2:
                            menuMdfcEndereco(contatos);
                            break;
                        default:
                            break;
                    }
                    break;
                }
                catch (FormatException e) {
                    Console.WriteLine("Digitou a opcao errada!" + e.Message);
                    Console.ReadLine();
                }
                catch (Exception e) {
                    Console.WriteLine("Algum erro inesperado aconteceu: " + e.Message);
                    Console.ReadLine();
                }
            }
        }
        /*
         * Modifica o telefone, mas como o registro tem so as de remover e adicionar telefone
         * ele primeiramente remove o telefone que o Usuario escolheu e depois substitui pelo telefone que ele quer
         */
        public static void menuMdfcTelefone (Dictionary<string, Registro> contatos) {
            imprimeContatos(contatos);
            try {

                Console.Write("Digite o CPF ou CNPJ do contato que deseja modificar: ");
                string numDeID = Console.ReadLine();
                Console.WriteLine();
                contatos.TryGetValue(numDeID, out Registro aux);

                Console.Write("Digite telefone que quer modificar (somente numeros sem caracteres): ");
                string telefoneAlterar = Console.ReadLine();

                aux.RemoveTelefone(telefoneAlterar);
                subMenuAdcTelefones(aux);
            }
            catch (ArgumentException e) {
                Console.WriteLine(e.Message);
            }
        }
        /*
         * Modifica o telefone, mas como o registro tem so as de remover e adicionar telefone
         * ele primeiramente remove o telefone que o Usuario escolheu e depois substitui pelo telefone que ele quer
         */
        public static void menuMdfcEndereco(Dictionary<string, Registro> contatos) {
            imprimeContatos(contatos);
            try {

                Console.Write("Digite o CPF ou CNPJ do contato que deseja modificar: ");
                string numDeID = Console.ReadLine();
                Console.WriteLine();
                contatos.TryGetValue(numDeID, out Registro aux);

                Console.Write("Digite o ID endereco que quer modificar: ");
                int endAlterar = int.Parse(Console.ReadLine());

                aux.RemoveEndereco(endAlterar);
                subMenuAdcEndereco(aux);
            }
            catch (FormatException e) {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e) {
                Console.WriteLine(e.Message);
            }
        }
        /*
         * Quando o usuario aperta o 4 no menu principal, vem para esse menu que somente imprime os contatos e espera
         * o Usuario aperta qualquer comando para prosseguir
         */
        public static void menuImprimeContatos(Dictionary<string, Registro> contatos) {
            if (contatos.Count != 0) {
                imprimeContatos(contatos);
                Console.WriteLine("Aperte ENTER para continuar: ");
                Console.ReadLine();
            }
        }
        /*
         * Imprime todos os contatos que o Usuario inseriu no programa.
         */
        public static void imprimeContatos(Dictionary<string, Registro> contatos) {
            if (contatos.Count == 0) {
                Console.WriteLine("Lista de contatos esta VAZIA!");
                Console.ReadLine();
                return;
            }

            Console.Clear();

            var contatosOrdenado = contatos.ToList();

            contatosOrdenado.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            foreach (KeyValuePair<string, Registro> contato in contatosOrdenado) {
                Console.WriteLine(contato.Value);
            }
        }
        /*
         * o subMenuAdcTelefones serve para adicionar um telefone, ele eh chamado na funcao menuAdcContato
         * utiliza de recursividade para adicionar varios telefones conforme o usuario quer.
         */
        public static void subMenuAdcTelefones(Registro contato) {
            while (true) {
                try {
                    Console.Write("Digite o telefone (Somente numeros com DDD total de 11 digitos por favor): ");
                    string telefone = Console.ReadLine();

                    contato.AdicionaTelefone(telefone);

                    Console.WriteLine("Deseja adicionar mais algum telefone (S/N): ");
                    char opcao = char.Parse(Console.ReadLine());

                    if (opcao == 'S' || opcao == 's') {
                        subMenuAdcTelefones(contato);
                        break;
                    }
                    else if (opcao == 'N' || opcao == 'n') {
                        break;
                    }
                }
                catch (AgendaException e) {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
                catch (FormatException e) {
                    Console.WriteLine("Erro na digitacao: " + e.Message);
                    Console.ReadLine();
                }
            }
        }
        /*
        * o subMenuAdcEndereco serve para adicionar um telefone, ele eh chamado na funcao menuAdcContato.
        *  utiliza de recursividade para adicionar varios endereco conforme o usuario quer.
        */
        public static void subMenuAdcEndereco(Registro contato) {
            while (true) {
                try {
                    Console.Clear();
                    Console.WriteLine("ENDERECO:");
                    Console.Write("Digite o logradouro: ");
                    string logradouro = Console.ReadLine();

                    Console.Write("Digite o Numero: ");
                    int numero = int.Parse(Console.ReadLine());

                    Console.Write("Digite o Complemento: ");
                    string complemento = Console.ReadLine();

                    Console.Write("Digite o Bairro: ");
                    string bairro = Console.ReadLine();

                    Console.Write("Digite a Cidade: ");
                    string cidade = Console.ReadLine();

                    Console.Write("Digite o Estado: ");
                    string estado = Console.ReadLine();

                    Console.Write("Digite o CEP: ");
                    string cep = Console.ReadLine();

                    contato.AdicionaEndereco(logradouro, numero, complemento, bairro, cidade, estado, cep);

                    Console.WriteLine("Deseja adicionar mais algum endereco (S/N): ");
                    char opcao = char.Parse(Console.ReadLine());

                    if (opcao == 'S' || opcao == 's') {
                        subMenuAdcEndereco(contato);
                        break;
                    }
                    else if (opcao == 'N' || opcao == 'n') {
                        break;
                    }
                }
                catch (AgendaException e) {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
                catch (FormatException e) {
                    Console.WriteLine("Erro na digitacao: " + e.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}