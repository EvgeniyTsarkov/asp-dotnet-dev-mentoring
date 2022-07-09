using HttpClientConsoleApp.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace HttpClientConsoleApp;

public class Program
{
    private const string GetProductsUrl = "https://localhost:7087/api/products";
    private const string GetCategoriesUrl = "https://localhost:7087/api/categories";
    private const string ContentTypeHeaderValue = "application/json";

    static HttpClient client = new HttpClient();

    static void ShowProducts(List<Product> products)
    {
        Console.WriteLine("List of Products");

        foreach (var product in products)
        {
            Console.WriteLine(product.ProductName);
        }
    }

    static void ShowCategories(List<Category> categories) 
    {
        Console.WriteLine("List of Categories");

        foreach(var category in categories) 
        {
            Console.WriteLine(category.CategoryName);
        }
    }

    static async Task<List<Product>> GetProductsAsync(string path)
    {
        var products = new List<Product>();

        var response = await client.GetAsync(path);

        if (response.IsSuccessStatusCode)
        {
            products = await response.Content.ReadFromJsonAsync<List<Product>>();
        }

        return products;
    }

    static async Task<List<Category>> GetCategoriesAsync(string path) 
    {
        var categories = new List<Category>();

        var response = await client.GetAsync(path);

        if (response.IsSuccessStatusCode)
        {
            categories = await response.Content.ReadFromJsonAsync<List<Category>>();
        }

        return categories;
    }

    static async Task RunAsync()
    {
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue(ContentTypeHeaderValue));

        try
        {
            var categories = await GetCategoriesAsync(GetCategoriesUrl);

            var products = await GetProductsAsync(GetProductsUrl);

            ShowCategories(categories);

            Console.WriteLine();

            ShowProducts(products);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Console.ReadLine();
    }

    static void Main()
    {
        RunAsync().GetAwaiter().GetResult();
    }
}
