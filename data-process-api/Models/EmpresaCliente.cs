using System.Security.Cryptography.X509Certificates;

namespace data_process_api.Models {


    

    public class EmpresaCliente {

        public EmpresaCliente()
        {
            
        }

        public EmpresaCliente(string cnpj, string endereco, string telefone, string email)
        {

            try
            {
                if (!string.IsNullOrEmpty(telefone) && !string.IsNullOrEmpty(endereco) && !string.IsNullOrEmpty(cnpj))
                {
                    this.Telefone == telefone;
                    this.Email = email;
                    this.Cnpj = cnpj;
                    this.Endereco = endereco;

                }
              
            }
            catch (Exception ex) {

                return ex.Message;
            }
        }
        
        public EmpresaCliente(string cnpj, string endereco, string telefone)
        {
            try { 
                if (!string.IsNullOrEmpty(telefone) && !string.IsNullOrEmpty(endereco) && !string.IsNullOrEmpty(cnpj)) { 
                    this.Cnpj = cnpj;
                    this.Endereco = endereco;
                    this.Telefone = telefone;
                }
            }
            catch(Exception ex)
            {
                return $"Dados invalido: {ex}";
            }
        }

        
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