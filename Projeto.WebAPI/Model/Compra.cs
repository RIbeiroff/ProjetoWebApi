using System;

namespace Projeto.WebAPI.Model
{
    public class Compra
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public DateTime Data_Transacao { get; set; }
        public Solicitacao Solicitacao { get; set; }

    }
}