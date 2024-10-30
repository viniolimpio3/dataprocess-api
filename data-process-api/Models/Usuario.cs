namespace data_process_api.Models {
    public class Usuario {
        public Usuario() { }
        public Usuario(int Id, string nome, string email, string password) {
            this.Id = Id;
            this.Nome = nome;
            this.Email = email;
            this.Password = password;
        }
        public Usuario(string nome, string email, string password) {
            this.Nome = nome;
            this.Email = email;
            this.Password = password;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


    }
}
