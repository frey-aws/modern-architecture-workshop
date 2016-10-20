using System;
using System.Diagnostics;

using Amazon.SQS;
using Amazon.SQS.Model;

namespace Koch.WorkerTwo
{
    class Program
    {


        private static int x;

        static void Main(string[] args)
        {
            ListenAndDoWorkWhenAsked();
        }

        private static void ListenAndDoWorkWhenAsked()
        {
            while (true)
            {
                try
                {
                    using (var client = new AmazonSQSClient(Amazon.RegionEndpoint.USEast1))
                    {
                        var receiveRequest = new ReceiveMessageRequest();
                        receiveRequest.QueueUrl = "sqs.us-east-1.amazonaws.com/787803147655/WorkForWorkerTwo";
                        var response = client.ReceiveMessage(receiveRequest);

                        foreach (Message item in response.Messages)
                        {
                            Console.WriteLine(item.Body);
                            // have to manually delete a message that was processed ...
                            client.DeleteMessage(receiveRequest.QueueUrl, item.ReceiptHandle);

                            Console.WriteLine("Did the work when asked");

                            InitiateWork();


                            break;
                        }

                        response.Messages.Clear();
                    }

                    Console.WriteLine("WorkerThree: Waiting for some work to do");

                    System.Threading.Thread.Sleep(5000);        // sleep for 5 seconds, this should only fire when the job is ready to be run ... which should take longer than 5 seconds
                }
                catch (Exception ex)
                {
                    // add error/logging handling
                }
            }
        }

        private static void InitiateWork()
        {
            try
            {
                using (var client = new AmazonSQSClient(Amazon.RegionEndpoint.USEast1))
                {
                    client.SendMessage(new SendMessageRequest
                    {
                        QueueUrl = "sqs.us-east-1.amazonaws.com/787803147655/WorkForWorkerOne",
                        MessageBody = "WorkerOne has some work for you to do:  Message - " + x++          // execution occurs upon receipt of message ... 
                    });
                }
            }
            catch (Exception ex)
            {
                // add error/logging handling
            }
        }
    }
}
