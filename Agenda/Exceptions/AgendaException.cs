using System;

namespace Agenda.Exceptions {
    /*
     * Classe AgendaException ela eh usada para qualquer excecao que eu queira lancar
     * durante a execucao da minha agenda 
     */
    class AgendaException : ApplicationException {
        public AgendaException(string message) : base(message) {
        }
    }
}
