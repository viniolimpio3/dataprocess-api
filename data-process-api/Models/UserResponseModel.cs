namespace data_process_api.Models {
    public class UserResponseModel {
        public UserResponseModel(int id, string nome, string email) {
            Id = id;
            Nome = nome;
            Email = email;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
