using Newtonsoft.Json;

namespace TodoActions.Shared
{
    public class TodoModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        [JsonProperty(PropertyName = "completed")]
        public bool Completed { get; set; }
    }
}
