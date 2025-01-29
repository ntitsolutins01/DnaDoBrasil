﻿using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    /// <summary>
    /// Aluno Client
    /// </summary>
    public partial class DnaApiClient
    {
        private const string ResourceAlunos = "Alunos";

        #region Main Methods
        /// <summary>
        /// Inclusão de Aluno
        /// </summary>
        /// <param name="command">Objeto para inclusão do Aluno</param>
        /// <returns>Id do Aluno inserido</returns>
        public Task<long> CreateDados(AlunoModel.CreateUpdateDadosAlunoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}"));
            return Post(requestUrl, command);
        }

        /// <summary>
        /// Alteração do Aluno
        /// </summary>
        /// <param name="id">Id de alteração do Aluno</param>
        /// <param name="command">Objeto de alteração do Aluno</param>
        /// <returns>Retorna true ou false</returns>
        public Task<bool> UpdateDados(int id, AlunoModel.CreateUpdateDadosAlunoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}/{id}"));
            return Put(requestUrl, command);
        }

        /// <summary>
        /// Alteração de Qr Code do Aluno
        /// </summary>
        /// <param name="id">Id de alteração de Qr Code do Aluno</param>
        /// <param name="command">Objeto de alteração de Qr Code Aluno</param>
        /// <returns>Retorna true ou false</returns>
        public Task<bool> UpdateQrCode(int id, AlunoModel.CreateUpdateDadosAlunoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}/QrCode/{id}"));
            return Put(requestUrl, command);
        }

        /// <summary>
        /// Alteração da Foto do Aluno
        /// </summary>
        /// <param name="id">Id de alteração da Foto do Aluno</param>
        /// <param name="command">Objeto de alteração da Foto do Aluno</param>
        /// <returns>Retorna true ou false</returns>
        public Task<bool> UpdateAlunoFoto(int id, AlunoModel.CreateUpdateDadosAlunoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}/UploadFoto/{id}"));
            return Put(requestUrl, command);
        }
        /// <summary>
        /// Exclusão de Aluno
        /// </summary>
        /// <param name="id">Id de exclusão de Aluno </param>
        /// <returns>Retorna true ou false</returns>
        public Task<bool> DeleteDados(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Busca todos os Aulo
        /// </summary>
        /// <returns>Retorna a lista de Aluno</returns>
        public List<AlunoDto> GetAlunosAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}"));
            return Get<List<AlunoDto>>(requestUrl);
        }

        /// <summary>
        /// Busca um único Aluno
        /// </summary>
        /// <param name="id">Id de Aluno a ser buscado</param>
        /// <returns>Retorna o objeto de Aluno</returns>
        public AlunoDto GetAlunoById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}/{id}"));
            return Get<AlunoDto>(requestUrl);
        }

        /// <summary>
        /// Busca Aluno por Email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>Retorna uma lista de Email</returns>
        public AlunoDto GetAlunoByEmail(string email)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}/Email/{email}"));
            return Get<AlunoDto>(requestUrl);
        }

        /// <summary>
        /// Busca Aluno por Rede Asp
        /// </summary>
        /// <param name="aspNetUserId">aspNetUserId</param>
        /// <returns>Retorna o Aluno por rede</returns>
        public AlunoDto GetAlunoByAspNetUser(string aspNetUserId)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}/AspNetUserId/{aspNetUserId}"));
            return Get<AlunoDto>(requestUrl);
        }

        /// <summary>
        /// Busca Aluno por Localidade
        /// </summary>
        /// <param name="id">Id de Aluno a ser buscado</param>
        /// <returns>Retorna a uma localidade</returns>
        public List<AlunoIndexDto> GetAlunosByLocalidade(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}/Localidade/{id}"));
            return Get<List<AlunoIndexDto>>(requestUrl);
        }

        /// <summary>
        /// Busca Todos Nome de Aluno
        /// </summary>
        /// <param name="id">Id da localidade a ser buscado</param>
        /// <returns>Retorna a todos os Aluno</returns>
        public List<SelectListDto> GetNomeAlunosAll(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}/NomeAlunos/{id}"));
            return Get<List<SelectListDto>>(requestUrl);
        }

        /// <summary>
        /// Busca Aluno por Filtro 
        /// </summary>
        /// <param name="searchFilter">filtro para pesquisas de Aluno</param>
        /// <returns>retorna a lista de Alunos</returns>
        public Task<AlunosFilterDto?> GetAlunosByFilter(AlunosFilterDto searchFilter)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}/Filter"));
            return GetFiltro(requestUrl, searchFilter);
        }

        /// <summary>
        /// Busca Todos Nome de Aluno
        /// </summary>
        /// <param name="id">Id do Profissional</param>
        /// <returns>Retorna lista dos alunos</returns>
        public List<SelectListDto> GetNomeAlunosByProfissionalId(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}/Profissional/{id}"));
            return Get<List<SelectListDto>>(requestUrl);
        }

        #endregion
    }
}