using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using Assets.Scripts.Entities;
using Assets.Scripts.Enums;
using Assets.Scripts.GameObjects;

namespace Assets.Scripts.ViewModels
{
    public class ChatControlViewModel : ViewModelBase
    {
        private ObservableCollection<ChatMessage> _messages = new ObservableCollection<ChatMessage>();

        public ObservableCollection<ChatMessage> Messages
        {
            get { return _messages; }
            set
            {
                if(_messages == value)
                    return;
                _messages = value;
                OnPropertyChanged("Messages");
            }
        }

        private string _currentMessage;

        public string CurrentMessage
        {
            get { return _currentMessage; }
            set
            {
                if(_currentMessage == value)
                    return;
                _currentMessage = value;
                OnPropertyChanged("CurrentMessage");
            }
        }

        public ICommand SendMessageCommand
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            Messages.Add(CurrentMessage);
                                            OnPropertyChanged("Messages");
                                            CurrentMessage = string.Empty;
                                            Messages.Add(new ChatMessage {Message = "Консультант скоро с вами свяжется.", Sender = MessageSender.Agent});
                                            OnPropertyChanged("Messages");
                                        });
            }
        }

        public ChatControlViewModel()
        {
            Messages.CollectionChanged += Messages_OnCollectionChanged;
        }

        ~ChatControlViewModel()
        {
            Messages.CollectionChanged -= Messages_OnCollectionChanged;
        }

        private void Messages_OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
           SceneControllerApartment.Instance.PlayChatSound((args.NewItems[0] as ChatMessage).Sender);
        }
    }
}