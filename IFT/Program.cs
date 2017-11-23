using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IFT
{
    public class Campagne
    {
        public String Id { get; set; }
        public String IdMetier { get; set; }
        public String Libelle { get; set; }
        public Boolean Active { get; set; }
    }
    class Program
    {
        static HttpClient client = new HttpClient();

        static async Task GetHelloAsStringAsync()
        {
            Console.WriteLine("GET hello");
            HttpResponseMessage response = await client.GetAsync("hello");
            if (response.IsSuccessStatusCode)
            {
                String hello = await response.Content.ReadAsStringAsync();
                Console.WriteLine(hello);
            }
            Console.WriteLine();
        }

        static async Task GetAllCampagnesAsStringAsync()
        {
            Console.WriteLine("GET campagnes");
            HttpResponseMessage response = await client.GetAsync($"campagnes");
            if (response.IsSuccessStatusCode)
            {
                String campagnes = await response.Content.ReadAsStringAsync();
                Console.WriteLine(campagnes);
            }
            Console.WriteLine();
        }

        static async Task GetAllCampagnesAsync()
        {
            Console.WriteLine("GET campagnes");
            HttpResponseMessage response = await client.GetAsync($"campagnes");
            if (response.IsSuccessStatusCode)
            {
                List<Campagne> campagnes = await response.Content.ReadAsAsync<List<Campagne>>();
                campagnes.ForEach(campagne =>
                {
                    Console.WriteLine($"Id: {campagne.Id}\tIdMetier: {campagne.IdMetier}\tLibelle: {campagne.Libelle}\tActive: {campagne.Active}");
                });
            }
            Console.WriteLine();
        }

        static async Task GetCampagneAsStringAsync(String idMetier)
        {
            Console.WriteLine($"GET campagnes/{idMetier}");
            HttpResponseMessage response = await client.GetAsync($"campagnes/{idMetier}");
            if (response.IsSuccessStatusCode)
            {
                String campagne = await response.Content.ReadAsStringAsync();
                Console.WriteLine(campagne);
            }
            Console.WriteLine();
        }

        static async Task GetCampagneAsync(String idMetier)
        {
            Console.WriteLine($"GET campagnes/{idMetier}");
            HttpResponseMessage response = await client.GetAsync($"campagnes/{idMetier}");
            if (response.IsSuccessStatusCode)
            {
                Campagne campagne = await response.Content.ReadAsAsync<Campagne>();
                Console.WriteLine($"Id: {campagne.Id}\tIdMetier: {campagne.IdMetier}\tLibelle: {campagne.Libelle}\tActive: {campagne.Active}");
            }
            Console.WriteLine();
        }

        static async Task GetProduitsDosesReferenceAsStringAsync(String campagneId, String cultureId, String produitId)
        {
            String query = $"campagneId={campagneId}&cultureId={cultureId}&produitId={produitId}";

            Console.WriteLine($"GET produits-doses-reference");
            HttpResponseMessage response = await client.GetAsync($"produits-doses-reference?{query}");
            if (response.IsSuccessStatusCode)
            {
                String produitsDosesReference = await response.Content.ReadAsStringAsync();
                Console.WriteLine(produitsDosesReference);
            }
            Console.WriteLine();
        }

        static async Task GetIftTraitementAsStringAsync(String campagneId, String numeroAmmId, String cultureId, String cibleId, String traitementId, String uniteId, Double dose, Double facteurDeCorrection)
        {
            String query = $"campagneId={campagneId}" +
                           $"&numeroAmmId={numeroAmmId}" +
                           $"&cultureId={cultureId}" +
                           $"&cibleId={cibleId}" +
                           $"&traitementId={traitementId}" +
                           $"&uniteId={uniteId}" +
                           $"&dose={dose}" +
                           $"&facteurDeCorrection={facteurDeCorrection}";

            Console.WriteLine($"GET ift/traitement");
            HttpResponseMessage response = await client.GetAsync($"ift/traitement?{query}");
            if (response.IsSuccessStatusCode)
            {
                String produitsDosesReference = await response.Content.ReadAsStringAsync();
                Console.WriteLine(produitsDosesReference);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://ift-api.herokuapp.com/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                await GetHelloAsStringAsync();
                await GetAllCampagnesAsStringAsync();
                await GetAllCampagnesAsync();
                await GetCampagneAsStringAsync("2017");
                await GetCampagneAsync("2017");
                await GetProduitsDosesReferenceAsStringAsync("e02b1e37-669b-497e-be4c-e8b9359cb587", "6f288e46-ead7-491d-9213-b498dcf82329", "64fa2f66-9cd6-46af-8ff0-2882edcfd869");
                await GetIftTraitementAsStringAsync("e02b1e37-669b-497e-be4c-e8b9359cb587",
                                                    "d76038c9-c456-4487-91ea-3fa1a8853978",
                                                    "6f288e46-ead7-491d-9213-b498dcf82329",
                                                    "19cc0b09-7d11-4ddf-848d-4f6c94a4febd",
                                                    "f4dd98e2-0638-4912-86ed-8cc3a9e9d728",
                                                    "d57ca2cc-a3ba-4bff-bae7-0d0bde82a0e3",
                                                    0.06,
                                                    100);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
