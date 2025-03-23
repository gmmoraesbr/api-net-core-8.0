namespace Base.Api.Common
{
    public class ApiError
    {
        /// <summary>
        /// Um identificador legível por máquina para o tipo de erro.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Uma mensagem curta e legível para humanos descrevendo o erro.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Uma explicação mais detalhada sobre este erro específico.
        /// </summary>
        public string Detail { get; set; }

        public ApiError(string type, string error, string detail)
        {
            Type = type;
            Error = error;
            Detail = detail;
        }
    }
}
