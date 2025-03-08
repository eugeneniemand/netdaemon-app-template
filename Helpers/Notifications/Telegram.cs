using System.Text.Json.Serialization;

namespace Niemand.Helpers.Notifications;

public static class ObservableExtensions
{
    public static TelegramCallbackHandler HandleTelegramCallback(this IObservable<Event> source, TelegramBotServices bot, ILogger<object> logger)
    {
        return new TelegramCallbackHandler(source.Filter<TelegramCallback>("telegram_callback"), bot, logger);
    }
}

public class TelegramCallbackHandler
{
    private readonly IObservable<Event<TelegramCallback>> _source;
    private readonly TelegramBotServices bot;
    private readonly ILogger<object> logger;
    private readonly List<IDisposable> _subscriptions = new();

    public TelegramCallbackHandler(IObservable<Event<TelegramCallback>> source, TelegramBotServices bot, ILogger<object> logger)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
        this.bot = bot;
        this.logger = logger;
    }

    public TelegramCallbackHandler On(string command, Action<TelegramCallback, TelegramCallbackHandler> handler)
    {
        if (string.IsNullOrEmpty(command)) return this;
        if (handler == null) return this;

        var subscription = _source
            .Where(e => string.Equals(e?.Data?.Command, command, StringComparison.OrdinalIgnoreCase))
            .Subscribe(e =>
            {
                logger.LogDebug($"Telegram callback command: {command} {string.Join(" ", e.Data?.Args ?? [])}");
                handler(e.Data, this);
            });

        _subscriptions.Add(subscription);
        return this;
    }

    public void EditMessage(TelegramCallback e, string message)
    {
        bot.EditMessage(new TelegramBotEditMessageParameters()
        {
            Message = message,
            MessageId = e.Message.MessageId.ToString(),
            ChatId = e.Message.Chat.Id.ToString(),
            InlineKeyboard = new List<string>()
        });
    }

}


// Root record for the callback data
public record TelegramCallback(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("chat_instance")] string ChatInstance,
    [property: JsonPropertyName("data")] string Data,
    [property: JsonPropertyName("message")] Message Message,
    [property: JsonPropertyName("chat_id")] long ChatId,
    [property: JsonPropertyName("user_id")] long UserId,
    [property: JsonPropertyName("from_first")] string FromFirst,
    [property: JsonPropertyName("from_last")] string FromLast,
    [property: JsonPropertyName("command")] string Command,
    [property: JsonPropertyName("args")] IReadOnlyList<string> Args,
    [property: JsonPropertyName("origin")] string Origin,
    [property: JsonPropertyName("time_fired")] DateTimeOffset TimeFired,
    [property: JsonPropertyName("context")] Context Context
);

// Nested record for the 'message' object
public record Message(
    [property: JsonPropertyName("channel_chat_created")] bool ChannelChatCreated,
    [property: JsonPropertyName("delete_chat_photo")] bool DeleteChatPhoto,
    [property: JsonPropertyName("group_chat_created")] bool GroupChatCreated,
    [property: JsonPropertyName("reply_markup")] ReplyMarkup ReplyMarkup,
    [property: JsonPropertyName("supergroup_chat_created")] bool SupergroupChatCreated,
    [property: JsonPropertyName("text")] string Text,
    [property: JsonPropertyName("chat")] Chat Chat,
    [property: JsonPropertyName("date")] long Date,
    [property: JsonPropertyName("message_id")] long MessageId,
    [property: JsonPropertyName("from")] From From
);

// Nested record for 'reply_markup'
public record ReplyMarkup(
    [property: JsonPropertyName("inline_keyboard")] IReadOnlyList<IReadOnlyList<InlineKeyboardButton>> InlineKeyboard
);

// Nested record for an inline keyboard button
public record InlineKeyboardButton(
    [property: JsonPropertyName("callback_data")] string CallbackData,
    [property: JsonPropertyName("text")] string Text
);

// Nested record for 'chat'
public record Chat(
    [property: JsonPropertyName("first_name")] string FirstName,
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("last_name")] string LastName,
    [property: JsonPropertyName("type")] string Type
);

// Nested record for 'from'
public record From(
    [property: JsonPropertyName("first_name")] string FirstName,
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("is_bot")] bool IsBot,
    [property: JsonPropertyName("username")] string Username
);

// Nested record for 'context'
public record Context(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("parent_id")] string? ParentId,
    [property: JsonPropertyName("user_id")] string? UserId
);