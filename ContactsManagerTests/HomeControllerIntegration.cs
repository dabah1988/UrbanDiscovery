using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Fizzler;
using Fizzler.Systems.HtmlAgilityPack;
using Xunit;
using System.Net.Http;
namespace ServiceCountryPersonTests
{
    public class HomeControllerIntegration : IClassFixture<CustomWebFactoryHttpClient>
    {
        private readonly HttpClient _httpClient;
    
        public HomeControllerIntegration(CustomWebFactoryHttpClient factoryHttpClient)
        {
            _httpClient =  factoryHttpClient.CreateClient();
        }
        [Fact]
        public async Task Index_ShouldReturnView()
        {
           HttpResponseMessage httpResponseMessage =  await _httpClient.GetAsync("/");
            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            string responBody = await httpResponseMessage.Content.ReadAsStringAsync();

            HtmlAgilityPack.HtmlDocument  htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(responBody);
            var document = htmlDocument.DocumentNode;
            document.QuerySelector("table").Should().NotBeNull();


        }
    }
}
