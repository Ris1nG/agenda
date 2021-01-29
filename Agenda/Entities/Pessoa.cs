namespace Agenda.Entities {
    abstract class Pessoa {
        protected string Numero;

        public string GetNumero() {
            return Numero;
        }

        public abstract void SetNumero(string numero);
        
    }
}
