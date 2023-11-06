namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceUsuario = "Usuario";

        #region Main Methods

        public Task<long> CreateUsuario(object command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceUsuario}/CreateUsuario"));
            return Post(requestUrl, command);
        }
        public Task<long> UpdateUsuario(object command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceUsuario}/UpdateUsuario"));
            return Post(requestUrl, command);
        }

        public Task<long> DeleteUsuario(object command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceUsuario}/DeleteUsuario"));
            return Post(requestUrl, command);
        }

        //public GridUsuario GetGridUsuario()
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceUsuario}/GetGridUsuario"));
        //    return Get<GridUsuario>(requestUrl);
        //}

        //public List<UsuarioDto> GetUsuarioAll()
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceUsuario}/GetUsuarioAll/"));
        //    return Get<List<UsuarioDto>>(requestUrl);
        //}

        #endregion

        #region Methods

        //public UsuarioDto GetUsuarioById(string id)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceUsuario}/GetUsuarioById/{id}"));
        //    return Get<UsuarioDto>(requestUrl);
        //}
        //public WebProfileUser GetUsuarioByName(string name)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceUsuario}/GetUsuarioByName/{name}"));
        //    return Get<WebProfileUser>(requestUrl);
        //}

        public Task<bool> GetUsuarioByCpf(string cpf)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceUsuario}/GetUsuarioByCpf"));
            return PostAsync<bool, string>(requestUrl, cpf);
        }
        public Task<bool> GetUsuarioByEmail(string email)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceUsuario}/GetUsuarioByEmail"));
            return PostAsync<bool, string>(requestUrl, email);
        }

        #endregion

    }
}