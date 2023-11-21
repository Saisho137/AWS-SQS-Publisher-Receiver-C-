using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace PublicadorSQS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sqsURL = "https://sqs.us-east-2.amazonaws.com/416835198340/ExampleSQS"; /*Your SQS url*/

                IAmazonSQS client = new AmazonSQSClient("YOUR_ACCESS_KEY_ID", "YOUR_SECRET_ACCESS_KEY", RegionEndpoint.USEast2 /*Your region*/);

                SendMessageRequest request = new SendMessageRequest
                {
                    MessageBody = textBox1.Text,
                    QueueUrl = sqsURL
                };

                var response = await client.SendMessageAsync(request);

                if(response.HttpStatusCode == System.Net.HttpStatusCode.OK) {
                    MessageBox.Show("The message has been sent successfully");
                    textBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("The message could not be sent, please try again.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("The message could not be sent, please try again." + ex);
            }
        }
    }
}