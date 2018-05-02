using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GraphQLClient;
using GraphQLClient.Implementations;
using Newtonsoft.Json;

namespace GraphQL_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseUlr = @"http://graphql.org/swapi-graphql/";

            //HttpClient client = new HttpClient();
            //var paramsDictionary = new Dictionary<string, string>
            //{
            //    {"query", "query Opa($name: String) {somedd: pokemon(name: $name) {id}}"},
            //    {"variables", "{\"name\":\"Pikachu\"}"},
            //    //{"operationName", "Opa"}
            //};

            //var content = new FormUrlEncodedContent(paramsDictionary);

            //var result = client.PostAsync(baseUlr, content).Result;

            //var str = result.Content.ReadAsStringAsync().Result;


            //var cl = new QlClinet(baseUlr);

            //Task.Run(async () =>
            //{
            //    var rr =  await cl.ExecuteQuery<FooClass>("{pokemon(name: \"Pikachu\") { id }}");
            //}).Wait();



            // -------------


            //new QueryBuilder().CreateQuery("blah-blah").AddFields
            //(() => new QLField(("lol","kek"))
            //                 .WithAlias("opa")
            //                 .AddFields(()=> new QLField()),
            //() => new QLField(),
            //() => new QLField());

            var query = new QueryBuilder()
                                .CreateQuery()
                                .AddFields
                                (
                                    () => new QLField("allPeople", ("first", "2"))
                                        .AddFields(()=> new QLField("people")
                                                   .AddFields(()=> new QLField("name"),
                                                              ()=> new QLField("gender")))
                                                
                                );

            var queryString = query.ToString();
        }
    }

    public class FooClass
    {
        [JsonProperty("pokemon")]
        public Pokemon Pokemon { get; set; }
    }

    public class Pokemon
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
