using Agenda.Exceptions;

namespace Agenda.Entities {

    class PessoaFisica : Pessoa {
        public PessoaFisica(string numero) {
            SetNumero(numero);
        }
        public override void SetNumero(string numero) {
            if (Valida(numero) == true) {
                Numero = numero;
            }
        }

        private bool Valida(string numero) {

            if (numero.Length != 9) {
                throw new AgendaException("O CPF digitado nao eh valido!");
            }
            else {
                return true;
            }
        }
    }
}
