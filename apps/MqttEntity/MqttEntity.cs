using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client.Options;
using NetDaemon.Common;


namespace LightManagerV2;

// [Focus]
[NetDaemonApp]
public class MqttEntity
{
    public MqttEntity()
    {
        TestMqttClient().GetAwaiter().GetResult();
    }

    public async Task TestMqttClient()
    {
        var factory    = new MqttFactory();
        var mqttClient = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
                      .WithTcpServer("192.168.1.3", 1883) // Port is optional
                      .Build();

        await mqttClient.ConnectAsync(options, CancellationToken.None);


        var discovery_message = new MqttApplicationMessageBuilder()
                                .WithTopic("homeassistant/binary_sensor/nd1/config")
                                .WithPayload("{\"name\": \"nd_test\", \"device_class\": \"motion\", \"state_topic\": \"netdaemon/binary_sensor/nd1/state\"}")
                                .Build();

        await mqttClient.PublishAsync(discovery_message, CancellationToken.None); // Since 3.0.5 with CancellationToken

        var message = new MqttApplicationMessageBuilder()
                      .WithTopic("netdaemon/binary_sensor/nd1/state")
                      .WithPayload("ON")
                      .Build();

        await mqttClient.PublishAsync(message, CancellationToken.None); // Since 3.0.5 with CancellationToken

        var delete_message = new MqttApplicationMessageBuilder()
                             .WithTopic("homeassistant/binary_sensor/nd1/config")
                             .Build();

        await mqttClient.PublishAsync(delete_message, CancellationToken.None); // Since 3.0.5 with CancellationToken
    }
}