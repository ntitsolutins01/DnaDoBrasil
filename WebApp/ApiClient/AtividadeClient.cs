﻿using System.Collections;
using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    /// <summary>
    /// Atividade Client
    /// </summary>
    public partial class DnaApiClient
    {
	    private const string ResourceAtividade = "Atividades";

        #region Main Methods

        /// <summary>
        /// Inclusão de Atividade
        /// </summary>
        /// <param name="command">Objeto para inclusão de Atividade</param>
        /// <returns>Id de Atividade inserido</returns>
        public Task<long> CreateAtividade (AtividadeModel.CreateUpdateAtividadeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAtividade }"));
            return Post(requestUrl, command);
        }

        /// <summary>
        /// Alteração de Atividade
        /// </summary>
        /// <param name="id">Id de alteração de Atividade</param>
        /// <param name="command">Objeto de alteração de Atividade</param>
        /// <returns>Retorna true ou false</returns>
        public Task<bool> UpdateAtividade (int id, AtividadeModel.CreateUpdateAtividadeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAtividade }/{id}"));
            return Put(requestUrl, command);
        }

        /// <summary>
        ///  Exclusão de Atividade
        /// </summary>
        /// <param name="id">Id de exclusão de Atividade</param>
        /// <returns>Retorna true ou false</returns>
        public Task<bool> DeleteAtividade (int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAtividade }/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Busca uma única Atividade
        /// </summary>
        /// <param name="id">Id de Atividade a ser buscado</param>
        /// <returns>Retorna o objeto de Atividade</returns>
        public AtividadeDto GetAtividadeById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAtividade}/{id}"));
            return Get<AtividadeDto>(requestUrl);
        }

        /// <summary>
        /// Busca todas as Atividade cadastradas
        /// </summary>
        /// <returns>Retorna a lista de Atividade</returns>
        public List<AtividadeDto> GetAtividadesAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAtividade}"));
            return Get<List<AtividadeDto>>(requestUrl);
        }

        /// <summary>
        /// Busca lista de turmas pelo id da modalidade e id do profissional 
        /// </summary>
        /// <param name="modalidadeId">Id da modalidade</param>
        /// <param name="profissionalId">Id do profissional</param>
        /// <returns>Retorna a Lista de turmas</returns>
        public List<AtividadeDto> GetTurmasByModalidadeIdProfissionalId(int modalidadeId, int profissionalId)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAtividade}/Modalidade/{modalidadeId}/Profissional/{profissionalId}"));
            return Get<List<AtividadeDto>>(requestUrl);
        }

        /// <summary>
        /// Busca atividade pelo id da modalidade, id do profissional e turma
        /// </summary>
        /// <param name="modalidadeId">Id da modalidade</param>
        /// <param name="profissionalId">Id do profissional</param>
        /// <param name="turma">Turma do profissional</param>
        /// <returns>Retorna o objeto da atividade</returns>
        public AtividadeDto GetAtividadeByModalidadeIdProfissionalIdTurma(int modalidadeId, int profissionalId, string turma)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAtividade}/Modalidade/{modalidadeId}/Profissional/{profissionalId}/Turma/{turma}"));
            return Get<AtividadeDto>(requestUrl);
        }

        #endregion
    }
}