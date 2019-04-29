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
            HttpResponseMessage response = await client.GetAsync(path);
            if(response.IsSuccessStatusCode){
            prog1 = await response.Content.ReadAsAsync<programCheck>();
            }
            return prog1;
        }

         static async Task<programCheck> UpdateProductAsync(programCheck product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/programcheck/{product.Id}", product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = await response.Content.ReadAsAsync<programCheck>();
            return product;
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //Please Don't add any code in the main function. Any Additional code needs to be 
            // updated on the try on RunAsync function.
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
                
                //Show the product
                ShowProgramCheck(prog2);
                //Only show programCount off the prog2
                Console.WriteLine(prog2.programCount);

                // Update the product
                Console.WriteLine("Updating programCount...");
                prog2.programCount = 7;
                await UpdateProductAsync(prog2);
               
               ShowProgramCheck(prog2);
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
