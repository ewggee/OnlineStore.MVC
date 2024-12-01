namespace OnlineStore.Core.Common.Models
{
    /// <summary>
    /// Класс настроек декорирования сервисов.
    /// </summary>
    public sealed class DecoratorSettings
    {
        /// <summary>
        /// Переключатель декорирования для всех сервисов.
        /// </summary>
        public bool AllowDecoration { get; set; }

        /// <summary>
        /// Переключатель декорирования для сервиса атрибутов.
        /// </summary>
        public bool UseCachedProductAttributeService { get; set; }

        /// <summary>
        /// Переключатель декорирования для сервиса корзины.
        /// </summary>
        public bool UseCachedCartService { get; set; }
    }
}
