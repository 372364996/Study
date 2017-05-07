using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ
{
    class Program
    {
        static void Main(string[] args)
        {
            string type = Console.ReadLine();
            if (type=="1")
            {
                ConnectionFactory cf = new ConnectionFactory();
                cf.HostName = "127.0.0.1";
                cf.Port = 5672;
                using (IConnection conn=cf.CreateConnection() )
                {
                    using (IModel channel=conn.CreateModel())
                    {
                        //在MQ上定义一个持久化队列，如果名字相同不重复创建
                        channel.QueueDeclare("MyQueue", true, false, false, null);
                        while (true)
                        {
                            string message = string.Format("Message_{0}", Console.ReadLine());
                            byte[] buffer = Encoding.UTF8.GetBytes(message);
                            IBasicProperties properties = channel.CreateBasicProperties();
                            properties.DeliveryMode = 2;
                            channel.BasicPublish("", "MyQueue", properties, buffer);
                            Console.WriteLine("消息发送成功:" + message);
                        }
                    }
                }
            }
            else
            {
                //消费者
                ConnectionFactory factory = new ConnectionFactory();
                factory.HostName = "127.0.0.1";
                //默认端口
                factory.Port = 5672;
                using (IConnection conn = factory.CreateConnection())
                {
                    using (IModel channel = conn.CreateModel())
                    {
                        //在MQ上定义一个持久化队列，如果名称相同不会重复创建
                        channel.QueueDeclare("MyQueue", true, false, false, null);

                        //输入1，那如果接收一个消息，但是没有应答，则客户端不会收到下一个消息
                        channel.BasicQos(0, 1, false);

                        Console.WriteLine("Listening...");

                        //在队列上定义一个消费者
                        QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
                        //消费队列，并设置应答模式为程序主动应答
                        channel.BasicConsume("MyQueue", false, consumer);

                        while (true)
                        {
                            //阻塞函数，获取队列中的消息
                            BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                            byte[] bytes = ea.Body;
                            string str = Encoding.UTF8.GetString(bytes);

                            Console.WriteLine("队列消息:" + str.ToString());
                            //回复确认
                            channel.BasicAck(ea.DeliveryTag, false);
                        }
                    }
                }
            }
        }
    }
}
