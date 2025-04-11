using MiniProjet.Projectmanagement.Contracts.Contracts.Responses;
using System.Text.Json;

namespace MiniProjet.ProjectManajement.Desktop;

public partial class Form1 : Form
{
    private readonly HttpClient _httpClient;

    public Form1()
    {
        InitializeComponent();

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7045/") // Replace with your API base URL
        };
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        // Optional: Set column headers (if not auto-generated)
        dataGridView1.AutoGenerateColumns = true; // This is true by default
                                                  // Or manually define columns if needed
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        try
        {
            // Call the API
            var response = await _httpClient.GetAsync("api/employees"); // Adjust endpoint path if needed
            response.EnsureSuccessStatusCode(); // Throws if not successful

            // Read and deserialize the response
            var json = await response.Content.ReadAsStringAsync();
            var employees = JsonSerializer.Deserialize<List<GetEmployeeResponse>>(json);

            // Bind data to DataGridView
            if (employees != null)
            {
                dataGridView1.DataSource = employees;
            }
            else
            {
                MessageBox.Show("No employees found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show($"Failed to connect to the API: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (JsonException ex)
        {
            MessageBox.Show($"Failed to parse response: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}