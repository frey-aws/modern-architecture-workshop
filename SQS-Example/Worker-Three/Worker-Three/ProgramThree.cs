﻿using System;
using System.Diagnostics;

using Amazon.SQS;
using Amazon.SQS.Model;

namespace Koch.WorkerThree
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
            Console.WriteLine("WorkerThree: Waiting for some work to do");

            while (true)
            {
                try
                {
                    using (var client = new AmazonSQSClient(Amazon.RegionEndpoint.USEast1))
                    {
                        var receiveRequest = new ReceiveMessageRequest();
                        receiveRequest.QueueUrl = "https://sqs.us-east-1.amazonaws.com/{ADDAWSACCOUNT}/ProcessTheData";
                        var response = client.ReceiveMessage(receiveRequest);

                        foreach (Message item in response.Messages)
                        {
                            Console.WriteLine(item.Body);
                            // have to manually delete a message that was processed ...
                            client.DeleteMessage(receiveRequest.QueueUrl, item.ReceiptHandle);

                            Console.WriteLine("Processed the data");

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
                    throw ex;
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
                        // Create work for Subscribers
                        QueueUrl = "https://sqs.us-east-1.amazonaws.com/{ADDAWSACCOUNT}/GetTheData",
                        MessageBody = "Please go get the data again " + x++          // execution occurs upon receipt of message ... 
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
