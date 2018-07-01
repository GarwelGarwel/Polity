namespace Polity
{
    /// <summary>
    /// Outputs a text Message a.k.a. Description
    /// </summary>
    class MessageEffect : Effect
    {
        public override string Description => Message;

        public string Message { get; set; } = "";

        public MessageEffect(string message) => Message = message;
    }
}
