namespace hr.makemystamp.com.core.Events
{
    public record ResponseDto
    {
        public string Id { get; set; }
        public string ActionMessage { get; set; } = string.Empty;
        public ResponseDto()
        {

        }
        public ResponseDto(string id, string actionMessage)
        {
            Id = id;
            ActionMessage = actionMessage;
        }

    }
}
