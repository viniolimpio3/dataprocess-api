using System.Security.Cryptography.X509Certificates;

namespace data_process_api.Models {




    public class EmpresaCliente {


        //public EmpresaCliente(string cnpj, string endereco, string telefone, string email) {
        //    if (!string.IsNullOrEmpty(telefone) && !string.IsNullOrEmpty(endereco) && !string.IsNullOrEmpty(cnpj)) {
        //        this.Telefones = telefone;
        //        this.Email = email;
        //        this.Cnpj = cnpj;
        //        this.Endereco = endereco;
        //    } else {
        //        throw new ArgumentException("Campos obrigatórios não preenchidos!");
        //    }
        //}

        //public EmpresaCliente(string cnpj, string endereco, string telefone) {
        //    if (!string.IsNullOrEmpty(telefone) && !string.IsNullOrEmpty(endereco) && !string.IsNullOrEmpty(cnpj)) {
        //        this.Cnpj = cnpj;
        //        this.Endereco = endereco;
        //        this.Telefones = telefone;
        //    } else {
        //        throw new ArgumentException("Campos obrigatórios não preenchidos!");
        //    }
        //}

        public int Id { get; set; }
        public string Telefones { get; set; }
        public string Nome { get; set; }
        public string? Email { get; set; }
        public string? Cnpj { get; set; }
        public string Endereco { get; set; }
        
    }
}