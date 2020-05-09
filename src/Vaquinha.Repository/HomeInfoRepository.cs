﻿using Dapper;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.ViewModels;
using Vaquinha.Repository.Provider;

namespace Vaquinha.Repository
{
    public class HomeInfoRepository : IHomeInfoRepository
    {
        private readonly ConvideDBConnectionProvider _convideDBConnectionProvider;

        public HomeInfoRepository(ConvideDBConnectionProvider convideDBConnectionProvider)
        {
            _convideDBConnectionProvider = convideDBConnectionProvider;
        }

        public async Task<HomeViewModel> RecuperarDadosIniciaisHomeAsync()
        {
            using var conn = _convideDBConnectionProvider.GetConnection();
            return await conn.QuerySingleOrDefaultAsync<HomeViewModel>(RecuperarDadosIniciaisHomeAsync_query);
        }

        private readonly static string RecuperarDadosIniciaisHomeAsync_query = @"
                                                                                SELECT
                                                                                SUM([Valor]) AS 'ValorTotalArrecadado', 
                                                                                COUNT(1) AS 'QuantidadeDoadores'
                                                                                FROM [Doacao] WITH (NOLOCK)
                                                                                ";
    }
}