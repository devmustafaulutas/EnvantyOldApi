namespace envantyService.Service
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class ChatbotService
    {
        private readonly HttpClient _httpClient;

        public ChatbotService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetChatbotAnswerAsync(string query)
        {
            var requestBody = new { query = query };
            var response = await _httpClient.PostAsJsonAsync("http://localhost:8000/chat", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Chatbot API error: {error}");
            }

            var responseData = await response.Content.ReadFromJsonAsync<ChatbotResponse>();
            return responseData?.answer ?? "Boş cevap döndü.";
        }


        public async Task<string> GetChatbotAnswer2Async(string query)
        {
            var requestBody = new { query = query };
            var response = await _httpClient.PostAsJsonAsync("http://localhost:8000/chat/2", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Chatbot API error: {error}");
            }

            var responseData = await response.Content.ReadFromJsonAsync<ChatbotResponse>();
            return responseData?.answer ?? "Boş cevap döndü.";
        }
    }

    public class ChatbotResponse
    {
        public string answer { get; set; }
    }



}