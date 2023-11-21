using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace ReceptorSQS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string sqsURL = "https://sqs.us-east-2.amazonaws.com/244073897298/ExampleSQS"; /*Your SQS url*/

            IAmazonSQS client = new AmazonSQSClient("YOUR_ACCESS_KEY_ID", "YOUR_SECRET_ACCESS_KEY", RegionEndpoint.USEast2 /*Your Region*/);

            ReceiveMessageRequest request = new ReceiveMessageRequest
            {
                QueueUrl = sqsURL
            };

            var response = await client.ReceiveMessageAsync(request);

            var messages = response.Messages;

            if(messages.Count > 0)
            {
                JObject json = JObject.Parse(Environment.NewLine + messages[0].Body);
                JToken message = json["Message"];
                JToken subject = json["Subject"];
                textBox1.Text = textBox1.Text + "\r\nSubject: " + subject + "\r\n" + "Message: " + message;
                await client.DeleteMessageAsync(sqsURL, messages[0].ReceiptHandle);
            }
            else
            {
                MessageBox.Show("No new messages found");
            }
        }
    }
}