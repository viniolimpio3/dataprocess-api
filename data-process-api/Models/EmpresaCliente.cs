using Microsoft.Extensions.WebEncoders.Testing;

namespace data_process_api.Models {

    public class EmpresaCliente {

        public int Id { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Cnpj { get; set; }
        public string Endereco { get; set; }

        public  EmpresaCliente(string cnpj, string endereco, string telefone, string email)
        {
            this.Cnpj = cnpj;
            this.Telefone = telefone;
            this.Endereco = endereco;
            this.Email = email;                   

        }    
        

        
    }
}