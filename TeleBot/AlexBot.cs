using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace TeleBot
{
    public class AlexBot
    {
        public ITelegramBotClient _bot { get; private set; }
        private IEnumerable<string> _anchor = new List<string> {
            "лех",
            "лёх",
            "алекс",
            "леш",
            "лёш",
            "олекс"
        };

        private IEnumerable<KeyValuePair<string, string>> _replyTemplates = new List<KeyValuePair<string, string>> {
            new KeyValuePair<string, string>("рыб", "Эт на голове у тя рыба"),
            new KeyValuePair<string, string>("личк", "Иичка у тя в штанах"),
            new KeyValuePair<string, string>("скот", "Давайте жить дружно"),
            new KeyValuePair<string, string>("скот", "...обаза"),
            new KeyValuePair<string, string>("рыб", "Тишка!"),
            new KeyValuePair<string, string>("бред", "а жизнь ща многих..."),
            new KeyValuePair<string, string>("собак", "кто сегодня полетит... на собаке рыжей?"),
            new KeyValuePair<string, string>("собак", "чо творите, псы немытые"),
            new KeyValuePair<string, string>("бред", "это было в серии шашлычника"),
            new KeyValuePair<string, string>("вышел", "или вздернулся"),
            new KeyValuePair<string, string>("докажи", "а ты стерва"),
            new KeyValuePair<string, string>("отзовись", "собаку свою так назови"),
        };

        private IEnumerable<string> _defaultReplies = new List<string>
        {
            "Ад",
            "Мило",
            "Жестко",
            "АД",
            "Влад ответит",
            "Оч",
            "Толпа",
            "Лучший",
            "ща бы куры с пеной",
            "четко!",
            "выбесили"
        };

        public AlexBot(ITelegramBotClient bot)
        {
            _bot = bot;
            _bot.OnMessage += BotClient_OnMessage;
            _bot.StartReceiving();
        }

        private bool ShouldReply(string message)
        {
            var words = message.ToLower().Split(' ').ToList();
            words.ForEach(_ => _.Replace(" ", ""));
            return words.Any(w => _anchor.Any(a => w.Contains(a)));
        }

        private string GetReply(string message)
        {
            var replyTemplateQuery = _replyTemplates.Where(_ => message.Contains(_.Key));
            var r = new Random(DateTime.Now.Millisecond);
            if (!replyTemplateQuery.Any())
                return _defaultReplies.ElementAt(r.Next(0, _defaultReplies.Count()));
            var replyPosition = r.Next(0, replyTemplateQuery.Count());
            return replyTemplateQuery.ElementAt(replyPosition).Value;
        }

        private void BotClient_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Type != MessageType.TextMessage || !ShouldReply(e.Message.Text)) return;
            _bot.SendChatActionAsync(e.Message.Chat.Id, ChatAction.Typing);
            _bot.SendTextMessageAsync(e.Message.Chat.Id, GetReply(e.Message.Text));
        }
    }
}
