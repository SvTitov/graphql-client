using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GraphQLClient;
using GraphQLClient.Implementations;
using GraphQLClient.Interfaces;
using Newtonsoft.Json;

namespace GraphQL_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseUlr = @"https://graphql-pokemon.now.sh";

            //new QueryBuilder().CreateQuery("blah-blah").AddFields
            //(() => new QLField(("lol","kek"))
            //                 .WithAlias("opa")
            //                 .AddFields(()=> new QLField()),
            //() => new QLField(),
            //() => new QLField());

            IFragment fragment = new QLFragment("somepeople", "Person")
                                      .AddFields(() => new QLField("name"),
                                                 () => new QLField("height"));

            var query = new QueryBuilder()
                                .CreateQuery("SomeQuery")
                                .AddFields
                                (
                                    () => new QLField("pokemon", ("name", "$name"))
                                                .AddFields(()=> new QLField("id"),
                                                           ()=> new QLField("attacks")
                                                                        .AddFields(()=> new QLField("special")
				                                                                                .AddDirectives("include", "if", "false")
                                                                                                .AddFields(()=> new QLField("name"),
                                                                                                           ()=> new QLField("type"),
                                                                                                           ()=> new QLField("damage"))))
                                ).AddVariables(new QLVariable{Name = "name", Type = "String", Value= "\"Pikachu\""});

            var queryString = query.ToString();
			var varStr = query.GetVariables();

            var cl = new QlClinet(baseUlr);

            Task.Run(async () =>
            {
                var rr =  await cl.ExecuteQuery<dynamic>(query.ToString());
            }
            ).Wait();
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
