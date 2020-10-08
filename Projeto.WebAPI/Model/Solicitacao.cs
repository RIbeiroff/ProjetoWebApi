namespace Projeto.WebAPI.Model
{
    public class Solicitacao 
    {
        public int Id { get; set; }
        public int Produto_Id { get; set; }
        public int Qtde_Comprada { get; set; }
        public Cartao Cartao { get; set; }
        
    }
}