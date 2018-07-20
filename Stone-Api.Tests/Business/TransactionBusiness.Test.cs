using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using StoneApi.Business;
using Xunit;

namespace StoneApi.Tests
{
    public class BusinessTest
    {
        public IServiceCollection Service {get;} = new ServiceCollection();
        ITransactionBusiness Business {get;}
        public BusinessTest()
        {
            Startup.ConfigureServices(Service);    
            Business = Service.BuildServiceProvider().GetService<ITransactionBusiness>();
        }
        
        [Theory(DisplayName = "Carrega registros da business e compara os CNPJ")]
        [InlineData(1, 77404852000179)]
        [InlineData(2, 30481457000126)]
        [InlineData(3, 28176030000172)]
        public void GetByIdAndCompareCNPJ(int id, double cnpj)
            => Assert.True(Business.GetById(id).MerchantCnpj == cnpj);

        
        [Theory(DisplayName = "Busca filtrada pelo cartão de crédito, valores recebidos não podem estar na lista dos cartões abaixo")]
        [InlineData("Maestro, Elo Debito")]
        [InlineData("Alelo Alimentacao, Alelo Refeicao")]
        [InlineData("Credz")]
        [InlineData("Electron")]
        [InlineData("Elo Credito")]
        [InlineData("Hipercard")]
        [InlineData("Mastercard, Sodexo Alimentacao")]
        [InlineData("Visa,Sodexo Refeicao")]
        public void GetWithFilter(string brands){            
            var brandList = brands.Split(',');
            var responseList = Business.GetWithQueryParameters( brand: brandList.ToArray());
            
            foreach(var responseItem in responseList)
                    Assert.True(brandList.Any(o => o == responseItem.CardBrandName), $"Bandeira recebida: {responseItem.CardBrandName} - Bandeiras esperasas: {brands}");        
        }

        [Theory(DisplayName = "Filtros com data incial e final devendo retornar apenas o range entre elas")]
        [InlineData("2018-02-01", "2018-02-28")]
        [InlineData("2018-03-01", "2018-03-30")]
        public void GetRowsByDate(string startDate, string endDate){
            var listOfDates = Business.GetWithQueryParameters(
                startDate: Convert.ToDateTime(startDate), 
                endDate: Convert.ToDateTime(endDate)
            ).Select(o => o.AcquirerAuthorizationDateTime);

            foreach(var date in listOfDates)
                Assert.True(date.Date >= Convert.ToDateTime(startDate) && date.Date <= Convert.ToDateTime(endDate), $"Data errada: {date}");
        }

        [Theory(DisplayName = "Teste para a união dos filtros de data e bandeira")]
        [InlineData("2018-02-01", "2018-02-28", "Maestro,Elo Debito,Visa")]
        [InlineData("2018-03-01", "2018-03-30", "Maestro")]
        public void GetRowsByFilter(string startDateString, string endDateString, string brand){
            var startDate =  Convert.ToDateTime(startDateString);
            var endDate =  Convert.ToDateTime(endDateString);
            
            var listOfRows = Business.GetWithQueryParameters(
                startDate: startDate, 
                endDate: endDate,
                brand: brand.Split(',')
            );
            
            foreach(var item in listOfRows)                
                    Assert.True(
                        brand.Split(',').Any(o => o == item.CardBrandName) && 
                        item.AcquirerAuthorizationDateTime.Date >= startDate && 
                        item.AcquirerAuthorizationDateTime.Date <= endDate, 
                        $"bandeira recebida: {item.CardBrandName} - bandeiras esperada: {brand.Split(',')}\n" +
                        $"data recebida: {item.AcquirerAuthorizationDateTime.Date} - datas limite: {startDate} <> {endDate}");
        }

        [Theory(DisplayName = "Passar datas que excedem os limites das datas no banco")]
        [InlineData("2019-01-01", "2019-01-31")]
        [InlineData("2018-01-01", "2018-01-31")]
        public void DateBigger(string startDateString, string endDateString){
            var startDate = Convert.ToDateTime(startDateString);
            var endDate = Convert.ToDateTime(endDateString);

            var listOfRows = Business.GetWithQueryParameters(
                startDate: startDate, 
                endDate: endDate
            );

            Assert.True(listOfRows.Count() == 0 , $"a lista deveria estar vazia, mas ela tem: {listOfRows.Count()} registros");
        }

    }
}
