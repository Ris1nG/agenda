using Agenda.Exceptions;

namespace Agenda.Entities {
    class Telefone {
        private string _numero;
        public string Numero {
            get { return _numero; }
            set {
                if (Valida(value) == true) {
                    _numero = value;
                }
            }
        }
        public Telefone(string numero) {
            Numero = numero;
        }
        /*
         *
         */
        private bool Valida(string numero) {

            if (numero.Length != 11) {
                throw new AgendaException("O Numero de Telefone digitado nao eh valido!");
            }
            else {
                return true;
            }
        }
        /*
         * Override no ToString() somente para imprimir de forma mais organizada as informacoes
         */
        public override string ToString() {
            string DDD = _numero.Substring(0, 2);
            string numeroCompleto = _numero.Substring(2);
            return "(" + DDD + ")" + numeroCompleto;
        }
    }
}
