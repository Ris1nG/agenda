using Agenda.Exceptions;

namespace Agenda.Entities {
    class PessoaJuridica : Pessoa {
        public PessoaJuridica(string numero) {
            SetNumero(numero);
        }
        public override void SetNumero(string numero) {
            if (Valida(numero) == true) {
                Numero = numero;
            }
        }
        private bool Valida(string numero) {

            if (numero.Length != 14) {
                throw new AgendaException("O CNPJ digitado nao eh valido!");
            }
            else {
                return true;
            }
        }
    }
}
