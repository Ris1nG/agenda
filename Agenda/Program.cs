using System;
using System.Collections.Generic;
using Agenda.Entities;

namespace Agenda {
    class Program {
        static void Main(string[] args) {
            int op = 6;
            Dictionary<string, Registro> contatos = new Dictionary<string, Registro>();

            while(op != 0) {
                try {
                    Menu.menuPrincipal();
                    op = int.Parse(Console.ReadLine());
                    switch (op) {
                        case 1:
                            Menu.menuAdcContato(contatos);
                            break;
                        case 2:
                            Menu.menuAdcInfoAdicional(contatos);
                            break;
                        case 3:
                            Menu.menuRemoveContato(contatos);
                            break;
                        case 4:
                            Menu.menuMdfcContato(contatos);
                            break;
                        case 5:
                            Menu.menuImprimeContatos(contatos);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e) {
                    Console.WriteLine("Algum erro inesperado aconteceu: " + e.Message);
                }
            }
        }
    }
}
