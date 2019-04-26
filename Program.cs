using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace heartBeatApp
{
     public class programCheck
    {
        public int Id {get; set;}
        public string programName{get; set;}
        public int programCount{get; set;}
        public int programLimit { get; set;}

    }
    class Program
    {
        static HttpClient client  = new HttpClient();
        static void ShowProgramCheck(programCheck prog1){
            Console.WriteLine($"Name: {prog1.programName}, {prog1.programCount}, {prog1.programLimit}");
        }

        static async Task<programCheck> GetProductAsync(string path){
            programCheck prog1 = null; 
            //programCheck prog2 = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if(response.IsSuccessStatusCode){
            prog1 = await response.Content.ReadAsAsync<programCheck>();
            //prog1 = JsonConvert.DeserializeAnonymousType<programCheck>(prog1);
            //var objResponse1 =  JsonConvert.DeserializeObject<List<programCheck>>(prog1);
            }
            return prog1;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync(){
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            try{
                programCheck prog2 = new programCheck();

                //Get the product
                prog2 = await GetProductAsync("http://localhost:5000/api/programcheck/1");
                //var prog3 = JsonConvert.DeserializeObject<programCheck>(prog2);
                ShowProgramCheck(prog2);
                Console.WriteLine(prog2.programCount);
               // Console.WriteLine(prog2);
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
