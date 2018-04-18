using Assets.Scripts.Entities;
using Assets.Scripts.Enums;

namespace Assets.Scripts.ViewModels
{
    public class ChatMessage : EntityBase
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                if(_message == value)
                    return;
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        private MessageSender _sender;

        public MessageSender Sender
        {
            get { return _sender; }
            set
            {
                if(_sender == value)
                    return;
                _sender = value;
                OnPropertyChanged("Sender");
            }
        }

        public static implicit operator ChatMessage(string message)
        {
            return new ChatMessage{Message = message, Sender = MessageSender.Customer};
        }

        public static implicit operator string(ChatMessage message)
        {
            return message.Message;
        }
    }
}