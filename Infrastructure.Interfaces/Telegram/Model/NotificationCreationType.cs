namespace Infrastructure.Interfaces.Telegram.Model
{
    public enum NotificationCreationType
    {
        /// <summary>
        /// Текстовое сообщение без картинки
        /// </summary>
        Default,
        /// <summary>
        /// Текстовое сообщение с картинкой
        /// </summary>
        WithImage
    }
}
