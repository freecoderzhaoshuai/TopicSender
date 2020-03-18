using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicSender
{
    class Program
    {
        static void Main(string[] args)
        {
            Send();
        }
        static void Send()
        {
            TopicClient topicClient;
            String Message = "Hello Subscription!";
            String connString = "Endpoint=sb://serivcebus1.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=fhZTCK4lF/GwtLL/j9q7X0TQkhG+6ejNvhVzFl+IaIY=";
            NamespaceManager namespaceManager = NamespaceManager.CreateFromConnectionString(connString);
            if (!namespaceManager.TopicExists("zstopic"))
            {
                TopicDescription topicDescription = new TopicDescription("zstopic");
                topicDescription.AutoDeleteOnIdle = TimeSpan.FromMinutes(60);
                namespaceManager.CreateTopic(topicDescription);
            }
            //创建TopicClient和SubscriptionClient
            topicClient = TopicClient.CreateFromConnectionString(connString, "zstopic");
            BrokeredMessage message = new BrokeredMessage(Message);
            topicClient.Send(message);
            topicClient.Close();
            Console.WriteLine("send ok");
        }
    }
}
